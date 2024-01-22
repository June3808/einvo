using Volo.Abp.Modularity;

namespace Sys;

[DependsOn(
    typeof(SysApplicationModule),
    typeof(SysDomainTestModule)
    )]
public class SysApplicationTestModule : AbpModule
{

}
