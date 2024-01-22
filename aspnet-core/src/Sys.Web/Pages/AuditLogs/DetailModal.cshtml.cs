using Sys.Web.Pages.AuditLogs.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sys.Web.Pages.AuditLogs
{
    public class DetailModalModel : PageModel
    {
        [BindProperty]
        public DetailViewModel SystemLog { get; set; }

        public void OnGet()
        {
        }
    }
}
