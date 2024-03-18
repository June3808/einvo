using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos.IRBM
{
    public class RejectedDocumentsDto
    {
        public string InvoiceCodeNumber {  get; set; }

        public ErrorResponse Error { get; set; }
    }
}
