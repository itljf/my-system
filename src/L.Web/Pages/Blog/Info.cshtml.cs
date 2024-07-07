using System.Collections.Generic;
using System.Threading.Tasks;
using L.WInformations;
using L.WInfoTags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace L.Web.Pages.Blog
{
    public class InfoModel(
        IInformationAppService informationAppService,IInfoTagAppService infoTagAppService)
        : AbpPageModel
    {
        /// <summary>
        /// БъЬт
        /// </summary>
        [BindProperty]
        public InformationDto info { get; set; }

        public IEnumerable<InfoTagDto> InfoTags { get; set; }

        public async Task OnGetAsync(long id)
        {
            info = await informationAppService.Get(id);
            InfoTags = await infoTagAppService.GetByIdList(info.TagItemIdList);
        }
    }
}
