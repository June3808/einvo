using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace TaskScheduler.EntityFrameworkCore;

[DependsOn(
    typeof(TaskSchedulerDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class TaskSchedulerEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<TaskSchedulerDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */

            options.AddDefaultRepositories(includeAllEntities: true);
        });
    }
}
