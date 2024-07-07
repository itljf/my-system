using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace L.Web.Pages.Books
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPostTest1([FromBody]int id)
        {
	        return Content("这个是post请求的结果1");
        }
        public IActionResult OnPostTest2([FromBody]int id,[FromBody]string name)
        {
	        return Content("这个是post请求的结果2");
        }
        
        public IActionResult OnPut()
        {
	        return Content("这个是put请求的结果");
        }
    }
}
