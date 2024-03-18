using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos
{
    public class GetDashboardOutputFilter
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
