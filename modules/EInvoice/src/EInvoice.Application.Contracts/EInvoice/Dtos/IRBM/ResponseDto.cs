using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos.IRBM
{
    public class ResponseDto
    {
        public string Uuid { get; set; }
        public string Status { get; set; }
        public string Error { get; set; }

        public string submissionUID { get { return Uuid; } set { this.Uuid = value;  } }
    }
}
