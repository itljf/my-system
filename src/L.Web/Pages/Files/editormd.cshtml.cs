using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WP.Web.Pages.Files
{
    public class editormdModel(IHostingEnvironment host) : PageModel
    {
        private List<string> allowFileExt = new List<string> { "7z", "bmp", "csv", "doc", "docx",  "gif", "jpeg", "jpg", "mp3", "mp4", "pdf", "png", "ppt", "pptx", "rar", "xls", "xlsx", "zip" };
        public void OnGet()
        {
        }

        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> OnPostAsync(IFormFile file,string folder)
        {
            string fileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
            EditorImg img=new EditorImg();
            
            string uid = (HttpContext.User.Identity?.Name ?? "").ToUpper().ToMd5();
            
            string basePath = $"/assets/userfiles/{uid}/";
            var name=file.FileName;
            if (!allowFileExt.Contains(Path.GetExtension(name).ToLower().Replace(".", string.Empty)))
            {
                img.success = 0;
                img.message = "���������ļ���ʽ";
                return new JsonResult(img);
            }
            name = Guid.NewGuid().ToString("N") + Path.GetExtension(name);
            string url = host.WebRootPath+basePath;
            string filePath = url+name;
            if (!Directory.Exists(url))
            {
                Directory.CreateDirectory(url);
            }
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            img.success = 1;
            img.message = "�ɹ��ϴ��ļ�";
            img.url= basePath+name;
            return new JsonResult(img);
        }
    }
    
    public class EditorImg
    {
        /// <summary>
        /// success : 0 | 1,     0 ��ʾ�ϴ�ʧ�ܣ�1 ��ʾ�ϴ��ɹ�
        /// </summary>
        public virtual int success { get; set; }
        /// <summary>
        /// "��ʾ����Ϣ���ϴ��ɹ����ϴ�ʧ�ܼ�������Ϣ�ȡ�"
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// "ͼƬ��ַ"         �ϴ��ɹ�ʱ�ŷ���
        /// </summary>
        public string url { get; set; }
    }
}
