using System;
using Volo.Abp.Application.Dtos;

namespace EInvoice.Dtos;

[Serializable]
public class InvoiceJournalsDto : EntityDto<Guid>
{
    public string SupplierName { get; set; }

    public string SupplierTIN { get; set; }

    public string SupplierIdentificationNo { get; set; }

    public string? SupplierSSTRegistrationNo { get; set; }

    public string? SupplierTourismTaxRegistrationNo { get; set; }

    public string SupplierEmail { get; set; }

    public string SupplierMSICCode { get; set; }

    public string SupplierBizActivityDesc { get; set; }

    public string SupplierAddress { get; set; }

    public string SupplierContactNo { get; set; }

    public string BuyerName { get; set; }

    public string BuyerTIN { get; set; }

    public string BuyerIdentificationNo { get; set; }

    public string? BuyerSSTRegistrationNo { get; set; }

    public string BuyerEmail { get; set; }

    public string BuyerAddress { get; set; }

    public string BuyerContactNo { get; set; }

    public string EInvoiceVersion { get; set; }

    public string EInvoiceType { get; set; }

    public string EInvoiceCode { get; set; }

    public string EInvoiceOriginalReferNo { get; set; }

    public DateTime EInvoiceDateTime { get; set; }

    public DateTime EInvoiceValidationDateTime { get; set; }

    public string IssuerDigitalSignature { get; set; }

    public string CurrencyCode { get; set; }

    public decimal CurrencyExchangeRate { get; set; }

    public string FrequencyOfBilling { get; set; }

    public string BillingPeriod { get; set; }

    public string IRBMUniqueIdentifierNo { get; set; }

    public string Classification { get; set; }

    public string ProductServiceDesc { get; set; }

    public decimal UnitPrice { get; set; }

    public string TaxType { get; set; }

    public decimal TaxRate { get; set; }

    public decimal TaxAmount { get; set; }

    public string? TaxExemptionDetail { get; set; }

    public decimal? TaxExemptedAmount { get; set; }

    public decimal SubTotal { get; set; }

    public decimal TotalExcludingTax { get; set; }

    public decimal TotalIncludingTax { get; set; }

    public decimal? Quantity { get; set; }

    public string? Measurement { get; set; }

    public decimal? DiscountRate { get; set; }

    public decimal? DiscountAmount { get; set; }

    public string? PaymentMode { get; set; }

    public string? SupplierBankAccountNo { get; set; }

    public string? PaymentTerms { get; set; }

    public decimal? PaymentAmount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? PaymentReferNo { get; set; }

    public string? BillReferNo { get; set; }

    public string ShippingRecipientName { get; set; }

    public string ShippingRecipientAddress { get; set; }

    public string ShippingRecipientTIN { get; set; }

    public string ShippingRecipientIdentification { get; set; }

    public string CustomsForm1ReferenceNumber { get; set; }

    public string Incoterms { get; set; }

    public string? ProductTariffCode { get; set; }

    public string? FTAInformation { get; set; }

    public string? AuthorisationNo { get; set; }

    public string? CustomsForm2ReferenceNumber { get; set; }

    public string? CountryOfOrigin { get; set; }

    public string? OtherCharges { get; set; }
}