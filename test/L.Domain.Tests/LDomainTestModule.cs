using Volo.Abp.Modularity;

namespace L;

[DependsOn(
    typeof(LDomainModule),
    typeof(LTestBaseModule)
)]
public class LDomainTestModule : AbpModule
{

}
