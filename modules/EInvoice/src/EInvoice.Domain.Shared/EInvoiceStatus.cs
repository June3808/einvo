using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice
{
    public enum EInvoiceStatus
    {
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Pending = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        InProgress = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Approved = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Rejected = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Failed = 4,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ManualRecon = 5,
    }
}
