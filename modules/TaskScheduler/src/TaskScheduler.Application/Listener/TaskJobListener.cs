using Quartz.Impl.Matchers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskScheduler.TaskSchedulers;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.DependencyInjection;
using Quartz.Logging;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;
using System.Runtime.CompilerServices;
using Volo.Abp.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TaskScheduler.Listener
{
    //[Dependency(ReplaceServices = true)]
    public class TaskJobListener : IJobListener
    {
        public string Name { get; } = "TSJobListener";

        private ITaskSchedulersAppService _taskSchedulerDomainService  => this.lazy.LazyGetRequiredService<ITaskSchedulersAppService>();
        private IScheduler _scheduler => this.lazy.LazyGetRequiredService<IScheduler>();

        private IAbpLazyServiceProvider lazy;

        public TaskJobListener(
            IAbpLazyServiceProvider lazy
            )
        {
            this.lazy = lazy;
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {


            var log = $"Job {context.JobDetail.JobType.Name} executing on {context.FireInstanceId}";
            var key = context.JobDetail.Key;

            ////Pause Job if is running
            //if ((await _taskSchedulerDomainService.GetJobStatus(key)) == JobRunStatus.Executing)
            //    {
            //        await context.Scheduler.PauseJob(key);
            //        await context.Scheduler.PauseTrigger(context.Trigger.Key);
            //    }

            await _taskSchedulerDomainService.UpdateJobStatus(key, JobRunStatus.Executing, log);

            var alltriggers = await _scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());
            foreach (var triggerkey in alltriggers)
            {
                //var triggerdetails = await _scheduler.GetTrigger(triggerkey);
                var triggerstate = await _scheduler.GetTriggerState(triggerkey);
                if (triggerstate == TriggerState.Error)
                {
                    await _scheduler.ResumeTrigger(triggerkey);
                }
            }
        }

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var log = $"Job {context.JobDetail.JobType.Name} executing operation vetoed on {context.FireInstanceId}";
            var key = context.JobDetail.Key;
            await _taskSchedulerDomainService.UpdateJobStatus(key, JobRunStatus.Vetoed, log);
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            var key = context.JobDetail.Key;
            if (jobException == null)
            {
                var CustomLog = context.Get("CustomLog");
                var log = $"Job {context.JobDetail.JobType.Name} sucessfully executed on {context.FireInstanceId}. {CustomLog}";
                //await _taskSchedulerDomainService.UpdateJobStatus(key, JobRunStatus.Finished, log);
            }
            else
            {
                //if (jobException.InnerException != null && jobException.InnerException is D365BlockingDetectedException)
                //{
                //    // Update job progress to "Vetoed"
                //    // Insert to job log
                //    var blockingEx = jobException.InnerException as D365BlockingDetectedException;
                //    await _taskSchedulerDomainService.UpdateJobStatus(context.JobDetail.Key, JobRunStatus.Vetoed, blockingEx.Message);

                //}
                //else
                //{
                    var log = $"Job {context.JobDetail.JobType.Name} failed on {context.FireInstanceId} with exception: {jobException}";
                    await _taskSchedulerDomainService.UpdateJobStatus(key, JobRunStatus.Error, log);
                //}

                var trigger = context.Trigger;
                var triggerkey = trigger.Key;
                var triggerstate = await context.Scheduler.GetTriggerState(triggerkey);

                if (triggerstate == TriggerState.Error)
                {
                    await context.Scheduler.ResumeTrigger(triggerkey);
                }
            }

            //resume Job if was finished
            //if ((await _taskSchedulerDomainService.GetJobStatus(key)) != JobRunStatus.Executing)
            //{
            //    await context.Scheduler.ResumeJob(key);
            //    await context.Scheduler.ResumeTrigger(context.Trigger.Key);
            //}
        }
    }
}
