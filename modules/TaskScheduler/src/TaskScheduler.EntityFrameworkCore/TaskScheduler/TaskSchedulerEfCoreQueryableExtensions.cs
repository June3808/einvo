using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler.TaskScheduler
{
    public static class TaskSchedulerEfCoreQueryableExtensions
    {
        public static IQueryable<ScheduleJobs> IncludeDetails(this IQueryable<ScheduleJobs> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                // .Include(x => x.xxx) // TODO: AbpHelper generated
                ;
        }
    }
}
