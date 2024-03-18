using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos.IRBM
{
    public class SeachDocumentViewDto
    {
        public string Uuid { get; set; }
        public string SubmissionUUID { get; set; }
        public string LongId { get; set; }
        public string InternalId { get; set; }
        public string TypeName { get; set; }
        public string TypeVersionName { get; set; }
        public string IssuerTin { get; set; }
        public string IssuerName { get; set; }
        public string ReceiverId { get; set; }
        public string receiverIdType { get; set; }
        public string ReceiverName { get; set; }
        public DateTime DateTimeIssued { get; set; }
        public DateTime DateTimeReceived { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime CancelDateTime { get; set; }
        public DateTime RejectRequestDateTime { get; set; }
        public string DocumentStatusReason { get; set;}
        public string CreatedByUserId { get; set; }
    }
}
