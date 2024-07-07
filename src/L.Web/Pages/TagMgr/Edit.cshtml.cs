using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using L.WInfoTags;

namespace L.Web.Pages.TagMgr
{
    public class EditModel(IInfoTagAppService infoTagAppService)
        : AbpPageModel
    {
        [BindProperty]
        public InfoTagDto tag { get; set; }
        public async Task OnGetAsync(long id)
        {
            if (id == 0)
            {
                tag= new InfoTagDto();
            }
            else
            {
                tag = await infoTagAppService.GetById(id);
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            tag.Code = InfoTagCode.blog;
            tag.Files = "";
            tag.Url = "";

            var dto = ObjectMapper.Map<InfoTagDto, InfoTagEditDto>(tag);
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
