using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using L.WInformations;
using L.WInfoTags;

namespace L.Web.Pages.Blog
{
    public class IndexModel(
        IInformationAppService informationAppService,IInfoTagAppService infoTagAppService)
        : AbpPageModel
    {
        public PagedList<WInformations.InformationDto> list;
        public List<InfoTagDto> tagList;
        public PagerModel PagerModel { get; set; }
        public GetInformationInput input { get; set; }

        public async Task OnGetAsync([FromQuery]GetInformationInput _input)
        {
            input = _input;
            input.PageSize = 10;
            list = await informationAppService.GetPagedList(input);
            var request = HttpContext.Request;
            var queryCollection = request.Query;

            string queryParams = string.Join("&", queryCollection.Keys.Where(m=>!m.Equals("currentPage",StringComparison.InvariantCultureIgnoreCase)).Select(k => $"{k}={queryCollection[k]}"));
            PagerModel = new PagerModel(list.TotalItemCount, list.Count, list.CurrentPageIndex, list.PageSize, "/Blog/Index?"+queryParams);
            

            tagList = await infoTagAppService.GetListByCode(InfoTagCode.blog);
        }

        
        public async Task<IActionResult> OnPostDeleteAsync(long id)
        {
            await informationAppService.Delete(id);

            return RedirectToPage();
        }


    }
}
