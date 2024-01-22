using Sys.Controllers;
using Sys.Tenants.Dashboard;
using Sys.Tenants.Dashboard.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Sys.Dashboard
{
    [RemoteService(Name = "abp")]
    [Route("/api/tenantDashboard/[action]")]
    public class TenantDashboardController :SysController, ITenantDashboardAppService
    {
        private readonly ITenantDashboardAppService _service;

        public TenantDashboardController(
            ITenantDashboardAppService _service
            )
        {
            this._service = _service;
        }

        [HttpGet]
        [Route("getDashboardData")]
        [Authorize]
        public async Task<GetDashboardDataOutput> GetDashboardData(GetDashboardDataInput input)
        {
            return await _service.GetDashboardData(input);
        }

        [HttpGet]
        [Route("getGeneralStats")]
        [Authorize]
        public async Task<GetGeneralStatsOutput> GetGeneralStats(GetGeneralStatsInput input)
        {
            return await _service.GetGeneralStats(input);
        }

        [HttpGet]
        [Route("getMemberActivity")]
        [Authorize]
        public async Task<GetMemberActivityOutput> GetMemberActivity()
        {
            var data = await _service.GetMemberActivity();
            return data;
        }

        [HttpGet]
        [Route("getSalesSummary")]
        [Authorize]
        public async Task<GetSalesSummaryOutput> GetSalesSummary(GetSalesSummaryInput input)
        {
            return await _service.GetSalesSummary(input);
        }

        [HttpGet]
        [Route("getWorldMap")]
        [Authorize]
        public async Task<GetWorldMapOutput> GetWorldMap(GetWorldMapInput input)
        {
            return await _service.GetWorldMap(input);
        }
    }
}
