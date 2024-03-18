using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos.IRBM
{
    public class DocumentValidationResultsDto
    {
        /// <summary>
        /// Validation status. Values: Submitted, Valid, Invalid
        /// </summary>
        public string Status {  get; set; }
        public List<ValidationStepResultDto> ValidationSteps { get; set; }
        public ErrorResponse Error {  get; set; }
    }
}
