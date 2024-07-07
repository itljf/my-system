using System.Threading.Tasks;

namespace L.Data;

public interface ILDbSchemaMigrator
{
    Task MigrateAsync();
}
