using System.Collections.Generic;
using System.Threading.Tasks;
using L.WInfoTags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace L.Web.Pages.Todo
{
    public class IndexModel(IInfoTagAppService infoTagAppService) : LPageModel
    {
        public List<InfoTagDto> list { get; set; }
        public async Task OnGetAsync()
        {
            list = await infoTagAppService.GetAllTodo();
        }
    }
}
