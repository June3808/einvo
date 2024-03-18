using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos.IRBM
{
    public class SubmissionDto
    {
        public Guid Id { get; set; }
        public string Uuid {  get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }

        public DocumentDto[] Documents { get; set; }
    }
}
