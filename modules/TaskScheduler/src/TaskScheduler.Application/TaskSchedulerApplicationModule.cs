using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.BackgroundJobs.Quartz;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Quartz;
using Volo.Abp;
using TaskScheduler.TaskSchedulers;
using Quartz;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz.Spi;
using System.ComponentModel;
using TaskScheduler.Listener;
using System.Collections.Specialized;
using System.Configuration;
//using TaskScheduler.Listener;

namespace TaskScheduler;

[DependsOn(
    typeof(TaskSchedulerDomainModule),
    typeof(TaskSchedulerApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpBackgroundJobsQuartzModule)
    )]
public class TaskSchedulerApplicationModule : AbpQuartzModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<TaskSchedulerApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<TaskSchedulerApplicationModule>(validate: true);
        });
    }

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var connectionString = context.Services.GetConfiguration().GetConnectionString("Default");

        context.Services.AddScoped<IJobListener, TaskJobListener>();

        //add custom quartz configuration here
        PreConfigure<AbpQuartzOptions>(options =>
        {
            options.Properties = new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = "Sys Scheduler",
                ["quartz.scheduler.instanceId"] = "AUTO",
                ["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz",
                ["quartz.threadPool.threadCount"] = "15",
                ["quartz.jobStore.misfireThreshold"] = "60000",
                //["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",
                //["quartz.jobStore.useProperties"] = "false",
                //["quartz.jobStore.dataSource"] = "default",
                //["quartz.jobStore.tablePrefix"] = "DOT_QRTZ_",
                //["quartz.jobStore.clustered"] = "true",
                //["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
                //["quartz.dataSource.default.connectionString"] = "Server=MSI-Amir; Database=Sys;User Id=sadmin;password=123qwe;TrustServerCertificate=true;",//_appConfiguration["ConnectionStrings:Default"],
                //["quartz.dataSource.default.provider"] = "SqlServer",
                //["quartz.serializer.type"] = "json"
            };
            options.Configurator = config =>
            {
                config.AddJobListener<TaskJobListener>();
            };
        });
    }

    public async override Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var taskScheduleDomainService = context.ServiceProvider.GetRequiredService<ITaskSchedulersAppService>();
        var liste = context.ServiceProvider.GetRequiredService<IJobListener>();

        await taskScheduleDomainService.ScheduleAllJobs().ContinueWith(t =>
        {
            var _scheduler = context.ServiceProvider.GetRequiredService<IScheduler>();
            _scheduler.Start();
        });
    }
}
