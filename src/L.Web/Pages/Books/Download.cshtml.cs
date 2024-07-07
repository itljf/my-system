using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Http;

namespace L.Web.Pages.Books
{
    public class DownloadModel : PageModel
    {
        public IActionResult OnGet()
        {
	        return File("~/files/Test.mp4", MimeTypes.Audio.Mp4);
        }
    }
}
