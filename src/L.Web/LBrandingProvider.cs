using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace L.Web;

[Dependency(ReplaceServices = true)]
public class LBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "L";
}
