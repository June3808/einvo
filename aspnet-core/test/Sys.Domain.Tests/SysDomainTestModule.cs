using Sys.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Sys;

[DependsOn(
    typeof(SysEntityFrameworkCoreTestModule)
    )]
public class SysDomainTestModule : AbpModule
{

}
