using System;
using EasyAbp.Abp.TagHelperPlus.EasySelector;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EInvoice.Web.Pages.EInvoice.InvoiceJournals.ViewModels
{
    public class FileUploadViewModel
    {
        [Placeholder("e.g. default")]
        public string FileContainerName { 
            get
            {
                return "default";
            }
        }
        
        //[EasySelector(
        //    getListedDataSourceUrl: "/api/identity/users",
        //    getSingleDataSourceUrl: "/api/identity/users/{id}",
        //    keyPropertyName: "id",
        //    textPropertyName: "name",
        //    alternativeTextPropertyName: "userName",
        //    hideSubText: false,
        //    runScriptOnWindowLoad: true)]
        public Guid? OwnerUserId { get; set; }
    }
}