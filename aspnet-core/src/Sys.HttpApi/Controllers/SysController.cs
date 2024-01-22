using Sys.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Sys.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SysController : AbpControllerBase
{
    protected SysController()
    {
        LocalizationResource = typeof(SysResource);
    }
}
