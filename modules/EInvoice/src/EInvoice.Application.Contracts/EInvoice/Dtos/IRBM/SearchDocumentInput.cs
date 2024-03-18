using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos.IRBM
{
    public class SearchDocumentInput
    {
        public string Uuid { get; set; }
        public DateTime SubmissionDateFrom { get; set; }
        public DateTime SubmissionDateTo { get; set; }
        public string ContinuationToken { get; set; }
        public int PageSize { get;}
        public DateTime IssueDateFrom { get; set; }
        public DateTime IissueDateTo { get; set; }
        public string Direction { get; set; }

        //Optional: status of the document. Possible values: (Valid, Invalid, Cancelled, Submitted)
        public string Status { get; set; }
        public string DocumentType { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverIdType { get; set; }
        public string IssuerTin { get; set; }

    }
}
