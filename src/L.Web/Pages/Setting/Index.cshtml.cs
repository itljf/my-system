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
            // 获取 POST 请求发送的表单数据
            var formValues = Request.Form;
            // 遍历所有的字段名和对应的值
            foreach (var key in formValues.Keys)
            {
                string name = key;
                string value = formValues[key];
                if (name.StartsWith("L."))
                {
                    await settingManager.SetGlobalAsync(name, value);
                }
            }
            Alerts.Success("保存成功！","这里是标题");
            return Page();

            
        }
    }
}
