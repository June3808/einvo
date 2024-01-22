using TaskScheduler.Localization;
using Volo.Abp.Application.Services;

namespace TaskScheduler;

public abstract class TaskSchedulerAppService : ApplicationService
{
    protected TaskSchedulerAppService()
    {
        LocalizationResource = typeof(TaskSchedulerResource);
        ObjectMapperContext = typeof(TaskSchedulerApplicationModule);
    }
}
