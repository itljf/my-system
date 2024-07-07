using System.Threading.Tasks;
using L.WInfoTags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace L.Web.Pages.Todo
{
    public class EditModel(IInfoTagAppService infoTagAppService)
        : LPageModel
    {
        [BindProperty]
        public InfoTagDto info { get; set; }
        public async Task OnGetAsync(long id)
        {
            if (id == 0)
            {
                info= new InfoTagDto();
            }
            else
            {
                info = await infoTagAppService.GetById(id);
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            info.Code = InfoTagCode.todo;

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
