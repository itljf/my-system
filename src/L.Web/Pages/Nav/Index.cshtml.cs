using L.WInfoTags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace L.Web.Pages.Nav
{
    public class IndexModel(IInfoTagAppService infoTagAppService)
        : AbpPageModel
    {
        public List<InfoTagDto> NavList { get; set; }
        public async Task OnGetAsync()
        {
            NavList = await infoTagAppService.GetNavigation();
        }
    }
}
