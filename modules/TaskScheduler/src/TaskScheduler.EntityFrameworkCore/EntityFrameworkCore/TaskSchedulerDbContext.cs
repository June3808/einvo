using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace TaskScheduler.EntityFrameworkCore;

[ConnectionStringName(TaskSchedulerDbProperties.ConnectionStringName)]
public class TaskSchedulerDbContext : AbpDbContext<TaskSchedulerDbContext>, ITaskSchedulerDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DbSet<ScheduleJobs> TaskScheduler { get; set; }

    public TaskSchedulerDbContext(DbContextOptions<TaskSchedulerDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureTaskScheduler();
    }
}
