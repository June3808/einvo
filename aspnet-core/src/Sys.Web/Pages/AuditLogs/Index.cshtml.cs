using Sys.Web.Pages.AuditLogs.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sys.Web.Pages.AuditLogs
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public QueryViewModel QueryModel { get; set; }

        public void OnGet()
        {
        }
    }
}
