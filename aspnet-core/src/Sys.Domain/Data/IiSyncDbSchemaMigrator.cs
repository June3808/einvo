using System.Threading.Tasks;

namespace Sys.Data;

public interface ISysDbSchemaMigrator
{
    Task MigrateAsync();
}
