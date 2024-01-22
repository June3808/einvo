using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using EasyAbp.FileManagement;

namespace EInvoice;

[DependsOn(
    typeof(FileManagementApplicationContractsModule),
    typeof(EInvoiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class EInvoiceApplicationContractsModule : AbpModule
{

}
