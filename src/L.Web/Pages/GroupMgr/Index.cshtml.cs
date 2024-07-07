using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using L.WInfoGroups;
using L.WInfoTags;

namespace L.Web.Pages.GroupMgr
{
    public class IndexModel(IInfoGroupAppService infoGroupAppService)
        : AbpPageModel
    {
        public PagedList<InfoGroupDto> list{ get; set; }
        public PagerModel PagerModel { get; set; }

        public async Task OnGetAsync( GetInfoGroupInput input)
        {
            list =await infoGroupAppService.GetPagedListAsync(input);
            PagerModel = new PagerModel(list.TotalItemCount, list.Count, list.CurrentPageIndex, list.PageSize, "/TagMgr/Index");
        }
    }
}
