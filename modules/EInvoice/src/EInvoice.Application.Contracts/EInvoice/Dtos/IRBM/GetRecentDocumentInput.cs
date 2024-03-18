using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos.IRBM
{
    public class GetRecentDocumentInput
    {
        public int PageNo { get; set; }
        public int PageSize {  get; set; }
        public DateTime? SubmissionDateFrom { get; set; }
        public DateTime? SubmissionDateTo { get; set; }
        public DateTime? IssueDateFrom { get; set; }
        public DateTime? IssueDateTo { get;set; }
        public string Direction { get; set; }
        public string Status { get; set; }
        public string DocumentType { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverIdType { get; set; }
        public string ReceiverTin { get; set; }
        public string IssuerTin { get; set; }

    }
}
