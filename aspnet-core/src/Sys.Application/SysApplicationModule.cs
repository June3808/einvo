using EInvoice;
using TaskScheduler;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Quartz;
using EasyAbp.Abp.SettingUi;

namespace Sys;

[DependsOn(
    typeof(SysDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(SysApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpSettingUiApplicationModule),
    typeof(EInvoiceApplicationModule)
    )]
[DependsOn(typeof(TaskSchedulerApplicationModule))]
public class SysApplicationModule : AbpQuartzModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<SysApplicationModule>();
        });
    }
}
