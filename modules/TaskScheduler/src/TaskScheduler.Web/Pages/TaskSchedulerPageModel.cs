using TaskScheduler.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace TaskScheduler.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class TaskSchedulerPageModel : AbpPageModel
{
    protected TaskSchedulerPageModel()
    {
        LocalizationResourceType = typeof(TaskSchedulerResource);
        ObjectMapperContext = typeof(TaskSchedulerWebModule);
    }
}
