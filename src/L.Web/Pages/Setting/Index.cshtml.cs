using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.SettingManagement;

namespace L.Web.Pages.Setting
{
    public class IndexModel(ISettingManager settingManager)
        : AbpPageModel
    {
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            // ��ȡ POST �����͵ı�����
            var formValues = Request.Form;
            // �������е��ֶ����Ͷ�Ӧ��ֵ
            foreach (var key in formValues.Keys)
            {
                string name = key;
                string value = formValues[key];
                if (name.StartsWith("L."))
                {
                    await settingManager.SetGlobalAsync(name, value);
                }
            }
            Alerts.Success("����ɹ���","�����Ǳ���");
            return Page();

            
        }
    }
}
