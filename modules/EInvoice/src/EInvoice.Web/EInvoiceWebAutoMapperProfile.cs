using EInvoice.Dtos;
using EInvoice.Web.Pages.EInvoice.EInvoice.InvoiceJournals.ViewModels;
using AutoMapper;

namespace EInvoice.Web;

public class EInvoiceWebAutoMapperProfile : Profile
{
    public EInvoiceWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<InvoiceJournalsDto, CreateEditInvoiceJournalsViewModel>();
        CreateMap<CreateEditInvoiceJournalsViewModel, CreateUpdateInvoiceJournalsDto>();
    }
}
