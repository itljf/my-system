using L.WInfoTags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace L.Web.Pages.NavigationMgr
{
    public class IndexModel(IInfoTagAppService infoTagAppService)
        : AbpPageModel
    {
        public List<InfoTagDto> NavList { get; set; }
        public long SelectId { get; set; }
        public async Task OnGetAsync(long id=0)
        {
            NavList = await infoTagAppService.GetNavigation();
            if (id == 0 && NavList.Count>0)
            {
                id = NavList.FirstOrDefault(m => m.Fid == 0)?.Id ?? 0;
            }
            SelectId = id;
        }
    }
}
