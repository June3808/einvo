using Localization.Resources.AbpUi;
using EInvoice.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using EasyAbp.FileManagement;

namespace EInvoice;

[DependsOn(
    typeof(FileManagementHttpApiModule),
    typeof(EInvoiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class EInvoiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(EInvoiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<EInvoiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
