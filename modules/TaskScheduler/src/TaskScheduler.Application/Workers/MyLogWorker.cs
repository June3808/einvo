using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskScheduler.Listener;
using TaskScheduler.TaskSchedulers;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace TaskScheduler.Workers
{
    [DisallowConcurrentExecution]
    public class IRBMSync : QuartzBackgroundWorkerBase, ISingletonDependency
    {
        public IRBMSync()
        {
            AutoRegister = false;
            //JobDetail = JobBuilder.Create<MyLogWorker>().WithIdentity(nameof(MyLogWorker)).Build();
            //Trigger = TriggerBuilder.Create().WithIdentity(nameof(MyLogWorker)).WithSimpleSchedule(s => s.WithIntervalInMinutes(1).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();
        }

        public override Task Execute(IJobExecutionContext context)
        {
            this.Logger.LogInformation("MyLogWorker");
            return Task.CompletedTask;
        }
    }
}
