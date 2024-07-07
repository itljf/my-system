using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace L.Web.Themes
{
    public class UserInfoViewComponent : AbpViewComponent
    {
        public virtual IViewComponentResult Invoke()
        {
            return View("~/Themes/Basic/Components/Web/UserInfo.cshtml");
        }
    }
}
