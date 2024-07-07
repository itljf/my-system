using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using L.WInfoTags;

namespace L.Web.Pages.TagMgr
{
    public class IndexModel(IInfoTagAppService infoTagAppService)
        : AbpPageModel
    {
        public PagedList<InfoTagDto> list{ get; set; }
        public PagerModel PagerModel { get; set; }

        public async Task OnGetAsync( GetInfoTagInput input)
        {
            input.Code = InfoTagCode.blog;
            list =await infoTagAppService.GetPagedList(input);
            PagerModel = new PagerModel(list.TotalItemCount, list.Count, list.CurrentPageIndex, list.PageSize, "/TagMgr/Index");
        }

    }
}
