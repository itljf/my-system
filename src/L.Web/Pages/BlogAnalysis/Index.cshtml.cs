using Flurl.Http;
using HtmlAgilityPack;
using L.WInformations;
using L.WInfoTags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace L.Web.Pages.BlogAnalysis
{
    public class IndexModel(
        IInformationAppService informationAppService,IInfoTagAppService infoTagAppService,Microsoft.AspNetCore.Hosting.IWebHostEnvironment host)
        : LPageModel
    {
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync([FromBody]AnalysisDto input)
        {
            // if (types == 0)
            // {
            // }
            if (input.isBatch)
            {
                if (await informationAppService.HasByUrl(input.urls))
                {
                    return new JsonResult(new { success = true });
                }
            }
            else
            {
                if (await informationAppService.HasByUrl(input.urls))
                {
                    return new JsonResult(new InformationEditDto(){Title = "信息已存在"});
                }
            }

            

            if (input.types==0)
            {
                try
                {
                
                    var information = await Analyze(input.urls);
                    if (input.isAutoSave)
                    {
                        //自动保存信息
                        var model = new InformationEditDto();
                        model.Title = information.Title;
                        model.Time = information.Time;
                        model.Author = information.Author;
                        model.Url = information.Url;
                        model.Cnt = information.Cnt;
                        model.IsReprint = true;
                        model.Tag = "";
                        model.Info = "";
                        model.Files = "";
                        model.Markdown = "";
                        model.BlogFiles = "";
                        await informationAppService.Create(model);
                    }

                    return new JsonResult(information);
                }
                catch
                {
                    return new JsonResult(new InformationEditDto(){Title = "出错了"});
                }
            }else if (input.types==1)
            {
                //微信
                if (input.isBatch)
                {
                    //批量操作
                    try
                    {
                        var information = await AnalyzeWX(input.urls);
                        if (information.Title == "出错了")
                        {
                            return new JsonResult(new { success = false });
                        }
                        if (input.isAutoSave)
                        {
                            //自动保存信息
                            var model = new InformationEditDto();
                            model.Title = information.Title;
                            model.Time = information.Time;
                            model.Author = information.Author;
                            model.Url = information.Url;
                            model.Cnt = information.Cnt;
                            model.IsReprint = true;
                            model.Tag = "";
                            model.Info = "";
                            model.Files = "";
                            model.Markdown = "";
                            model.BlogFiles = "";
                            await informationAppService.Create(model);
                        }

                        return new JsonResult(new { success = true });
                    }
                    catch (Exception e)
                    {
                        return new JsonResult(new { success = false });
                    }

                }
                else
                {
                    var information = await AnalyzeWX(input.urls);

                    if (input.isAutoSave)
                    {
                        //自动保存信息
                        var model = new InformationEditDto();
                        model.Title = information.Title;
                        model.Time = information.Time;
                        model.Author = information.Author;
                        model.Url = information.Url;
                        model.Cnt = information.Cnt;
                        model.IsReprint = true;
                        model.Tag = "";
                        model.Info = "";
                        model.Files = "";
                        model.Markdown = "";
                        model.BlogFiles = "";
                        await informationAppService.Create(model);
                    }
                    return new JsonResult(information);
                }
                
            }
            return new JsonResult(new InformationEditDto(){Title = "出错了"});
        }

        #region 解析方法

        public async Task<InformationDto> AnalyzeWX(string url)
        {
            InformationDto model = new InformationDto();
            var html = await url.WithTimeout(TimeSpan.FromSeconds(10)).GetStringAsync();
            if (html==null)
            {
                return new InformationDto(){Title = "出错了"};
            }
            // 创建 HtmlDocument 对象
            var doc = new HtmlDocument();

            // 加载 HTML 字符串
            doc.LoadHtml(html);
            model.Url = url;
            if (doc.DocumentNode.SelectSingleNode("//*[@id=\"js_content\"]") == null)
            {
                return new InformationDto(){Title = "出错了"};
            }
            model.Title=doc.DocumentNode.SelectSingleNode("//*[@id=\"activity-name\"]").InnerText;
            model.Cnt=doc.DocumentNode.SelectSingleNode("//*[@id=\"js_content\"]").InnerHtml;

            if (doc.DocumentNode.SelectSingleNode("//*[@id=\"js_name\"]") !=null)
            {
                model.Author=doc.DocumentNode.SelectSingleNode("//*[@id=\"js_name\"]").InnerText;
            }else if (doc.DocumentNode.SelectSingleNode("//*[@id=\"meta_content\"]/span[1]") != null)
            {
                model.Author=doc.DocumentNode.SelectSingleNode("//*[@id=\"meta_content\"]/span[1]").InnerText;
            }

            model.Title = model.Title.Trim('\n').Trim();
            model.Author = model.Author.Trim('\n').Trim();
            // string pattern = @"\d{4}-\d{2}-\d{2} \d{2}:\d{2}";
            string pattern = @"createTime = '\d{4}-\d{2}-\d{2} \d{2}:\d{2}'";
            Match match = Regex.Match(html, pattern);
            if (match.Success)
            {
                var time = match.Value;
                time = time.Replace("createTime = '", "").Replace("'", "");

                if (time.Length == 15)
                {
                    time += ":01";
                }
                model.Time = Convert.ToDateTime(time);
            }
            else
            {
                model.Time = DateTime.Now;
            }
            string uid = (HttpContext.User.Identity?.Name ?? "").ToUpper().ToMd5();

            var match2 = Regex.Matches(model.Cnt, @"data-src\s*=\s*[""']([^""']+)[""']");
            foreach (Match item in match2)
            {
                if (item.Success && item.Groups.Count>1)
                {
                    var fileName = Guid.NewGuid().ToString("N")+".png";
                    string basePath = $"/assets/userfiles/{uid}/";
                    var url1 = item.Groups[0].Value;
                    var url2 = item.Groups[1].Value;
                    await url2.WithTimeout(TimeSpan.FromSeconds(10)).DownloadFileAsync(host.WebRootPath+basePath, fileName);
                    model.Cnt = model.Cnt.Replace(url1, $"src='{basePath + fileName}'");

                }
            }


            return model;
        }
        public async Task<InformationDto> Analyze(string url)
        {
            InformationDto model = new InformationDto();
            var html = await url.WithTimeout(TimeSpan.FromSeconds(10)).GetStringAsync();
            if (html==null)
            {
                return new InformationDto(){Title = "出错了"};
            }
            // 创建 HtmlDocument 对象
            var doc = new HtmlDocument();

            // 加载 HTML 字符串
            doc.LoadHtml(html);
            model.Url = url;
            if (doc.DocumentNode.SelectSingleNode("//*[@id=\"cb_post_title_url\"]/span") == null)
            {
                return new InformationDto(){Title = "出错了"};
            }
            model.Title=doc.DocumentNode.SelectSingleNode("//*[@id=\"cb_post_title_url\"]/span").InnerText;
            model.Cnt=doc.DocumentNode.SelectSingleNode("//*[@id=\"cnblogs_post_body\"]").InnerHtml;

            if (doc.DocumentNode.SelectSingleNode("//*[@id=\"topics\"]/div/div[3]/a[1]") !=null)
            {
                model.Author=doc.DocumentNode.SelectSingleNode("//*[@id=\"topics\"]/div/div[3]/a[1]").InnerText;
            }else if (doc.DocumentNode.SelectSingleNode("//*[@id=\"post_detail\"]/div[1]/div[5]/a[1]") != null)
            {
                model.Author=doc.DocumentNode.SelectSingleNode("//*[@id=\"post_detail\"]/div[1]/div[5]/a[1]").InnerText;
            }

           
            var time=doc.DocumentNode.SelectSingleNode("//*[@id=\"post-date\"]").InnerText;

            model.Time = Convert.ToDateTime(time);
            var imgList = doc.DocumentNode.SelectSingleNode("//*[@id=\"cnblogs_post_body\"]").SelectNodes("//img[@src]").ToList();
            
            string uid = (HttpContext.User.Identity?.Name ?? "").ToUpper().ToMd5();
            List<string> oldUrlList=new List<string>();
            List<string> imgUrls=new List<string>();
            foreach (var img in imgList)
            {
                var href2 = img.Attributes["src"].Value;
                var fileName = System.IO.Path.GetFileName(href2);
                var fileExt = System.IO.Path.GetExtension(href2).ToLower();

                imgUrls.Add(href2);
            }

            imgUrls = imgUrls.Where(m =>
                    !m.IsNullOrEmpty() &&
                    System.IO.Path.GetExtension(m).ToLower().IsIn(".jpg", ".jpeg", ".png", ".bmp"))
                .Distinct().ToList();
            if (imgUrls.Count>0)
            {
                foreach (var imgUrl in imgUrls)
                {
                    try
                    {
                        var fileExt = System.IO.Path.GetExtension(imgUrl).ToLower();
                        var fileName = Guid.NewGuid().ToString("N")+fileExt;
                        string basePath = $"/assets/userfiles/{uid}/";
                        await imgUrl.WithTimeout(TimeSpan.FromSeconds(10)).DownloadFileAsync(host.WebRootPath+basePath, fileName);
                        model.Cnt = model.Cnt.Replace(imgUrl, basePath + fileName);
                    }
                    catch
                    {
                    }
                }
            }


            return model;
        }

        #endregion
    }
    public class AnalysisDto
    {
        public string urls { get; set; }
        public int types { get; set; }
        public bool isAutoSave { get; set; }
        public bool isBatch { get; set; }
    }
}
