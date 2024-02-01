using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using Sys.Controllers;
using Volo.Abp.AspNetCore.Mvc.ApiExploring;
using Volo.Abp.Identity;

namespace Sys.AuditLogs
{
    [RemoteService(Name = "abp")]
    [Route("/api/logging-management/[action]")]
    public class AuditLogController : SysController
    {
        private readonly IAuditLogAppService _service;

        public AuditLogController(
            IAuditLogAppService _service
            )
        {
            this._service = _service;
        }

        [HttpGet]
        public async Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLogListDto input)
        {
            return await _service.GetListAsync(input);
        }

        [HttpGet]
        public async Task<GetErrorRateOutput> GetErrorRateAsync(GetErrorRateFilter filter)
        {
            return await _service.GetErrorRateAsync(filter);
        }

        [HttpGet]
        public async Task<ListResultDto<IdentityUserDto>> GetActiveUsersAsync(GetErrorRateFilter filter)
        {
            return await _service.GetActiveUsersAsync(filter);
        }
    }
}
