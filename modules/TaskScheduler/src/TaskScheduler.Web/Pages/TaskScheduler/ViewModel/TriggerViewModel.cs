namespace TaskScheduler.Web.Pages.TaskScheduler.ViewModel
{
    public class TriggerViewModel
    {
        public SimpleTriggerViewModel Simple { get; set; } = new SimpleTriggerViewModel();
        public DailyTriggerViewModel Daily { get; set; } = new DailyTriggerViewModel();
        public CronTriggerViewModel Cron { get; set; } = new CronTriggerViewModel();

    }
}
