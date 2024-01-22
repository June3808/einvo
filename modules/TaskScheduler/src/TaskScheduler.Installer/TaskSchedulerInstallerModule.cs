using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace TaskScheduler;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class TaskSchedulerInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TaskSchedulerInstallerModule>();
        });
    }
}
