using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos.BC
{
    public class BCSalesReturnSyncDto
    {
        public BCSalesInvoiceDto salesInvoice { get; set; }
        public BCCompanyInfo companyInfo { get; set; }
    }
}
