using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L.Web.Common;
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
            if (_input.Name.IsNullOrWhiteSpace())
            {
                list = await informationAppService.GetPagedList(input);
            }
            else
            {
                list = await informationAppService.SearchByLucene(input);
            }
            PagerModel = new PagerModel(list.TotalItemCount, list.Count, list.CurrentPageIndex, list.PageSize, "/Blog/Index?"+HttpContext.GetQueryStr());
            

            tagList = await infoTagAppService.GetListByCode(InfoTagCode.blog);
        }

        
        public async Task<IActionResult> OnPostDeleteAsync(long id)
        {
            await informationAppService.Delete(id);

            return RedirectToPage();
        }


    }
}
