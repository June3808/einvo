using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice
{
    public enum ProcessingStatus
    {
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Imported = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Ready = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        InProgress = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Completed = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Error = 4,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        OnHold = 5,

        //[System.Runtime.Serialization.EnumMemberAttribute()]
        //OutOfPeriod = 6,
    }
}
