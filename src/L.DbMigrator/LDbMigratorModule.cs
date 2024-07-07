using L.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace L.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(LEntityFrameworkCoreModule),
    typeof(LApplicationContractsModule)
    )]
public class LDbMigratorModule : AbpModule
{
}
