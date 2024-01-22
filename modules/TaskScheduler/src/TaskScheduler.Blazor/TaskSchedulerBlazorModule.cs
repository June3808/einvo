using Microsoft.Extensions.DependencyInjection;
using TaskScheduler.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace TaskScheduler.Blazor;

[DependsOn(
    typeof(TaskSchedulerApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
public class TaskSchedulerBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<TaskSchedulerBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<TaskSchedulerBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new TaskSchedulerMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(TaskSchedulerBlazorModule).Assembly);
        });
    }
}
