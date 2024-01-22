using Quartz.Impl.Triggers;
using Quartz;

namespace TaskScheduler.Web.Pages.TaskScheduler.ViewModel
{
    public class SimpleTriggerViewModel
    {
        public int IntervalMinutes { get; set; }

        public int IntervalSeconds { get; set; }

        public int? RunTimes { get; set; }

        public bool RepeatForever { get; set; }

        public static SimpleTriggerViewModel FromTrigger(ISimpleTrigger trigger)
        {
            var model = new SimpleTriggerViewModel()
            {
                RunTimes = trigger.RepeatCount,
                RepeatForever = trigger.RepeatCount == SimpleTriggerImpl.RepeatIndefinitely,
            };

            if (model.RunTimes == -1)
                model.RunTimes = null;

            return model;
        }
    }
}
