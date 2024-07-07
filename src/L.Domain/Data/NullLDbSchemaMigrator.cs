using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace L.Data;

/* This is used if database provider does't define
 * ILDbSchemaMigrator implementation.
 */
public class NullLDbSchemaMigrator : ILDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
