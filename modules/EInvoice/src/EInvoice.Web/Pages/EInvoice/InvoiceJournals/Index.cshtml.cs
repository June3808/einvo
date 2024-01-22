using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.FileManagement.Files.Dtos;
using EasyAbp.FileManagement.Files;
using EInvoice.Web.Pages.EInvoice.InvoiceJournals.ViewModels;

namespace EInvoice.Web.Pages.EInvoice.EInvoice.InvoiceJournals;

public class IndexModel : EInvoicePageModel
{
    public InvoiceJournalsFilterInput InvoiceJournalsFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public FileUploadViewModel ViewModel { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid? ParentId { get; set; }

    public FileInfoDto CurrentDirectory { get; set; }

    //private readonly IFileAppService _service;

    public IndexModel(
        //IFileAppService service
        )
    {
        //_service = service;
    }

    public virtual async Task OnGetAsync()
    {
        //if (ParentId.HasValue)
        //{
        //    CurrentDirectory = await _service.GetAsync(ParentId.Value);
        //}
    }
}
