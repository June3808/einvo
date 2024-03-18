using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using TaskScheduler;
using TaskScheduler.TaskSchedulers;
using TaskScheduler.TaskSchedulers.Dtos;
using TaskScheduler.Web.Pages;
using TaskScheduler.Web.Pages.TaskScheduler.ViewModel;

namespace TaskSchedulers.Web.Pages.TaskScheduler.ViewModel
{
    public class CreateModel : TaskSchedulerPageModel
    {
        private readonly ITaskSchedulersAppService _taskSchedulerAppService;
        public CreateModel(ITaskSchedulersAppService taskSchedulerAppService)
        {
            this._taskSchedulerAppService = taskSchedulerAppService;
        }

        [BindProperty]
        public CreateUpdateScheduleJobDto ScheduleJob { get; set; }

        [BindProperty]
        public TriggerViewModel Trigger { get; set; }

        [BindProperty]
        public bool TriggerNow { get; set; }

        public List<SelectListItem> TriggersList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "simple", Text = "Simple"},
            new SelectListItem { Value = "cron", Text = "Cron"},
            new SelectListItem { Value = "daily", Text = "Daily"}
        };

        public async Task OnGet(Guid? id)
        {
            CreateUpdateScheduleJobDto scheduleJob = await _taskSchedulerAppService.GetScheduleJobForEdit(id);

            var model = new CreateOrEditScheduleJobModalViewModel()
            {
                ScheduleJob = scheduleJob
            };

            model.ApplyFromJob();

            ScheduleJob = model.ScheduleJob;
            Trigger = model.Trigger;
        }

        public virtual async Task<IActionResult> OnPostAsync([FromForm] CreateOrEditScheduleJobModalViewModel input)
        {
            CreateUpdateScheduleJobDto model = ProcessData(input);
            await _taskSchedulerAppService.CreateOrEditAdvanced(model, input.mustStartNow);
            return Redirect("/TaskScheduler");
        }

        private CreateUpdateScheduleJobDto ProcessData(CreateOrEditScheduleJobModalViewModel input)
        {
            var model = input.ScheduleJob;

            JobTriggerDto trigger = new JobTriggerDto();

            if (model.TriggerType == TriggerType.Simple)
            {
                var m = input.Trigger.Simple;

                trigger.IntervalMinutes = m.IntervalMinutes;
                trigger.IntervalSeconds = m.IntervalSeconds;
                trigger.RunTimes = m.RunTimes;
                trigger.RepeatForever = m.RepeatForever;

                model.RepeatForever = m.RepeatForever;
                model.IntervalMinutes = m.IntervalMinutes;
                model.IntervalSeconds = m.IntervalSeconds;
            }
            else if (model.TriggerType == TriggerType.Cron)
            {
                var m = input.Trigger.Cron;

                trigger.CronExpression = m.CronExpression;
                model.Cron = m.CronExpression;
                model.CronRemark = m.CronRemark;
            }
            else if (model.TriggerType == TriggerType.Daily)
            {
                var m = input.Trigger.Daily;

                trigger.IntervalSeconds = m.IntervalSeconds;
                trigger.IntervalMinutes = m.IntervalMinutes;
                trigger.RunTimes = m.RunTimes;
                trigger.RepeatForever = m.RepeatForever;
                trigger.StartTime = m.StartTime;
                trigger.EndTime = m.EndTime;
                trigger.DaysOfWeek = m.DaysOfWeek;

                model.RepeatForever = m.RepeatForever;
                model.IntervalMinutes = m.IntervalMinutes;
                model.IntervalSeconds = m.IntervalSeconds;
                model.StartTime = m.StartTime;
                model.EndTime = m.EndTime;

                model.SelectedDaysOfWeek = m.DaysOfWeek.SelectedDaysInJSON;
            }

            model.Trigger = trigger;

            return model;
        }



    }
}
