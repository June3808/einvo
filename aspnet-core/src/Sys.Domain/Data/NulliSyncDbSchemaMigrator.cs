using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Sys.Data;

/* This is used if database provider does't define
 * ISysDbSchemaMigrator implementation.
 */
public class NullSysDbSchemaMigrator : ISysDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
