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
	        return Content("�����post����Ľ��1");
        }
        public IActionResult OnPostTest2([FromBody]int id,[FromBody]string name)
        {
	        return Content("�����post����Ľ��2");
        }
        
        public IActionResult OnPut()
        {
	        return Content("�����put����Ľ��");
        }
    }
}
