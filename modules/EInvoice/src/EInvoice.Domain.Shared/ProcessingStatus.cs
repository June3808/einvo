using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice
{
    public enum ProcessingStatus
    {
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InboundImported = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        InboundSuccessful = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        InboundFailed = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        OutboundSuccessful = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        OutboundFailed = 4,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Error = 5,
    }
}
