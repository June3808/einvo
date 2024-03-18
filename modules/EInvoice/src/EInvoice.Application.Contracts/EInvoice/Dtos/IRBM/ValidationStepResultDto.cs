using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos.IRBM
{
    public class ValidationStepResultDto
    {
        public string Name {  get; set; }
        public string Status { get; set; }
        public string Error { get; set; }
    }
}
