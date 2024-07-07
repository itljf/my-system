using Volo.Abp.Modularity;

namespace L;

/* Inherit from this class for your domain layer tests. */
public abstract class LDomainTestBase<TStartupModule> : LTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
