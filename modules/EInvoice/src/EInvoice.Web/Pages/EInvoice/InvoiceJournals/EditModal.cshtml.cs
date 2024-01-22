using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EInvoice;
using EInvoice.Dtos;
using EInvoice.Web.Pages.EInvoice.EInvoice.InvoiceJournals.ViewModels;

namespace EInvoice.Web.Pages.EInvoice.EInvoice.InvoiceJournals;

public class EditModalModel : EInvoicePageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateEditInvoiceJournalsViewModel ViewModel { get; set; }

    private readonly IInvoiceJournalsAppService _service;

    public EditModalModel(IInvoiceJournalsAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<InvoiceJournalsDto, CreateEditInvoiceJournalsViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditInvoiceJournalsViewModel, CreateUpdateInvoiceJournalsDto>(ViewModel);
        await _service.UpdateAsync(Id, dto);
        return NoContent();
    }
}