using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos
{
    public enum GenJournalDocType
    {
        None = 0,
        Payment = 1,
        Invoice = 2,
        CreditMemo = 3,
        FinanceChargeMemo = 4,
        Reminder = 5,
        Refund = 6
    }
}
