using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace TaskScheduler;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(TaskSchedulerDomainSharedModule)
)]
public class TaskSchedulerDomainModule : AbpModule
{

}
