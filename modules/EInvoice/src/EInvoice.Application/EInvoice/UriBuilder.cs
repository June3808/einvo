using EInvoice.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace EInvoice.EInvoice
{
    public class EInvoiceUriBuilder: ApplicationService, IEInvoiceUriBuilder
    {
        private ISettingProvider _settingRepository;

        public EInvoiceUriBuilder(ISettingProvider settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public async virtual Task<string> BaseUri() {

            var test = await _settingRepository.GetOrNullAsync(EInvoiceSettings.EInvoice.URLAddress);

            if (test == null)
            {
                return "";
            }

            return test + "/api/v1.0/";
        }

        public async Task<string> CancelOrRejectDocUri()
        {
            return await BaseUri() + "documents/state/";
        }

        public async Task<string> SubmitDocumentUri()
        { 
            return  await BaseUri()+ "documentsubmissions";
        }

        public async Task<string> GetRecentDocUri()
        {
            return await BaseUri() + "recent";
        }

        public async Task<string> GetSubmissionUri()
        {
            return await BaseUri() + "documentsubmissions";
        }

        public async Task<string> GetDocDetailsUri()
        {
            return await BaseUri() + "documents";
        }

        public async Task<string> SearchDocUri()
        {
            return await BaseUri() + "documents/search";
        }
    }
}
