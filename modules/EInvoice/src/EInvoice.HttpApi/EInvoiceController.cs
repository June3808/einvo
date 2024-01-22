using EInvoice.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EInvoice;

public abstract class EInvoiceController : AbpControllerBase
{
    protected EInvoiceController()
    {
        LocalizationResource = typeof(EInvoiceResource);
    }
}
