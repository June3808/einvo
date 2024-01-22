using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace TaskScheduler.TaskSchedulers.Dtos
{
    [Serializable]
    public class CreateUpdateScheduleJobDto : FullAuditedEntityDto<Guid?>
    {
        public CreateUpdateScheduleJobDto()
        {
            this.JobGroupNames = new List<ComboboxItemDto>();
            //this.StartDateTime = DateTime.Now;

            this.Trigger = new JobTriggerDto();
        }

        public string JobName { get; set; } = string.Empty;

        public string JobGroup { get; set; } = string.Empty;

        public List<ComboboxItemDto> JobGroupNames { get; set; }

        public JobStatus JobStatus { get; set; }

        public JobRunStatus JobRunStatus { get; set; }
        /// <summary>
        /// Maximum retry times inside the job
        /// </summary>
        public int MaximumRetries { get; set; }
        /// <summary>
        /// Execution interval, by Minutes
        /// </summary>
        public int IntervalMinutes { get; set; }
        /// <summary>
        /// Execution interval, by Seconds
        /// </summary>
        public int IntervalSeconds { get; set; }
        /// <summary>
        /// Limit the job max repeat run times
        /// </summary>
        public int RunTimes { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDateTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDateTime { get; set; }

        //advanced features
        public TriggerType TriggerType { get; set; }
        public string Cron { get; set; }

        public string CronRemark { get; set; }

        public string AssemblyName { get; set; }

        public string ClassName { get; set; }

        public int MisfireInstruction { get; set; }

        public int? Priority { get; set; }

        public int PriorityOrDefault => Priority ?? 5;

        public string MisfireInstructionsJson => _misfireInstructionsJson;

        static readonly string _misfireInstructionsJson = CreateMisfireInstructionsJson();

        private static string CreateMisfireInstructionsJson()
        {
            var standardMisfireInstructions = new Dictionary<int, string>()
            {
                [0] = "Smart Policy",
                [1] = "Fire Once Now",
                [2] = "Do Nothing",
            };

            var validMisfireInstructions = new Dictionary<string, Dictionary<int, string>>()
            {
                ["cron"] = standardMisfireInstructions,
                ["daily"] = standardMisfireInstructions,
                ["simple"] = new Dictionary<int, string>()
                {
                    [0] = "Smart Policy",
                    [1] = "Fire Now",
                    [2] = "Reschedule Now With Existing Repeat Count",
                    [3] = "Reschedule Now With Remaining Repeat Count",
                    [4] = "Reschedule Next With Remaining Count",
                    [5] = "Reschedule Next With Existing Count",
                },
            };

            return JsonConvert.SerializeObject(validMisfireInstructions, Formatting.None);
        }

        public IEnumerable<string> PriorityList => Enumerable.Range(1, 10).Select(x => x.ToString());

        public JobTriggerDto Trigger { get; set; }

        public string TriggerId { get; set; } = string.Empty;

        public bool RepeatForever { get; set; }
        public string SelectedDaysOfWeek { get; set; } = string.Empty;
        public virtual TimeSpan? StartTime { get; set; }

        public virtual TimeSpan? EndTime { get; set; }

        //public virtual JobCategory JobCategory { get; set; }

        //public virtual IEnumerable<ComboboxItemDto> Categories { get; set; }
        public virtual int UIBatchSeq { get; set; }

        public virtual DateTime LastExecuteTime { get; set; }
        public virtual DateTime NextRunTime { get; set; }
        public virtual string JsonParamters { get; set; }
        public virtual bool IsAdhotJob { get; set; }
    }
}
