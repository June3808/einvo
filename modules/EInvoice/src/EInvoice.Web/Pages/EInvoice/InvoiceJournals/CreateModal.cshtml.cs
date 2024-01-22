using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EInvoice;
using EInvoice.Dtos;
using EInvoice.Web.Pages.EInvoice.EInvoice.InvoiceJournals.ViewModels;

namespace EInvoice.Web.Pages.EInvoice.EInvoice.InvoiceJournals;

public class CreateModalModel : EInvoicePageModel
{
    [BindProperty]
    public CreateEditInvoiceJournalsViewModel ViewModel { get; set; }

    private readonly IInvoiceJournalsAppService _service;

    public CreateModalModel(IInvoiceJournalsAppService service)
    {
        _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditInvoiceJournalsViewModel, CreateUpdateInvoiceJournalsDto>(ViewModel);
        await _service.CreateAsync(dto);
        return NoContent();
    }
}