using System;
using System.Threading.Tasks;
using EasyAbp.FileManagement.Files.Dtos;
using EInvoice.Dtos;
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
        [Route("update")]
        [Authorize]
        public async Task<InvoiceJournalsDto> UpdateAsync(Guid id, CreateUpdateInvoiceJournalsDto input)
        {
            return await _invoiceJournalsAppService.UpdateAsync(id, input);
        }
    }
}
