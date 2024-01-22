using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EInvoice.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
