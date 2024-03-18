﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos
{
    public enum SalesDocumentType
    {
        Quote = 0,
        Order = 1,
        Invoice = 2,
        CreditMemo = 3,
        BlanketOrder = 4,
        ReturnOrder = 5,
    }
}
