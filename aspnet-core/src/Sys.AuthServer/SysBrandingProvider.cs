using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Sys;

[Dependency(ReplaceServices = true)]
public class SysBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "EInvo";
}
