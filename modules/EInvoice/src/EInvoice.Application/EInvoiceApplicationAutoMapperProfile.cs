using EInvoice;
using EInvoice.Dtos;
using AutoMapper;

namespace EInvoice;

public class EInvoiceApplicationAutoMapperProfile : Profile
{
    public EInvoiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<InvoiceJournals, InvoiceJournalsDto>();
        CreateMap<CreateUpdateInvoiceJournalsDto, InvoiceJournals>(MemberList.Source);
    }
}
