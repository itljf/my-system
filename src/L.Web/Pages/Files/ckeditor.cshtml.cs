using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;

namespace WP.Web.Pages.Files
{
    public class ckeditorModel(IHostingEnvironment host) : PageModel
    {
        private List<string> allowFileExt = new List<string> { "7z", "bmp", "csv", "doc", "docx",  "gif", "jpeg", "jpg", "mp3", "mp4", "pdf", "png", "ppt", "pptx", "rar", "xls", "xlsx", "zip" };

        public IActionResult OnGet()
        {
            return NotFound();
        }
        [AbpValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            string uid = (HttpContext.User.Identity?.Name ?? "").ToUpper().ToMd5();
            string strchunk = Request.Query["chunk"];
            string strchunks = Request.Query["chunks"];

            var data = Request.Form.Files;

            int chunk = 0;
            int chunks = 0;
            int.TryParse(strchunk, out chunk);
            int.TryParse(strchunks, out chunks);
            if (Request.Form.Files.Count==0)
            {
                return new JsonResult(new {
                    error=new
                    {
                        message="未找到文件"
                    }
                });
            }

            var upload = Request.Form.Files[0];
            var name=upload.FileName;
            if (!allowFileExt.Contains(Path.GetExtension(name).ToLower().Replace(".", string.Empty)))
            {
                return new JsonResult(new {
                    error=new
                    {
                        message="不被允许文件格式"
                    }
                });
            }
            // name = SafeSearchStrFileName(name, "_");
            string basePath = $"/assets/userfiles/{uid}/";
            name = Guid.NewGuid().ToString("N") + Path.GetExtension(name);
            string url = host.WebRootPath+basePath;
            string filePath = url+name;
            if (!Directory.Exists(url))
            {
                Directory.CreateDirectory(url);
            }
                
            using (var stream = System.IO.File.Create(filePath))
            {
                await upload.CopyToAsync(stream);
            }
            return new JsonResult(new {
                url=basePath+name
            });
        }
    }
}
