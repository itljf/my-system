using System;
using System.Collections.Generic;
using System.Text;
using L.Localization;
using Volo.Abp.Application.Services;

namespace L;

/* Inherit your application services from this class.
 */
public abstract class LAppService : ApplicationService
{
    protected LAppService()
    {
        LocalizationResource = typeof(LResource);
    }
}
