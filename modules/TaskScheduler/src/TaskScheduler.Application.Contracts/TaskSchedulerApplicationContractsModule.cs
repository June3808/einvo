﻿using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace TaskScheduler;

[DependsOn(
    typeof(TaskSchedulerDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class TaskSchedulerApplicationContractsModule : AbpModule
{

}
