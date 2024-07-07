using L.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace L.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class LController : AbpControllerBase
{
    protected LController()
    {
        LocalizationResource = typeof(LResource);
    }
}
