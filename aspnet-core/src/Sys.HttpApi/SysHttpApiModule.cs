using Localization.Resources.AbpUi;
using Sys.Localization;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using EasyAbp.Abp.SettingUi;
using Microsoft.Extensions.DependencyInjection;
using EInvoice;
using TaskScheduler;

namespace Sys;

[DependsOn(
    typeof(SysApplicationContractsModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(AbpSettingUiHttpApiModule),
    typeof(EInvoiceHttpApiModule)
    )]
[DependsOn(typeof(TaskSchedulerHttpApiModule))]
public class SysHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SysResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
