using EasyAbp.FileManagement;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EInvoice;

[DependsOn(
    typeof(FileManagementDomainModule),
    typeof(AbpDddDomainModule),
    typeof(EInvoiceDomainSharedModule)
)]
public class EInvoiceDomainModule : AbpModule
{

}
