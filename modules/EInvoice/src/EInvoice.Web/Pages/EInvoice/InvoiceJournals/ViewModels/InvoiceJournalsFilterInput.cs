using System.ComponentModel.DataAnnotations;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EInvoice.Web.Pages.EInvoice.InvoiceJournals.ViewModels
{
    public class InvoiceJournalsFilterInput
    {
        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsSupplierName")]
        public string? SupplierName { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsSupplierTIN")]
        public string? SupplierTIN { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsSupplierIdentificationNo")]
        public string? SupplierIdentificationNo { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsSupplierSSTRegistrationNo")]
        public string? SupplierSSTRegistrationNo { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsSupplierTourismTaxRegistrationNo")]
        public string? SupplierTourismTaxRegistrationNo { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsSupplierEmail")]
        public string? SupplierEmail { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsSupplierMSICCode")]
        public string? SupplierMSICCode { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsSupplierBizActivityDesc")]
        public string? SupplierBizActivityDesc { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsSupplierAddress")]
        public string? SupplierAddress { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsSupplierContactNo")]
        public string? SupplierContactNo { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsBuyerName")]
        public string? BuyerName { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsBuyerTIN")]
        public string? BuyerTIN { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsBuyerIdentificationNo")]
        public string? BuyerIdentificationNo { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsBuyerSSTRegistrationNo")]
        public string? BuyerSSTRegistrationNo { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsBuyerEmail")]
        public string? BuyerEmail { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsBuyerAddress")]
        public string? BuyerAddress { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsBuyerContactNo")]
        public string? BuyerContactNo { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsEInvoiceVersion")]
        public string? EInvoiceVersion { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsEInvoiceType")]
        public string? EInvoiceType { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsEInvoiceCode")]
        public string? EInvoiceCode { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsEInvoiceOriginalReferNo")]
        public string? EInvoiceOriginalReferNo { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsEInvoiceDateTime")]
        public DateTime? EInvoiceDateTime { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsEInvoiceValidationDateTime")]
        public DateTime? EInvoiceValidationDateTime { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsIssuerDigitalSignature")]
        public string? IssuerDigitalSignature { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsCurrencyCode")]
        public string? CurrencyCode { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsCurrencyExchangeRate")]
        public decimal? CurrencyExchangeRate { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsFrequencyOfBilling")]
        public string? FrequencyOfBilling { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsBillingPeriod")]
        public string? BillingPeriod { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsIRBMUniqueIdentifierNo")]
        public string? IRBMUniqueIdentifierNo { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsClassification")]
        public string? Classification { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsProductServiceDesc")]
        public string? ProductServiceDesc { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsUnitPrice")]
        public decimal? UnitPrice { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsTaxType")]
        public string? TaxType { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsTaxRate")]
        public decimal? TaxRate { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsTaxAmount")]
        public decimal? TaxAmount { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsTaxExemptionDetail")]
        public string? TaxExemptionDetail { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsTaxExemptedAmount")]
        public decimal? TaxExemptedAmount { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsSubTotal")]
        public decimal? SubTotal { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsTotalExcludingTax")]
        public decimal? TotalExcludingTax { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsTotalIncludingTax")]
        public decimal? TotalIncludingTax { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsQuantity")]
        public decimal? Quantity { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsMeasurement")]
        public string? Measurement { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsDiscountRate")]
        public decimal? DiscountRate { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsDiscountAmount")]
        public decimal? DiscountAmount { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsPaymentMode")]
        public string? PaymentMode { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsSupplierBankAccountNo")]
        public string? SupplierBankAccountNo { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsPaymentTerms")]
        public string? PaymentTerms { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsPaymentAmount")]
        public decimal? PaymentAmount { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsPaymentDate")]
        public DateTime? PaymentDate { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsPaymentReferNo")]
        public string? PaymentReferNo { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsBillReferNo")]
        public string? BillReferNo { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsShippingRecipientName")]
        public string? ShippingRecipientName { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsShippingRecipientAddress")]
        public string? ShippingRecipientAddress { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsShippingRecipientTIN")]
        public string? ShippingRecipientTIN { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsShippingRecipientIdentification")]
        public string? ShippingRecipientIdentification { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsCustomsForm1ReferenceNumber")]
        public string? CustomsForm1ReferenceNumber { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsIncoterms")]
        public string? Incoterms { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsProductTariffCode")]
        public string? ProductTariffCode { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsFTAInformation")]
        public string? FTAInformation { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsAuthorisationNo")]
        public string? AuthorisationNo { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsCustomsForm2ReferenceNumber")]
        public string? CustomsForm2ReferenceNumber { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsCountryOfOrigin")]
        public string? CountryOfOrigin { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "InvoiceJournalsOtherCharges")]
        public string? OtherCharges { get; set; }
    }
}
