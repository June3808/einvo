using Localization.Resources.AbpUi;
using TaskScheduler.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace TaskScheduler;

[DependsOn(
    typeof(TaskSchedulerApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class TaskSchedulerHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(TaskSchedulerHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<TaskSchedulerResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
