using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using EasyAbp.FileManagement;

namespace EInvoice;

[DependsOn(
    typeof(FileManagementApplicationModule),
    typeof(EInvoiceDomainModule),
    typeof(EInvoiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class EInvoiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<EInvoiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<EInvoiceApplicationModule>(validate: true);
        });
    }
}
