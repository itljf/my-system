using System.Threading.Tasks;
using Azure;
using L.WInfoTags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace L.Web.Pages.NavigationMgr
{
    public class Edit1Model(IInfoTagAppService infoTagAppService)
        : AbpPageModel
    {
        [BindProperty]
        public InfoTagDto info { get; set; }
        public async Task OnGetAsync(long id)
        {
            if (id==0)
            {
                info = new InfoTagDto();
            }
            else
            {
                info = await infoTagAppService.GetById(id);
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            info.Code = InfoTagCode.navigation;

            var dto = ObjectMapper.Map<InfoTagDto, InfoTagEditDto>(info);
            if (dto.Id == 0)
            {
                await infoTagAppService.Create(dto);
            }
            else
            {
                await infoTagAppService.Edit(dto);
            }
            return NoContent();
        }
    }
}
