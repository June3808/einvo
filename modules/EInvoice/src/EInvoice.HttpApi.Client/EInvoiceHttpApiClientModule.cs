using EasyAbp.FileManagement;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EInvoice;

[DependsOn(
    typeof(FileManagementHttpApiClientModule),
    typeof(EInvoiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class EInvoiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(EInvoiceApplicationContractsModule).Assembly,
            EInvoiceRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EInvoiceHttpApiClientModule>();
        });

    }
}
