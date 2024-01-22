using System;
using System.Collections.Generic;
using System.Text;
using Sys.Localization;
using Volo.Abp.Application.Services;

namespace Sys;

/* Inherit your application services from this class.
 */
public abstract class SysAppService : ApplicationService
{
    protected SysAppService()
    {
        LocalizationResource = typeof(SysResource);
    }
}
