using EasyAbp.FileManagement.EntityFrameworkCore;
using EInvoice;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EInvoice.EntityFrameworkCore;

[DependsOn(
    typeof(FileManagementEntityFrameworkCoreModule),
    typeof(EInvoiceDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class EInvoiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<EInvoiceDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            //options.AddRepository<InvoiceJournals, InvoiceJournalsRepository>();
            options.AddDefaultRepositories(includeAllEntities: true);
        });
    }
}
