using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using L.WInfoGroups;
using L.WInfoTags;

namespace L.Web.Pages.GroupMgr
{
    public class InfoModel(IInfoGroupAppService infoGroupAppService,IInfoTagAppService infoTagAppService)
        : AbpPageModel
    {
        /// <summary>
        /// БъЬт
        /// </summary>
        [BindProperty]
        public InfoGroupDto info { get; set; }
        public List<InfoTagDto> tagList { get; set; }


        public async Task OnGetAsync(long id)
        {
            info = await infoGroupAppService.GetAllInfoById(id);
            var tagIdList = info.Informations.Select(m => m.TagItemIdList).SelectMany(m => m).ToList();
            tagList = await infoTagAppService.GetByIdList(tagIdList);
        }
    }
}
