using TaskScheduler.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TaskScheduler;

public abstract class TaskSchedulerController : AbpControllerBase
{
    protected TaskSchedulerController()
    {
        LocalizationResource = typeof(TaskSchedulerResource);
    }
}
