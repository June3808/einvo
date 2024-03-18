using Quartz;
using System.ComponentModel.DataAnnotations;

namespace TaskScheduler.Web.Pages.TaskScheduler.ViewModel
{
    public class CronTriggerViewModel
    {
        public string? CronExpression { get; set; }

        public string? CronRemark { get; set; }
        public static CronTriggerViewModel FromTrigger(ICronTrigger trigger)
        {
            return new CronTriggerViewModel()
            {
                CronExpression = trigger.CronExpressionString,
            };
        }
    }
}
