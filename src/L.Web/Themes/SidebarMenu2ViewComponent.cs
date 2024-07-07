using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.UI.Navigation;

namespace L.Web.Themes
{
    public class SidebarMenu2ViewComponent : AbpViewComponent
    {
	    protected IMenuManager _menuManager { get; }
	    
	    public SidebarMenu2ViewComponent(IMenuManager menuManager)
	    {
            _menuManager = menuManager;
	    }
        public virtual async Task<IViewComponentResult> InvokeAsync()
        {
	        var menu = await _menuManager.GetMainMenuAsync();

            return View("~/Themes/Basic/Components/Web/SidebarMenu.cshtml",menu);
        }
    }
}
