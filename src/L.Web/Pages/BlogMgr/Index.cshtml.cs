using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using L.WInformations;
using L.WInfoTags;
using L.Web.Common;

namespace L.Web.Pages.BlogMgr
{
    public class IndexModel(
        IInformationAppService informationAppService,IInfoTagAppService infoTagAppService)
        : AbpPageModel
    {
        public PagedList<WInformations.InformationDto> list{ get; set; }
        public PagerModel PagerModel { get; set; }
        public GetInformationInput input { get; set; }
        public List<InfoTagDto> tagInfoList { get; set; }

        public async Task OnGetAsync([FromQuery]GetInformationInput _input)
        {
            input = _input;
            list = await informationAppService.GetPagedList(input);
            PagerModel = new PagerModel(list.TotalItemCount, list.Count, list.CurrentPageIndex, list.PageSize, "/BlogMgr/Index?"+HttpContext.GetQueryStr());
            tagInfoList = await infoTagAppService.GetListByCode(InfoTagCode.blog);
        }
    }
}
