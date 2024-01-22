using Sys.Tenants.Dashboard.Dto;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Sys.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        Task<GetMemberActivityOutput> GetMemberActivity();

        Task<GetDashboardDataOutput> GetDashboardData(GetDashboardDataInput input);

        Task<GetSalesSummaryOutput> GetSalesSummary(GetSalesSummaryInput input);

        Task<GetWorldMapOutput> GetWorldMap(GetWorldMapInput input);

        Task<GetGeneralStatsOutput> GetGeneralStats(GetGeneralStatsInput input);
    }
}
