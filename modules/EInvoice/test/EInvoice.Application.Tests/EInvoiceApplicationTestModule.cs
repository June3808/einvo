using Volo.Abp.Modularity;

namespace EInvoice;

[DependsOn(
    typeof(EInvoiceApplicationModule),
    typeof(EInvoiceDomainTestModule)
    )]
public class EInvoiceApplicationTestModule : AbpModule
{

}
