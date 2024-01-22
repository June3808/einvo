using Quartz;
using System;
using TaskScheduler.TaskSchedulers.Dtos;

namespace TaskScheduler.Web.Pages.TaskScheduler.ViewModel
{
    public class DailyTriggerViewModel
    {
        public DaysOfWeekViewModel DaysOfWeek { get; set; } = new DaysOfWeekViewModel();

        public int IntervalSeconds { get; set; }

        public int IntervalMinutes { get; set; }

        public int? RunTimes { get; set; }

        public bool RepeatForever { get; set; }

        public TimeSpan? StartTime { get; set; }


        public TimeSpan? EndTime { get; set; }


        public static DailyTriggerViewModel FromTrigger(IDailyTimeIntervalTrigger trigger)
        {
            var model = new DailyTriggerViewModel()
            {
                RunTimes = trigger.RepeatCount,
                StartTime = TimeSpan.FromSeconds(trigger.StartTimeOfDay.Second + trigger.StartTimeOfDay.Minute * 60 + trigger.StartTimeOfDay.Hour * 3600),
                EndTime = TimeSpan.FromSeconds(trigger.EndTimeOfDay.Second + trigger.EndTimeOfDay.Minute * 60 + trigger.EndTimeOfDay.Hour * 3600),
                DaysOfWeek = DaysOfWeekViewModel.Create(trigger.DaysOfWeek),
            };

            if (model.RunTimes == -1)
            {
                model.RunTimes = null;
                model.RepeatForever = true;
            }

            return model;
        }
    }
}
