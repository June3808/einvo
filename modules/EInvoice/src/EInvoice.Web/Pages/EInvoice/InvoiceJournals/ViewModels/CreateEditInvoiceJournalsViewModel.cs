using System;
using System.ComponentModel.DataAnnotations;

namespace EInvoice.Web.Pages.EInvoice.EInvoice.InvoiceJournals.ViewModels;

public class CreateEditInvoiceJournalsViewModel
{
    [Display(Name = "InvoiceJournalsSupplierName")]
    public string SupplierName { get; set; }

    [Display(Name = "InvoiceJournalsSupplierTIN")]
    public string SupplierTIN { get; set; }

    [Display(Name = "InvoiceJournalsSupplierIdentificationNo")]
    public string SupplierIdentificationNo { get; set; }

    [Display(Name = "InvoiceJournalsSupplierSSTRegistrationNo")]
    public string? SupplierSSTRegistrationNo { get; set; }

    [Display(Name = "InvoiceJournalsSupplierTourismTaxRegistrationNo")]
    public string? SupplierTourismTaxRegistrationNo { get; set; }

    [Display(Name = "InvoiceJournalsSupplierEmail")]
    public string SupplierEmail { get; set; }

    [Display(Name = "InvoiceJournalsSupplierMSICCode")]
    public string SupplierMSICCode { get; set; }

    [Display(Name = "InvoiceJournalsSupplierBizActivityDesc")]
    public string SupplierBizActivityDesc { get; set; }

    [Display(Name = "InvoiceJournalsSupplierAddress")]
    public string SupplierAddress { get; set; }

    [Display(Name = "InvoiceJournalsSupplierContactNo")]
    public string SupplierContactNo { get; set; }

    [Display(Name = "InvoiceJournalsBuyerName")]
    public string BuyerName { get; set; }

    [Display(Name = "InvoiceJournalsBuyerTIN")]
    public string BuyerTIN { get; set; }

    [Display(Name = "InvoiceJournalsBuyerIdentificationNo")]
    public string BuyerIdentificationNo { get; set; }

    [Display(Name = "InvoiceJournalsBuyerSSTRegistrationNo")]
    public string? BuyerSSTRegistrationNo { get; set; }

    [Display(Name = "InvoiceJournalsBuyerEmail")]
    public string BuyerEmail { get; set; }

    [Display(Name = "InvoiceJournalsBuyerAddress")]
    public string BuyerAddress { get; set; }

    [Display(Name = "InvoiceJournalsBuyerContactNo")]
    public string BuyerContactNo { get; set; }

    [Display(Name = "InvoiceJournalsEInvoiceVersion")]
    public string EInvoiceVersion { get; set; }

    [Display(Name = "InvoiceJournalsEInvoiceType")]
    public string EInvoiceType { get; set; }

    [Display(Name = "InvoiceJournalsEInvoiceCode")]
    public string EInvoiceCode { get; set; }

    [Display(Name = "InvoiceJournalsEInvoiceOriginalReferNo")]
    public string EInvoiceOriginalReferNo { get; set; }

    [Display(Name = "InvoiceJournalsEInvoiceDateTime")]
    public DateTime EInvoiceDateTime { get; set; }

    [Display(Name = "InvoiceJournalsEInvoiceValidationDateTime")]
    public DateTime EInvoiceValidationDateTime { get; set; }

    [Display(Name = "InvoiceJournalsIssuerDigitalSignature")]
    public string IssuerDigitalSignature { get; set; }

    [Display(Name = "InvoiceJournalsCurrencyCode")]
    public string CurrencyCode { get; set; }

    [Display(Name = "InvoiceJournalsCurrencyExchangeRate")]
    public decimal CurrencyExchangeRate { get; set; }

    [Display(Name = "InvoiceJournalsFrequencyOfBilling")]
    public string FrequencyOfBilling { get; set; }

    [Display(Name = "InvoiceJournalsBillingPeriod")]
    public string BillingPeriod { get; set; }

    [Display(Name = "InvoiceJournalsIRBMUniqueIdentifierNo")]
    public string IRBMUniqueIdentifierNo { get; set; }

    [Display(Name = "InvoiceJournalsClassification")]
    public string Classification { get; set; }

    [Display(Name = "InvoiceJournalsProductServiceDesc")]
    public string ProductServiceDesc { get; set; }

    [Display(Name = "InvoiceJournalsUnitPrice")]
    public decimal UnitPrice { get; set; }

    [Display(Name = "InvoiceJournalsTaxType")]
    public string TaxType { get; set; }

    [Display(Name = "InvoiceJournalsTaxRate")]
    public decimal TaxRate { get; set; }

    [Display(Name = "InvoiceJournalsTaxAmount")]
    public decimal TaxAmount { get; set; }

    [Display(Name = "InvoiceJournalsTaxExemptionDetail")]
    public string? TaxExemptionDetail { get; set; }

    [Display(Name = "InvoiceJournalsTaxExemptedAmount")]
    public decimal? TaxExemptedAmount { get; set; }

    [Display(Name = "InvoiceJournalsSubTotal")]
    public decimal SubTotal { get; set; }

    [Display(Name = "InvoiceJournalsTotalExcludingTax")]
    public decimal TotalExcludingTax { get; set; }

    [Display(Name = "InvoiceJournalsTotalIncludingTax")]
    public decimal TotalIncludingTax { get; set; }

    [Display(Name = "InvoiceJournalsQuantity")]
    public decimal? Quantity { get; set; }

    [Display(Name = "InvoiceJournalsMeasurement")]
    public string? Measurement { get; set; }

    [Display(Name = "InvoiceJournalsDiscountRate")]
    public decimal? DiscountRate { get; set; }

    [Display(Name = "InvoiceJournalsDiscountAmount")]
    public decimal? DiscountAmount { get; set; }

    [Display(Name = "InvoiceJournalsPaymentMode")]
    public string? PaymentMode { get; set; }

    [Display(Name = "InvoiceJournalsSupplierBankAccountNo")]
    public string? SupplierBankAccountNo { get; set; }

    [Display(Name = "InvoiceJournalsPaymentTerms")]
    public string? PaymentTerms { get; set; }

    [Display(Name = "InvoiceJournalsPaymentAmount")]
    public decimal? PaymentAmount { get; set; }

    [Display(Name = "InvoiceJournalsPaymentDate")]
    public DateTime? PaymentDate { get; set; }

    [Display(Name = "InvoiceJournalsPaymentReferNo")]
    public string? PaymentReferNo { get; set; }

    [Display(Name = "InvoiceJournalsBillReferNo")]
    public string? BillReferNo { get; set; }

    [Display(Name = "InvoiceJournalsShippingRecipientName")]
    public string ShippingRecipientName { get; set; }

    [Display(Name = "InvoiceJournalsShippingRecipientAddress")]
    public string ShippingRecipientAddress { get; set; }

    [Display(Name = "InvoiceJournalsShippingRecipientTIN")]
    public string ShippingRecipientTIN { get; set; }

    [Display(Name = "InvoiceJournalsShippingRecipientIdentification")]
    public string ShippingRecipientIdentification { get; set; }

    [Display(Name = "InvoiceJournalsCustomsForm1ReferenceNumber")]
    public string CustomsForm1ReferenceNumber { get; set; }

    [Display(Name = "InvoiceJournalsIncoterms")]
    public string Incoterms { get; set; }

    [Display(Name = "InvoiceJournalsProductTariffCode")]
    public string? ProductTariffCode { get; set; }

    [Display(Name = "InvoiceJournalsFTAInformation")]
    public string? FTAInformation { get; set; }

    [Display(Name = "InvoiceJournalsAuthorisationNo")]
    public string? AuthorisationNo { get; set; }

    [Display(Name = "InvoiceJournalsCustomsForm2ReferenceNumber")]
    public string? CustomsForm2ReferenceNumber { get; set; }

    [Display(Name = "InvoiceJournalsCountryOfOrigin")]
    public string? CountryOfOrigin { get; set; }

    [Display(Name = "InvoiceJournalsOtherCharges")]
    public string? OtherCharges { get; set; }
}
