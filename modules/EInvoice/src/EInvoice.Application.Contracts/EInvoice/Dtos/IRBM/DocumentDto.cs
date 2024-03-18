using System;
using System.Collections.Generic;
using System.Text;

namespace EInvoice.EInvoice.Dtos.IRBM
{
    public class DocumentDto
    {
        //refer https://sdk.myinvois.hasil.gov.my/einvoicingapi/02-submit-documents/#single-document-consists-of

        /// <summary>
        /// XML, JSON
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// The base64 of the document JSON or XML
        /// </summary>
        public string Document {  get; set; }

        /// <summary>
        /// The hash value of the document being submitted
        /// </summary>
        public string DocumentHash {  get; set; }

        /// <summary>
        /// Document reference number used by Supplier for internal tracking purpose
        /// eg: INV12345, CN23456, DN34567
        /// </summary>
        public string CodeNumber {  get; set; }
    }
}
