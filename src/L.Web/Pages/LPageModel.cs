using L.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace L.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class LPageModel : AbpPageModel
{
    protected LPageModel()
    {
        LocalizationResourceType = typeof(LResource);
    }
}
