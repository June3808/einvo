using Volo.Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler.TaskSchedulers;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Dtos;
using System.Reflection;
using System.Threading;
using System.Net.Http;
using System.Net;
using TaskScheduler.TaskSchedulers.Dtos;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp;
using Quartz;
using System.Data;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;
using Volo.Abp.Uow;
using TaskScheduler.Listener;
using static Quartz.Logging.OperationName;

namespace TaskScheduler.TaskSchedulersAppService
{
    public class TaskSchedulersAppService : CrudAppService<ScheduleJobs,ScheduleJobDto, Guid, ScheduleJobGetListInput, CreateUpdateScheduleJobDto,CreateUpdateScheduleJobDto>, ITaskSchedulersAppService
    {
        private readonly IScheduler _scheduler;

        public TaskSchedulersAppService(
            IRepository<ScheduleJobs, Guid> repository, 
            IScheduler scheduler) : base(repository)
        {
            _scheduler = scheduler;
        }

        public async Task<PagedResultDto<ScheduleJobDto>> GetAll(ScheduleJobGetListInput input,CancellationToken token)
        {
            var scheduleJobs = await this.Repository.GetPagedListAsync(input.SkipCount,input.MaxResultCount,input.Sorting,false, token);

            var totalCount = await this.Repository.GetCountAsync();
            return new PagedResultDto<ScheduleJobDto>(
            totalCount,
                ObjectMapper.Map<List<ScheduleJobs>, List<ScheduleJobDto>>(scheduleJobs)
            );
        }

        public async Task<CreateUpdateScheduleJobDto> GetScheduleJobForEdit(Guid? Id)
        {
            var query = await this.Repository.GetQueryableAsync();

            CreateUpdateScheduleJobDto output = new CreateUpdateScheduleJobDto();
            if (Id != Guid.Empty && Id.HasValue)
            {
                var scheduleJob = query.FirstOrDefault(x => x.Id == Id);
                output = ObjectMapper.Map<ScheduleJobs, CreateUpdateScheduleJobDto>(scheduleJob);
            }
            // Set current Assembly Name
            output.AssemblyName = this.getAssemblyName();

            // Get all registered jobs
            var registeredJobs = query.Select(x => x.JobGroup).ToList();

            // Create Job Group dropdown list
            // where if same name as current job class or not registered yet

            var modules = this.GetModules();

            output.JobGroupNames = modules
                .Where(x => !registeredJobs.Contains(x.Name) || x.Name == output.JobGroup)
                .Select(c => new ComboboxItemDto(c.Name, c.Name + " (" + c.FullName + ")")
                { IsSelected = output.JobGroup == c.Name })
                .ToList();

            //output.Categories = this.GetCategory(output.JobCategory);
            return output;
        }

        private List<Type> GetModules()
        {
            var modules = new List<Type>();
            Assembly assembly = Assembly.Load(this.getAssemblyName());
            try
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (this.IsScheduleJob(type))
                    {
                        modules.AddIfNotContains(type);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get module types from assembly: " + assembly.FullName, ex);
            }
            return modules;
        }
        private string getAssemblyName()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly?.FullName;
        }

        public bool IsScheduleJob(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(QuartzBackgroundWorkerBase).IsAssignableFrom(type);
        }

        public async Task CreateOrEditAdvanced(CreateUpdateScheduleJobDto job, bool mustStartNow)
        {
            try
            {
                if (!job.Id.HasValue)
                {
                    await CreateJobWIthTrigger(job, mustStartNow);
                }
                else
                {
                    await UpdateJobWithTrigger(job, mustStartNow);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task CreateJobWIthTrigger(CreateUpdateScheduleJobDto job, bool mustStartNow)
        {
            ScheduleJobs scheduleJob = new ScheduleJobs();
            var trigger = job.Trigger;

            scheduleJob = ObjectMapper.Map<CreateUpdateScheduleJobDto,ScheduleJobs>(job);
            scheduleJob.TriggerId = Guid.NewGuid().ToString();

            if (mustStartNow)
            {
                scheduleJob.JobStatus = JobStatus.Ready;
                if (scheduleJob.StartDateTime == DateTime.MinValue)
                {
                    scheduleJob.StartDateTime = DateTime.Now.AddSeconds(10);
                }
            }
            else
            {
                scheduleJob.JobStatus = JobStatus.Hold;
            }

            if (scheduleJob.StartDateTime > scheduleJob.EndDateTime)
                scheduleJob.EndDateTime = null;

            //set class name
            scheduleJob.ClassName = getAssemblyQualifiedName(scheduleJob.JobGroup);

            await this.Repository.InsertAsync(scheduleJob);

            //build and run the job
            if (mustStartNow)
                await this.BuildAndRunJob(scheduleJob, true);
        }

        //[AbpAuthorize(AppPermissions.Pages_MES_ScheduleJobs_Edit, AppPermissions.Pages_ERP_ScheduleJobs_Edit)]
        private async Task UpdateJobWithTrigger(CreateUpdateScheduleJobDto input, bool mustStartNow)
        {
            var query = await this.Repository.GetQueryableAsync();

            var scheduleJob = query.FirstOrDefault(x => x.Id == input.Id);
            var trigger = input.Trigger;
            input.TriggerId = scheduleJob.TriggerId;

            scheduleJob.TriggerId = scheduleJob.TriggerId ?? Guid.NewGuid().ToString();
            var orignal = scheduleJob;
            ObjectMapper.Map(input, scheduleJob);

            var jobKey = new JobKey(scheduleJob.Id.ToString(), scheduleJob.JobName);
            if (await _scheduler.CheckExists(jobKey))
            {
                //delete the job from quartz
                await _scheduler.DeleteJob(jobKey);
            }

            if (mustStartNow)
            {
                if (scheduleJob.StartDateTime == DateTime.MinValue)
                {
                    scheduleJob.StartDateTime = DateTime.Now.AddSeconds(30);
                }

                if (scheduleJob.StartDateTime > scheduleJob.EndDateTime)
                    scheduleJob.EndDateTime = null;

                scheduleJob.JobStatus = JobStatus.Ready;
            }
            else
            {
                scheduleJob.StartDateTime = input.StartDateTime.HasValue ? input.StartDateTime.Value : DateTime.MinValue;
                scheduleJob.JobStatus = input.JobStatus;

            }

            //set class name
            scheduleJob.ClassName = getAssemblyQualifiedName(scheduleJob.JobGroup);

            //build and run the job
            if (mustStartNow)
            {
                //var dtExecutingJob = await this.Repository
                //    .GetDataTable("SELECT * FROM DOT_QRTZ_FIRED_TRIGGERS with(nolock)", System.Data.CommandType.Text);

                //var isJobRunning = false;
                //for (int i = 0; i < dtExecutingJob.Rows.Count; i++)
                //{
                //    DataRow row = dtExecutingJob.Rows[i];

                //    //var TriggerGroup = Convert.ToString(row["TRIGGER_GROUP"]);
                //    var JobName = Convert.ToString(row["JOB_NAME"]);
                //    //var JobGroup = Convert.ToString(row["JOB_GROUP"]);

                //    if (JobName == scheduleJob.Id.ToString())
                //    {
                //        isJobRunning = true;
                //        break;
                //    }
                //}

                //if (isJobRunning)
                //    throw new UserFriendlyException("Job is already running");

                await this.BuildAndRunJob(scheduleJob, true);
            }

            //var Log = "Update Job:" + scheduleJob.JobName + " successfully.";
            //await _taskSchedulerDomainService.InsertJobHistory(scheduleJob, Log);
        }

        private string getAssemblyQualifiedName(string typeName)
        {
            var type = this.GetModules().Where(t => t.Name == typeName).FirstOrDefault();
            if (type == null)
                throw new AbpException("Can not find the job type name!");
            return type.AssemblyQualifiedName;
        }


        #region Domain Service

        protected async Task<IJobDetail> BuildJob(ScheduleJobs scheduleJob, JobDataMap jobData = null)
        {
            // quartz
            var jobKey = new JobKey(scheduleJob.Id.ToString(), scheduleJob.JobName);

            var workerClass = Type.GetType(scheduleJob.ClassName);

            // filter only webAdmin Job
            if (!this.IsScheduleJob(workerClass))
                return null;

            var builder = JobBuilder.Create(workerClass)
                        .WithIdentity(jobKey);

            //Jobs added with no trigger must be durable
            if (scheduleJob.IsAdhotJob)
            {
                builder.StoreDurably(true);
                builder.RequestRecovery(true);
            }
            else
            {
                builder.RequestRecovery(false);
            }

            if (jobData != null)
                builder.UsingJobData(jobData);

            var job = builder.Build();
            return job;
        }

        protected async Task BuildAndRunJob(ScheduleJobs scheduleJob, bool deleteIfExists = true)
        {
            try
            {
                var job = await BuildJob(scheduleJob);

                if (job == null)
                    return;

                var trigger = await BuildTrigger(scheduleJob);

                //var starRunTime = DateBuilder.NextGivenSecondDate(DateTime.Now, 5);
                //var startRunTime = DateBuilder.FutureDate(30, IntervalUnit.Second);
                //DateTimeOffset? endRunTime = null;
                //if (scheduleJob.EndDateTime.HasValue)
                //    endRunTime = DateBuilder.NextGivenSecondDate(scheduleJob.EndDateTime, 1);

                var isJobExists = await _scheduler.CheckExists(job.Key);


                // if job exists and require skip delete
                if (isJobExists)
                {
                    if (deleteIfExists)
                    {
                        await _scheduler.DeleteJob(job.Key);
                        await _scheduler.ScheduleJob(job, trigger);
                    }
                }
                else
                {
                    //fix where job already deleted from scheduler but trigger still exists
                    //delete existing trigger if any
                    var isTriggerExists = await _scheduler.CheckExists(trigger.Key);
                    if (isTriggerExists)
                    {
                        //var oldTrigger = _scheduler.GetTrigger(trigger.Key);
                        await _scheduler.UnscheduleJob(trigger.Key);
                    }

                    await _scheduler.ScheduleJob(job, trigger);
                }
            }
            catch (Exception e)
            {
                this.Logger.LogError(e.Message);
                //throw e;
            }
        }

        protected async Task<ITrigger> BuildTrigger(ScheduleJobs scheduleJob)
        {
            //check starttime
            DateTimeOffset startRunTime;
            if (scheduleJob.StartDateTime == DateTime.MinValue || scheduleJob.StartDateTime < DateTime.Now)
            {
                startRunTime = DateBuilder.FutureDate(10, IntervalUnit.Second);
            }
            else
            {
                startRunTime = scheduleJob.StartDateTime;
            }

            ITrigger trigger = null;
            var triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(scheduleJob.TriggerId, scheduleJob.JobGroup)
                .UsingJobData("ScheduleJobId", scheduleJob.Id);
            //.EndAt(endRunTime);
            triggerBuilder = triggerBuilder.StartAt(startRunTime);

            if (scheduleJob.EndDateTime > startRunTime)
                triggerBuilder.EndAt(scheduleJob.EndDateTime);

            if (scheduleJob.TriggerType == TriggerType.Cron)
            {
                triggerBuilder.WithCronSchedule(scheduleJob.Cron, x =>
                {
                    switch (scheduleJob.MisfireInstruction)
                    {
                        case null:
                            x.WithMisfireHandlingInstructionDoNothing();
                            break;
                        case MisfireInstruction.InstructionNotSet:
                            break;
                        case MisfireInstruction.IgnoreMisfirePolicy:
                            x.WithMisfireHandlingInstructionIgnoreMisfires();
                            break;
                        case MisfireInstruction.CronTrigger.DoNothing:
                            x.WithMisfireHandlingInstructionDoNothing();
                            break;
                        case MisfireInstruction.CronTrigger.FireOnceNow:
                            x.WithMisfireHandlingInstructionFireAndProceed();
                            break;
                        default:
                            throw new ArgumentException("Invalid value: " + scheduleJob.MisfireInstruction, nameof(scheduleJob.MisfireInstruction));
                    }
                });

                //triggerBuilder = triggerBuilder.WithCronSchedule(scheduleJob.Cron);
                ////if (mustStartNow)
                ////    triggerBuilder = triggerBuilder.StartNow();
                ////else
                trigger = (ICronTrigger)triggerBuilder.Build();
            }
            else if (scheduleJob.TriggerType == TriggerType.Daily)
            {
                triggerBuilder.WithDailyTimeIntervalSchedule(x =>
                {
                    if (!scheduleJob.RepeatForever)
                        x.WithRepeatCount(scheduleJob.RunTimes);

                    if (scheduleJob.IntervalMinutes > 0 || scheduleJob.IntervalSeconds > 0)
                        x.WithIntervalInSeconds((scheduleJob.IntervalMinutes * 60) + scheduleJob.IntervalSeconds);

                    x.StartingDailyAt(new TimeOfDay(scheduleJob.StartTime.Value.Hours, scheduleJob.StartTime.Value.Minutes, scheduleJob.StartTime.Value.Seconds));
                    x.EndingDailyAt(new TimeOfDay(scheduleJob.EndTime.Value.Hours, scheduleJob.EndTime.Value.Minutes, scheduleJob.EndTime.Value.Seconds));

                    if (!string.IsNullOrEmpty(scheduleJob.SelectedDaysOfWeek))
                    {
                        var DaysOfWeek = DaysOfWeekViewModel.Create(scheduleJob.SelectedDaysOfWeek);
                        var selectedDays = DaysOfWeek.GetSelected().ToArray();
                        x.OnDaysOfTheWeek(selectedDays);
                    }

                    switch (scheduleJob.MisfireInstruction)
                    {
                        case null:
                            x.WithMisfireHandlingInstructionDoNothing();
                            break;
                        case MisfireInstruction.InstructionNotSet:
                            x.WithMisfireHandlingInstructionFireAndProceed();
                            break;
                        case MisfireInstruction.IgnoreMisfirePolicy:
                            x.WithMisfireHandlingInstructionIgnoreMisfires();
                            break;
                        case MisfireInstruction.DailyTimeIntervalTrigger.DoNothing:
                            x.WithMisfireHandlingInstructionDoNothing();
                            break;
                        case MisfireInstruction.DailyTimeIntervalTrigger.FireOnceNow:
                            x.WithMisfireHandlingInstructionFireAndProceed();
                            break;
                        default:
                            throw new ArgumentException("Invalid value: " + scheduleJob.MisfireInstruction, nameof(scheduleJob.MisfireInstruction));
                    }
                });

                trigger = (IDailyTimeIntervalTrigger)triggerBuilder.Build();
            }
            else
            {
                triggerBuilder.WithSimpleSchedule(x =>
                {
                    if (scheduleJob.IntervalMinutes > 0 || scheduleJob.IntervalSeconds > 0)
                        x.WithIntervalInSeconds((scheduleJob.IntervalMinutes * 60) + scheduleJob.IntervalSeconds);

                    if (scheduleJob.RepeatForever || scheduleJob.RunTimes == 0)
                        x.RepeatForever();
                    else
                        x.WithRepeatCount(scheduleJob.RunTimes);

                    switch (scheduleJob.MisfireInstruction)
                    {
                        case null:
                            x.WithMisfireHandlingInstructionFireNow();
                            break;
                        case (int)MisfireInstruction.InstructionNotSet:
                            break;
                        case (int)MisfireInstruction.IgnoreMisfirePolicy:
                            x.WithMisfireHandlingInstructionIgnoreMisfires();
                            break;
                        case (int)MisfireInstruction.SimpleTrigger.FireNow:
                            x.WithMisfireHandlingInstructionFireNow();
                            break;
                        case (int)MisfireInstruction.SimpleTrigger.RescheduleNowWithExistingRepeatCount:
                            x.WithMisfireHandlingInstructionNowWithExistingCount();
                            break;
                        case (int)MisfireInstruction.SimpleTrigger.RescheduleNowWithRemainingRepeatCount:
                            x.WithMisfireHandlingInstructionNowWithRemainingCount();
                            break;
                        case (int)MisfireInstruction.SimpleTrigger.RescheduleNextWithRemainingCount:
                            x.WithMisfireHandlingInstructionNextWithExistingCount();
                            break;
                        default:
                            x.WithMisfireHandlingInstructionFireNow();
                            break;
                    }
                });

                //if (mustStartNow)
                //    triggerBuilder = triggerBuilder.StartNow();
                //else
                trigger = (ISimpleTrigger)triggerBuilder.Build();
            }

            return trigger;
        } 

        public async Task<IJobDetail?> BuildJobDetail(Type type)
        {
            var query = await this.ReadOnlyRepository.GetQueryableAsync();

            var className = getAssemblyQualifiedName(type.Name);


            var registeredJob = query.Where(s=> s.ClassName == className).FirstOrDefault();


            if (registeredJob == null)
                return null;

            var job = await BuildJob(registeredJob);


            return job;
        }

        public async Task<ITrigger?> BuildTrigger(Type type)
        {
            var query = await this.ReadOnlyRepository.GetQueryableAsync();

            var className = getAssemblyQualifiedName(type.Name);


            var registeredJob = query.Where(s => s.ClassName == className).FirstOrDefault();

            if (registeredJob == null)
                return null;

            var trigger = await BuildTrigger(registeredJob);

            return trigger;
        }

        public async Task StopScheduleJob(Guid id)
        {
            var query = await this.Repository.GetQueryableAsync();
            var scheduleJob = query.FirstOrDefault(x=> x.Id == id);

            scheduleJob.JobStatus = JobStatus.Hold;
            //await _scheduler.PauseJob(new JobKey(scheduleJob.Id.ToString(), scheduleJob.JobGroup)); //pause job not working for web admin
            var jobKey = new JobKey(scheduleJob.Id.ToString(), scheduleJob.JobName);

            //cannot delete durable job
            //delete adhoc job triggers by job key
            if (scheduleJob.IsAdhotJob)
            {
                var pendingTriggers = await _scheduler.GetTriggersOfJob(jobKey);
                if (pendingTriggers.Count() > 0)
                {
                    foreach (var trigger in pendingTriggers)
                    {
                        await _scheduler.UnscheduleJob(trigger.Key);
                    }
                }

                //var sql = $"delete DOT_QRTZ_FIRED_TRIGGERS where JOB_NAME = '{scheduleJob.Id}'";
                //var rowsAffected = _context.Database.ExecuteSqlRaw(sql);
            }
            else
            {
                if (await _scheduler.CheckExists(jobKey))
                {
                    //delete the job from quartz
                    await _scheduler.DeleteJob(jobKey);
                }

                //fix where job already deleted from scheduler but trigger still exists
                //delete existing trigger if any
                var triggerkey = new TriggerKey(scheduleJob.TriggerId, scheduleJob.JobGroup);
                var trigger = await _scheduler.GetTrigger(triggerkey);
                if (trigger != null)
                {
                    var isTriggerExists = await _scheduler.CheckExists(trigger.Key);
                    if (isTriggerExists)
                    {
                        await _scheduler.UnscheduleJob(trigger.Key);
                    }
                }

                //var sql = $"delete DOT_QRTZ_FIRED_TRIGGERS where TRIGGER_NAME = '{scheduleJob.TriggerId}'";
                //var rowsAffected = _context.Database.ExecuteSqlRaw(sql);
            }


            //var Log = "On Hold Job:" + scheduleJob.JobName + " successfully.";
            //await _taskSchedulerDomainService.InsertJobHistory(scheduleJob, Log);

        }

        public async Task ScheduleAllJobs()
        {
            try
            {
                var jobs = await this.Repository.GetListAsync();

                //var jobs = query.Where(job => job.JobStatus != JobStatus.Hold && !job.IsAdhotJob).ToList(); //filter out on-Hold status
                                                                                                    // filter by version to avoid run WebAdmin2 job
                
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                //query = query.Where(e => e.ClassName.Contains(version));

                foreach (var scheduleJob in jobs)
                {
                    await BuildAndRunJob(scheduleJob, true);
                }
            }
            catch(Exception e){
                throw e;
            }
        }

        public async Task StartScheduleJob(Guid id)
        {
            var query = await this.Repository.GetQueryableAsync();
            var scheduleJob = query.FirstOrDefault(x => x.Id == id);

            //var job = _scheduler.GetJobDetail(jobKey);
            var jobKey = new JobKey(scheduleJob.Id.ToString(), scheduleJob.JobName);
            var Log = "";
            try
            {
                //await _scheduler.ResumeJob(jobKey); resume not working for web admin
                //rebuild and run the job
                await this.BuildAndRunJob(scheduleJob, true);
                scheduleJob.JobStatus = JobStatus.Ready;
                Log = "Start Job:" + scheduleJob.JobName + " successfully.";
            }
            catch (Exception e)
            {
                Log = "Start Job:" + scheduleJob.JobName + " fail:" + e.Message;
            }

            Logger.LogInformation(Log);
            //await _taskSchedulerDomainService.InsertJobHistory(scheduleJob, Log);
        }

        //[UnitOfWork(IsDisabled = true)]
        public async Task UpdateJobStatus(JobKey key, JobRunStatus runStatus, string log)
        {

            var job = await _scheduler.GetJobDetail(key);
            var jobId = Guid.Parse(key.Name);
            var logBuilder = new StringBuilder();
            logBuilder.AppendLine(log);

            var query = await this.ReadOnlyRepository.GetQueryableAsync();

            var scheduleJob = query.FirstOrDefault(s => s.Id == jobId);

            if (scheduleJob == null)
            {
                logBuilder.AppendLine(string.Format("Job Id:{0} not found!", jobId));
                Logger.LogError(logBuilder.ToString());
            }
            else
            {
                try
                {
                    // Update job detail info
                    scheduleJob.JobRunStatus = runStatus;
                    // Update next run time
                    var triggers = await _scheduler.GetTriggersOfJob(key);
                    //var history = new JobHistory();
                    //history.JobName = scheduleJob.JobName;
                    var logToDb = false; // mark to require log to Job_history table 
                    foreach (var trigger in triggers)
                    {
                        switch (runStatus)
                        {
                            case JobRunStatus.Waiting:
                            case JobRunStatus.Executing:
                            case JobRunStatus.Vetoed:
                                scheduleJob.StartDateTime = DateTime.Now;
                                //history.StartDateTime = trigger.StartTimeUtc.LocalDateTime;
                                break;
                            case JobRunStatus.Error:
                            case JobRunStatus.Finished:
                                logToDb = true; // only Error / Finished only log to Job_history table 
                                scheduleJob.EndDateTime = DateTime.Now;
                                //history.EndDateTime = DateTime.Now;
                                break;
                            default:
                                break;
                        }
                        scheduleJob.LastExecuteTime = trigger.GetPreviousFireTimeUtc().HasValue ? trigger.GetPreviousFireTimeUtc().Value.LocalDateTime : DateTime.MinValue;
                        scheduleJob.NextRunTime = trigger.GetNextFireTimeUtc().HasValue ? trigger.GetNextFireTimeUtc().Value.LocalDateTime : DateTime.MaxValue;

                        ////history.JobRunStatus = await _scheduler.GetTriggerState(trigger.Key);// trigger status not correct
                        //history.JobRunStatus = runStatus;

                        ////history.StartDateTime = trigger.StartTimeUtc.LocalDateTime;
                        ////history.EndDateTime = trigger.EndTimeUtc.HasValue ? trigger.EndTimeUtc.Value.LocalDateTime : DateTime.MaxValue;
                        //history.TriggerName = trigger.Key.Name;
                        //history.TriggerGroup = trigger.Key.Group;
                    }
                    await this.Repository.UpdateAsync(scheduleJob);
                    //if (logToDb)
                    //    await this.InsertJobHistory(scheduleJob, logBuilder.ToString(), history);

                    Logger.LogInformation(log);
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Fail to Update Job:{key} Status:{runStatus},{ex.Message}");
                }

            }
        }
        #endregion

    }
}
