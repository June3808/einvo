using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.EInvoice
{
    public interface IEInvoiceUriBuilder
    {
        Task<string> BaseUri();
        Task<string> CancelOrRejectDocUri();
        Task<string> GetRecentDocUri();
        Task<string> GetSubmissionUri();
        Task<string> GetDocDetailsUri();
        Task<string> SearchDocUri();
    }
}
