using System;
using L.Common;
using L.WInformations;
using L.WInfoTags;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace L.Web.Pages.BlogMgr
{
    public class EditModel(IInformationAppService informationAppService,IInfoTagAppService infoTagAppService)
        : AbpPageModel
    {

        [BindProperty]
        public InformationDto InformationDto { get; set; }
        public List<InfoTagDto> tagInfoList { get; set; }

        
        public async Task OnGetAsync(long id,int types=0)
        {
            tagInfoList = await infoTagAppService.GetListByCode(InfoTagCode.blog);
            if (id == 0)
            {
                InformationDto= new InformationDto();
                InformationDto.EditorType = (EditorType)types;
                InformationDto.Time = DateTime.Now;
            }
            else
            {
                InformationDto= await informationAppService.Get(id);
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            if (InformationDto.EditorType == EditorType.Markdown)
            {
                InformationDto.Cnt = MDHelper.ToHtml(InformationDto.Markdown);
            }
            var dto = ObjectMapper.Map<InformationDto, InformationEditDto>(InformationDto);
            if (InformationDto.Id == 0)
            {
                await informationAppService.Create(dto);
            }
            else
            {
                await informationAppService.Update(InformationDto.Id,dto);
            }
            return RedirectToPage("./Index");
        }
    }
}
