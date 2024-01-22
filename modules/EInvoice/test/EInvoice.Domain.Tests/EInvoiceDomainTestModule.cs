using EInvoice.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EInvoice;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(EInvoiceEntityFrameworkCoreTestModule)
    )]
public class EInvoiceDomainTestModule : AbpModule
{

}
