using Quartz;

namespace TaskScheduler.Web.Pages.TaskScheduler.ViewModel
{
    public class CronTriggerViewModel
    {
        public string CronExpression { get; set; } = string.Empty;
        public string CronRemark { get; set; } = string.Empty;
        public static CronTriggerViewModel FromTrigger(ICronTrigger trigger)
        {
            return new CronTriggerViewModel()
            {
                CronExpression = trigger.CronExpressionString,
            };
        }
    }
}
