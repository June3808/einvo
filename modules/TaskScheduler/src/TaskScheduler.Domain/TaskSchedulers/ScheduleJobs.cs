using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace TaskScheduler
{
    public class ScheduleJobs : FullAuditedEntity<Guid>
    {
        public ScheduleJobs()
        {
        }

        public ScheduleJobs(string jobName, string jobGroup, JobStatus jobStatus, JobRunStatus jobRunStatus, DateTime lastExecuteTime, int maximumRetries, int intervalMinutes, int intervalSeconds, int runTimes, DateTime startDateTime, DateTime? endDateTime, string cron, string cronRemark, string assemblyName, string className, TriggerType triggerType, string triggerId, DateTime nextRunTime, string jsonParamters, int? misfireInstruction, int priority, bool repeatForever, string selectedDaysOfWeek, TimeSpan? startTime, TimeSpan? endTime, int uIBatchSeq, bool isAdhotJob)
        {
            JobName = jobName;
            JobGroup = jobGroup;
            JobStatus = jobStatus;
            JobRunStatus = jobRunStatus;
            LastExecuteTime = lastExecuteTime;
            MaximumRetries = maximumRetries;
            IntervalMinutes = intervalMinutes;
            IntervalSeconds = intervalSeconds;
            RunTimes = runTimes;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Cron = cron;
            CronRemark = cronRemark;
            AssemblyName = assemblyName;
            ClassName = className;
            TriggerType = triggerType;
            TriggerId = triggerId;
            NextRunTime = nextRunTime;
            JsonParamters = jsonParamters;
            MisfireInstruction = misfireInstruction;
            Priority = priority;
            RepeatForever = repeatForever;
            SelectedDaysOfWeek = selectedDaysOfWeek;
            StartTime = startTime;
            EndTime = endTime;
            UIBatchSeq = uIBatchSeq;
            IsAdhotJob = isAdhotJob;
        }

        /// <summary>
        /// Job Decsription
        /// </summary>
        [Required]
        public virtual string JobName { get; set; }

        /// <summary>
        /// Job Group, corresponding to the Job Class Name
        /// </summary>
        public virtual string JobGroup { get; set; }
        /// <summary>
        /// Job Status: On Hold ; Ready 
        /// </summary>
        public virtual JobStatus JobStatus { get; set; }
        /// <summary>
        /// Job running status
        /// </summary>
        public virtual JobRunStatus JobRunStatus { get; set; }

        public virtual DateTime LastExecuteTime { get; set; }
        /// <summary>
        /// Maximum retry times inside the job
        /// </summary>
        public virtual int MaximumRetries { get; set; }
        /// <summary>
        /// Execution interval, by minutes
        /// </summary>
        public virtual int IntervalMinutes { get; set; }
        /// <summary>
        /// Execution interval, by seconds
        /// </summary>
        public virtual int IntervalSeconds { get; set; }
        /// <summary>
        /// Limit the job max repeat run times
        /// </summary>
        public virtual int RunTimes { get; set; }

        public virtual DateTime StartDateTime { get; set; }

        public virtual DateTime? EndDateTime { get; set; }

        public virtual string? Cron { get; set; } = string.Empty;

        public virtual string? CronRemark { get; set; } = string.Empty;
        /// <summary>
        /// The name of the assembly corresponding to the DLL where the task is located
        /// </summary>
        public virtual string? AssemblyName { get; set; }

        public virtual string? ClassName { get; set; } = string.Empty;
        /// <summary>
        /// Trigger Type£¨0¡¢simple 1¡¢cron£©
        /// </summary>
        public TriggerType TriggerType { get; set; }
        /// <summary>
        /// Trigger Instance Id
        /// </summary>
        [MaxLength(100)]
        public virtual string TriggerId { get; set; } = string.Empty;

        public virtual DateTime NextRunTime { get; set; }


        public virtual string? JsonParamters { get; set; } = string.Empty;

        ///// <summary>
        ///// Job runing history records.
        ///// </summary>
        //public virtual ICollection<JobHistory> JobHistories { get; set; }

        public virtual int? MisfireInstruction { get; set; }
        public virtual int Priority { get; set; }
        public virtual bool RepeatForever { get; set; }

        [MaxLength(20)]
        public virtual string? SelectedDaysOfWeek { get; set; } = string.Empty;

        public virtual TimeSpan? StartTime { get; set; }

        public virtual TimeSpan? EndTime { get; set; }

        //public virtual JobCategory JobCategory { get; set; }

        public virtual int UIBatchSeq { get; set; }

        public virtual bool IsAdhotJob { get; set; }
    }


}
