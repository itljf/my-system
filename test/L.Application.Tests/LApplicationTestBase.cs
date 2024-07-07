using Volo.Abp.Modularity;

namespace L;

public abstract class LApplicationTestBase<TStartupModule> : LTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
