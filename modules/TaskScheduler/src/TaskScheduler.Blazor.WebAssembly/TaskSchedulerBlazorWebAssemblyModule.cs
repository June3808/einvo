using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace TaskScheduler.Blazor.WebAssembly;

[DependsOn(
    typeof(TaskSchedulerBlazorModule),
    typeof(TaskSchedulerHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class TaskSchedulerBlazorWebAssemblyModule : AbpModule
{

}
