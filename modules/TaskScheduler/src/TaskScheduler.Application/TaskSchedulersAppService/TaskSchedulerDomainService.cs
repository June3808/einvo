//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using TaskScheduler.TaskSchedulers;
//using Volo.Abp.Application.Services;
//using Volo.Abp.BackgroundWorkers.Quartz;

//namespace TaskScheduler.TaskSchedulersAppService
//{
//    public class TaskSchedulerDomainService : ApplicationService, ITaskSchedulerDomainService
//    {
//        public TaskSchedulerDomainService()
//        {
//        }

//        public bool IsScheduleJob(Type type)
//        {
//            var typeInfo = type.GetTypeInfo();
//            return
//                typeInfo.IsClass &&
//                !typeInfo.IsAbstract &&
//                !typeInfo.IsGenericType &&
//                typeof(QuartzBackgroundWorkerBase).IsAssignableFrom(type);
//        }
//    }
//}
