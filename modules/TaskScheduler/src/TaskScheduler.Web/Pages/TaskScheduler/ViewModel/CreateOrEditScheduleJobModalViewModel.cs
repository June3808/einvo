using System;
using TaskScheduler.TaskSchedulers.Dtos;

namespace TaskScheduler.Web.Pages.TaskScheduler.ViewModel
{
    public class CreateOrEditScheduleJobModalViewModel
    {
        public CreateOrEditScheduleJobModalViewModel()
        {
            this.ScheduleJob = new CreateUpdateScheduleJobDto();
            this.Trigger = new TriggerViewModel();
        }

        public CreateUpdateScheduleJobDto ScheduleJob { get; set; }

        public TriggerViewModel Trigger { get; set; }

        public bool IsEditMode
        {
            get { return (this.ScheduleJob.Id.HasValue || this.ScheduleJob.Id == Guid.Empty); }
        }

        public void ApplyFromJob()
        {
            if (ScheduleJob == null)
                return;

            this.Trigger = new TriggerViewModel();

            // don't show start time in the past because rescheduling cause triggering missfire policies
            ScheduleJob.StartDateTime = ScheduleJob.StartDateTime > DateTime.Now ? ScheduleJob.StartDateTime : null;

            if (!ScheduleJob.StartDateTime.HasValue)
                ScheduleJob.EndDateTime = null;

            if (ScheduleJob.TriggerType == TriggerType.Simple)
            {

                Trigger.Simple.IntervalMinutes = this.ScheduleJob.IntervalMinutes;
                Trigger.Simple.IntervalSeconds = this.ScheduleJob.IntervalSeconds;
                Trigger.Simple.RunTimes = this.ScheduleJob.RunTimes;
                Trigger.Simple.RepeatForever = ScheduleJob.RepeatForever;
            }
            else if (ScheduleJob.TriggerType == TriggerType.Cron)
            {
                Trigger.Cron.CronExpression = this.ScheduleJob.Cron;
                Trigger.Cron.CronRemark = this.ScheduleJob.CronRemark;
            }
            else if (ScheduleJob.TriggerType == TriggerType.Daily)
            {
                Trigger.Daily.IntervalMinutes = this.ScheduleJob.IntervalMinutes;
                Trigger.Daily.IntervalSeconds = this.ScheduleJob.IntervalSeconds;

                Trigger.Daily.RunTimes = this.ScheduleJob.RunTimes;
                Trigger.Daily.RepeatForever = this.ScheduleJob.RepeatForever;
                Trigger.Daily.StartTime = this.ScheduleJob.StartDateTime.HasValue ? this.ScheduleJob.StartDateTime.Value.TimeOfDay : null;
                Trigger.Daily.EndTime = this.ScheduleJob.EndDateTime.HasValue ? this.ScheduleJob.EndDateTime.Value.TimeOfDay : null;

                Trigger.Daily.DaysOfWeek = DaysOfWeekViewModel.Create(this.ScheduleJob.SelectedDaysOfWeek);

                Trigger.Daily.StartTime = this.ScheduleJob.StartTime;
                Trigger.Daily.EndTime = this.ScheduleJob.EndTime;
            }
        }

        public bool mustStartNow { get; set; }

        //public JobRoles JobRole { get; set; }
    }
}
