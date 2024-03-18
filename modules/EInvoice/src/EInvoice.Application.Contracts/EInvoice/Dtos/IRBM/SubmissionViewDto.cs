using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos.IRBM
{
    public class SubmissionViewDto
    {
        public string SubmissionUid { get; set; }
        public int DocumentCount {  get; set; }
        public DateTime DateTimeReceived { get; set; }
        public string OverallStatus { get; set; }
        public List<SubmissionViewDto> DocumentSummary { get; set; }

    }
}
