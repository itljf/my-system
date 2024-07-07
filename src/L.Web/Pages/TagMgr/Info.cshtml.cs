using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using L.WInformations;
using L.WInfoTags;

namespace L.Web.Pages.TagMgr
{
    public class InfoModel(IInformationAppService informationAppService,IInfoTagAppService infoTagAppService)
        : AbpPageModel
    {
        public InfoTagDto tag { get; set; }
        public List<InfoTagDto> tagList { get; set; }

        public async Task OnGetAsync(long id)
        {
            tag = await infoTagAppService.GetAllInfoById(id);
            var tagIdList = tag.Informations.Select(m => m.TagItemIdList).SelectMany(m => m).ToList();
            tagList = await infoTagAppService.GetByIdList(tagIdList);

        }
    }
}
