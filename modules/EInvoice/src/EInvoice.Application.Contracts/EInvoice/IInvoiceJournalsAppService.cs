using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.FileManagement.Files.Dtos;
using EInvoice.Dtos;
using EInvoice.EInvoice.Dtos;
using EInvoice.EInvoice.Dtos.BC;
using EInvoice.EInvoice.Dtos.IRBM;
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

    Task syncSalesInvoice(BCSalesInvoiceSyncDto sales);

    Task syncCreditMemo(BCSalesInvoiceSyncDto sales);

    Task syncSalesReturn(BCSalesInvoiceSyncDto sales);

    Task<List<InvoiceJournalsDto>> PostSalesInvoice();

    Task CancelDocument(SubmissionDto dto);

    Task RejectDocument(SubmissionDto dto);

    Task SubmitDocument(Guid id);

    Task<List<int>> MonthlyInvoiceCount();

    Task<decimal> MonthlyInvoiceSum();

    Task<decimal> YearlyInvoiceSum();

    /// <summary>
    /// invoice inbound from ERP
    /// </summary>
    /// <returns></returns>
    Task<GetDashboardOutputDto> InboundCount(GetDashboardOutputFilter filter);

    /// <summary>
    /// invoice outbound to IRBM
    /// </summary>
    /// <returns></returns>
    Task<GetDashboardOutputDto> OutboundCount(GetDashboardOutputFilter filter);

}