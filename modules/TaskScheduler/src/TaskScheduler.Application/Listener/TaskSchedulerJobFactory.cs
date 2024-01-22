using Quartz.Spi;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace TaskScheduler.Listener
{
    public class TaskSchedulerJobFactory : IJobFactory
    {
        protected readonly IServiceProvider _serviceProvider;

        public TaskSchedulerJobFactory(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
            => _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;

        public void ReturnJob(IJob job)
            => (job as IDisposable)?.Dispose();
    }
}
