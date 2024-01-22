using System;
using System.Threading.Tasks;
using EasyAbp.FileManagement.Files.Dtos;
using EInvoice.Dtos;
using Volo.Abp.Application.Services;

namespace EInvoice;


public interface IInvoiceJournalsAppService :
    ICrudAppService< 
        InvoiceJournalsDto, 
        Guid, 
        InvoiceJournalsGetListInput,
        CreateUpdateInvoiceJournalsDto,
        CreateUpdateInvoiceJournalsDto>
{

    Task<CreateManyFileOutput> CreateManyWithStreamAsync(CreateManyFileWithStreamInput input);

}