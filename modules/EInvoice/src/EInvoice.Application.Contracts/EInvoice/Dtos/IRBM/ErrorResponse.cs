using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos.IRBM
{
    public class ErrorResponse
    {
        public string Code {  get; set; }
        public string Message { get; set; }
        public string Target {  get; set; }
        public ErrorResponse Details {  get; set; }
    }
}
