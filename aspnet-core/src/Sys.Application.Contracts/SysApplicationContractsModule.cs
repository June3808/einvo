using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using EasyAbp.Abp.SettingUi;
using EInvoice;
using TaskScheduler;

namespace Sys;

[DependsOn(
    typeof(AbpSettingUiApplicationContractsModule),
    typeof(SysDomainSharedModule),
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpTenantManagementApplicationContractsModule),
    typeof(AbpObjectExtendingModule),
    typeof(AbpSettingUiApplicationContractsModule),
    typeof(EInvoiceApplicationContractsModule)
)]
[DependsOn(typeof(TaskSchedulerApplicationContractsModule))]
public class SysApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SysDtoExtensions.Configure();
    }
}
