using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using L.WInfoGroups;
using L.WInfoTags;

namespace L.Web.Pages.GroupMgr
{
    public class EditModel(IInfoGroupAppService infoGroupAppService)
        : LPageModel
    {
        [BindProperty]
        public InfoGroupDto info { get; set; }
        public async Task OnGetAsync(long id)
        {
            if (id == 0)
            {
                info= new InfoGroupDto();
            }
            else
            {
                info = await infoGroupAppService.GetById(id);
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            // info.Author

            var dto = ObjectMapper.Map<InfoGroupDto, InfoGroupEditDto>(info);
            if (dto.Id == 0)
            {
                await infoGroupAppService.Create(dto);
            }
            else
            {
                await infoGroupAppService.Edit(dto);
            }
            return NoContent();
        }
    }
}
