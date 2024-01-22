using System;
using Volo.Abp.Application.Dtos;

namespace TaskScheduler.TaskSchedulers.Dtos
{
    [Serializable]
    public class ScheduleJobDto : EntityDto<Guid>
    {
        public virtual string JobName { get; set; }

        public virtual string JobGroup { get; set; }

        public virtual JobStatus JobStatus { get; set; }

        public virtual JobRunStatus JobRunStatus { get; set; }

        public virtual DateTime LastExecuteTime { get; set; }

        public virtual int MaximumRetries { get; set; }

        public virtual int IntervalMinutes { get; set; }

        public virtual int IntervalSeconds { get; set; }

        public virtual int RunTimes { get; set; }

        public virtual DateTime StartDateTime { get; set; }

        public virtual DateTime? EndDateTime { get; set; }

        public virtual string Cron { get; set; } = string.Empty;

        public virtual string CronRemark { get; set; } = string.Empty;

        public virtual string AssemblyName { get; set; } = string.Empty;

        public virtual string ClassName { get; set; } = string.Empty;

        public TriggerType TriggerType { get; set; }

        public virtual string TriggerId { get; set; } = string.Empty;

        public virtual DateTime NextRunTime { get; set; }

        public virtual string JsonParamters { get; set; } = string.Empty;

        public virtual int? MisfireInstruction { get; set; }

        public virtual int Priority { get; set; }

        public virtual bool RepeatForever { get; set; }

        public virtual string SelectedDaysOfWeek { get; set; } = string.Empty;

        public virtual TimeSpan? StartTime { get; set; }

        public virtual TimeSpan? EndTime { get; set; }

        //public virtual JobCategory JobCategory { get; set; }

        public virtual int UIBatchSeq { get; set; }

        public virtual bool IsAdhotJob { get; set; }
    }
}
