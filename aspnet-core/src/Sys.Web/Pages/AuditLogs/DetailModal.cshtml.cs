using Sys.AuditLogs;
using Sys.Web.Pages.AuditLogs.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using TaskScheduler.TaskSchedulers;
using TaskScheduler.TaskSchedulers.Dtos;

namespace Sys.Web.Pages.AuditLogs
{
    public class DetailModalModel : PageModel
    {
        private readonly IAuditLogAppService _auditLogAppService;

        public DetailModalModel(IAuditLogAppService auditLogAppService)
        {
            _auditLogAppService = auditLogAppService;
        }

        [BindProperty]
        public DetailViewModel SystemLog { get; set; }

        [BindProperty]
        public AuditLogDto AuditDto { get; set; }

        public async Task OnGet(Guid id)
        {
            AuditDto = await _auditLogAppService.GetAsync(id);
        }
    }
}
