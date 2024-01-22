using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.AuditLogs
{
    public class GetAverageExecutionDurationPerDayInput
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
