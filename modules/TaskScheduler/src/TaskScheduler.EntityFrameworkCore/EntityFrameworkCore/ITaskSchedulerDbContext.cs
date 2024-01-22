using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace TaskScheduler.EntityFrameworkCore;

[ConnectionStringName(TaskSchedulerDbProperties.ConnectionStringName)]
public interface ITaskSchedulerDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */

    DbSet<ScheduleJobs> TaskScheduler { get; set; }
}
