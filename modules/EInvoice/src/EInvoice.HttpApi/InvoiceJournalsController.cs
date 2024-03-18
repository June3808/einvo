using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.FileManagement.Files.Dtos;
using EInvoice.Dtos;
using EInvoice.EInvoice.Dtos;
using EInvoice.EInvoice.Dtos.BC;
using EInvoice.EInvoice.Dtos.IRBM;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EInvoice
{
    [Area(EInvoiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = EInvoiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/EInvoice/InvoiceJournals")]
    public class InvoiceJournalsController : EInvoiceController, IInvoiceJournalsAppService
    {
        private readonly IInvoiceJournalsAppService _invoiceJournalsAppService;

        public InvoiceJournalsController(IInvoiceJournalsAppService invoiceJournalsAppService)
        { 
            _invoiceJournalsAppService = invoiceJournalsAppService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<InvoiceJournalsDto> CreateAsync(CreateUpdateInvoiceJournalsDto input)
        {
            return await _invoiceJournalsAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("importFiles")]
        [Authorize]
        public Task<CreateManyFileOutput> CreateManyWithStreamAsync(CreateManyFileWithStreamInput input)
        {
            return _invoiceJournalsAppService.CreateManyWithStreamAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize]
        public async Task DeleteAsync(Guid id)
        {
            await _invoiceJournalsAppService.DeleteAsync(id);
        }


        [HttpGet]
        [Route("get")]
        [Authorize]
        public async Task<InvoiceJournalsDto> GetAsync(Guid id)
        {
            return await _invoiceJournalsAppService.GetAsync(id);
        }


        [HttpPost]
        [Route("getList")]
        [Authorize]
        public async Task<PagedResultDto<InvoiceJournalsDto>> GetListAsync(InvoiceJournalsGetListInput input)
        {
            return await _invoiceJournalsAppService.GetListAsync(input);
        }

        [HttpPost]
        [Route("syncSalesInvoice")]
        [Authorize]
        public async Task syncSalesInvoice(BCSalesInvoiceSyncDto data)
        {
            await _invoiceJournalsAppService.syncSalesInvoice(data);
        }

        [HttpPost]
        [Route("syncCreditMemo")]
        [Authorize]
        public async Task syncCreditMemo(BCSalesInvoiceSyncDto data)
        {
            await _invoiceJournalsAppService.syncCreditMemo(data);
        }

        [HttpPost]
        [Route("syncSalesReturn")]
        [Authorize]
        public async Task syncSalesReturn(BCSalesInvoiceSyncDto data)
        {
            await _invoiceJournalsAppService.syncSalesReturn(data);
        }

        /// <summary>
        /// Example for Get request for dynamic return format(XML,JSON)
        /// </summary>
        /// <returns></returns>
        [HttpGet("getSalesInvoice/get.{format}"), FormatFilter]
        public async Task<List<InvoiceJournalsDto>> getSalesInvoice()
        {
            return await _invoiceJournalsAppService.PostSalesInvoice();
        }


        [HttpPost]
        [Route("postSalesInvoice")]
        [Authorize]
        public Task<List<InvoiceJournalsDto>> PostSalesInvoice()
        {
            return _invoiceJournalsAppService.PostSalesInvoice();
        }

        /// <summary>
        /// Example for Post request for dynamic return format(XML,JSON)
        /// </summary>
        /// <returns></returns>
        [HttpPost("postSalesInvoice/post.{format}"), FormatFilter]
        [AllowAnonymous]
        public async Task<List<InvoiceJournalsDto>> postSalesInvoice(List<InvoiceJournalsDto> test)
        {
            if(ModelState.IsValid)
            return test;

            return new List<InvoiceJournalsDto>();
        }

        [HttpPost]
        [Route("update")]
        [Authorize]
        public async Task<InvoiceJournalsDto> UpdateAsync(Guid id, CreateUpdateInvoiceJournalsDto input)
        {
            return await _invoiceJournalsAppService.UpdateAsync(id, input);
        }

        [HttpPost]
        [Route("CancelDocument")]
        [Authorize]
        public async Task CancelDocument(SubmissionDto dto)
        {
            await _invoiceJournalsAppService.CancelDocument(dto);
        }

        [HttpPost]
        [Route("RejectDocument")]
        [Authorize]
        public Task RejectDocument(SubmissionDto dto)
        {
            return _invoiceJournalsAppService.RejectDocument(dto);
        }

        [HttpPost]
        [Route("SubmitDocument")]
        [Authorize]
        public Task SubmitDocument(Guid id)
        {
            return _invoiceJournalsAppService.SubmitDocument(id);
        }

        [HttpPost]
        [Route("monthlyInvoiceCount")]
        [Authorize]
        public Task<List<int>> MonthlyInvoiceCount()
        {
            return _invoiceJournalsAppService.MonthlyInvoiceCount();
        }

        [HttpPost]
        [Route("monthlyInvoiceSum")]
        [Authorize]
        public Task<decimal> MonthlyInvoiceSum()
        {
            return _invoiceJournalsAppService.MonthlyInvoiceSum();
        }
		
        [HttpPost]
        [Route("yearlyInvoiceSum")]
        [Authorize]
        public Task<decimal> YearlyInvoiceSum()
        {
            return _invoiceJournalsAppService.YearlyInvoiceSum();
        }

        [HttpPost]
        [Route("inboundCount")]
        [Authorize]
        public Task<GetDashboardOutputDto> InboundCount(GetDashboardOutputFilter filter)
        {
            return _invoiceJournalsAppService.InboundCount(filter);
        }

        [HttpPost]
        [Route("outboundCount")]
        [Authorize]
        public Task<GetDashboardOutputDto> OutboundCount(GetDashboardOutputFilter filter)
        {
            return _invoiceJournalsAppService.OutboundCount(filter);
        }

    }
}
