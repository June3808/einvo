using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace TaskScheduler;

[DependsOn(
    typeof(TaskSchedulerApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class TaskSchedulerHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(TaskSchedulerApplicationContractsModule).Assembly,
            TaskSchedulerRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TaskSchedulerHttpApiClientModule>();
        });

    }
}
