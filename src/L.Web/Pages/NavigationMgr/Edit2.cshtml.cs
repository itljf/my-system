using L.WInfoTags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace L.Web.Pages.NavigationMgr
{
    public class Edit2Model(IInfoTagAppService infoTagAppService)
        : AbpPageModel
    {
        [BindProperty]
        public InfoTagDto info { get; set; }
        public async Task OnGetAsync(long id,int fid)
        {
            if (id==0)
            {
                info = new InfoTagDto();
                info.Fid = fid;
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
