using EInvoice.Localization;
using Volo.Abp.Application.Services;

namespace EInvoice;

public abstract class EInvoiceAppService : ApplicationService
{
    protected EInvoiceAppService()
    {
        LocalizationResource = typeof(EInvoiceResource);
        ObjectMapperContext = typeof(EInvoiceApplicationModule);
    }
}
