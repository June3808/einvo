using System;
using System.Collections.Generic;
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

    void test(dynamic obj);

    Task<List<int>> MonthlyInvoiceCount();

    Task<decimal> MonthlyInvoiceSum();

    Task<decimal> YearlyInvoiceSum();
}