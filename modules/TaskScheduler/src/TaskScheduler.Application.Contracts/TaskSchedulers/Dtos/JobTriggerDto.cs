using System;
using System.Collections.Generic;
using System.Text;

namespace TaskScheduler.TaskSchedulers.Dtos
{
    public class JobTriggerDto
    {
        public int TriggerType { get; set; }

        public int IntervalMinutes { get; set; }

        public int IntervalSeconds { get; set; }

        public int? RunTimes { get; set; }

        public bool RepeatForever { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public string CronExpression { get; set; }

        public DaysOfWeekViewModel DaysOfWeek { get; set; } = new DaysOfWeekViewModel();
    }
}
