using Volo.Abp.Modularity;

namespace L;

[DependsOn(
    typeof(LApplicationModule),
    typeof(LDomainTestModule)
)]
public class LApplicationTestModule : AbpModule
{

}
