using Sys.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Sys.Web.Pages;

public abstract class SysPageModel : AbpPageModel
{
    protected SysPageModel()
    {
        LocalizationResourceType = typeof(SysResource);
    }
}
