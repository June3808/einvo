using EInvoice.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EInvoice.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class EInvoicePageModel : AbpPageModel
{
    protected EInvoicePageModel()
    {
        LocalizationResourceType = typeof(EInvoiceResource);
        ObjectMapperContext = typeof(EInvoiceWebModule);
    }
}
