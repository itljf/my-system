using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using L.WInfoGroups;
using L.WInformations;
using L.WInfoTags;

namespace L.Web.Pages.GroupMgr
{
    public class SearchModel(IInfoGroupAppService infoGroupAppService,IInfoTagAppService infoTagAppService,IInformationAppService informationAppService)
        : AbpPageModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public InfoGroupDto info { get; set; }
        public List<InformationDto> informations { get; set; }

        // public string name { get; set;}

        public async Task<IActionResult> OnGetAsync(long id,string name)
        {
            informations = await informationAppService.GetListByTitle(name);
            //获取已经选中的信息
            info = await infoGroupAppService.GetAllInfoById(id);
            // this.name=name;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string name)
        {
            informations = await informationAppService.GetListByTitle(name);
            return new JsonResult(informations);
        }
    }
}
