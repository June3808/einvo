using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace EInvoice
{
    public class InvoiceJournals:AggregateRoot<Guid>
    {
        public virtual string SupplierName { get; set; }
        public virtual string SupplierTIN { get; set; }
        public virtual string SupplierIdentificationNo { get; set; }
        public virtual string? SupplierSSTRegistrationNo { get; set; }
        public virtual string? SupplierTourismTaxRegistrationNo { get; set; }
        public virtual string SupplierEmail { get; set; }
        public virtual string SupplierMSICCode { get; set; }
        public virtual string SupplierBizActivityDesc { get; set; }
        public virtual string SupplierAddress { get; set; }
        public virtual string SupplierContactNo { get; set; }
        public virtual string BuyerName { get; set; }
        public virtual string BuyerTIN { get; set; }
        public virtual string BuyerIdentificationNo { get; set; }
        public virtual string? BuyerSSTRegistrationNo { get; set; }
        public virtual string BuyerEmail { get; set; }
        public virtual string BuyerAddress { get; set; }
        public virtual string BuyerContactNo { get; set; }
        public virtual string EInvoiceVersion { get; set; }
        public virtual string EInvoiceType { get; set; }
        public virtual string EInvoiceCode { get; set; }
        public virtual string EInvoiceOriginalReferNo { get; set; }
        public virtual DateTime EInvoiceDateTime { get; set; }
        public virtual DateTime EInvoiceValidationDateTime { get; set; }
        public virtual string IssuerDigitalSignature { get; set; }
        public virtual string CurrencyCode { get; set; }
        public virtual decimal CurrencyExchangeRate { get; set; }
        public virtual string FrequencyOfBilling { get; set; }
        public virtual string BillingPeriod { get; set; }
        public virtual string IRBMUniqueIdentifierNo { get; set; }
        public virtual string Classification { get; set; }
        public virtual string ProductServiceDesc { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual string TaxType { get; set; }
        public virtual decimal TaxRate { get; set; }
        public virtual decimal TaxAmount { get; set; }
        public virtual string? TaxExemptionDetail { get; set; }
        public virtual decimal? TaxExemptedAmount { get; set; }
        public virtual decimal SubTotal { get; set; }
        public virtual decimal TotalExcludingTax { get; set; }
        public virtual decimal TotalIncludingTax { get; set; }
        public virtual decimal? Quantity { get; set; }
        public virtual string? Measurement { get; set; }
        public virtual decimal? DiscountRate { get; set; }
        public virtual decimal? DiscountAmount { get; set; }
        public virtual string? PaymentMode { get; set; }
        public virtual string? SupplierBankAccountNo { get; set; }
        public virtual string? PaymentTerms { get; set; }
        public virtual decimal? PaymentAmount { get; set; }
        public virtual DateTime? PaymentDate { get; set; }
        public virtual string? PaymentReferNo { get; set; }
        public virtual string? BillReferNo { get; set; }
        public virtual string ShippingRecipientName { get; set; }
        public virtual string ShippingRecipientAddress { get; set; }
        public virtual string ShippingRecipientTIN { get; set; }
        public virtual string ShippingRecipientIdentification { get; set; }
        public virtual string CustomsForm1ReferenceNumber { get; set; }
        public virtual string Incoterms { get; set; }
        public virtual string? ProductTariffCode { get; set; }
        public virtual string? FTAInformation { get; set; }
        public virtual string? AuthorisationNo { get; set; }
        public virtual string? CustomsForm2ReferenceNumber { get; set; }
        public virtual string? CountryOfOrigin { get; set; }
        public virtual string? OtherCharges { get; set; }

    public InvoiceJournals()
    {
    }

    public InvoiceJournals(
        Guid id,
        string supplierName,
        string supplierTIN,
        string supplierIdentificationNo,
        string? supplierSSTRegistrationNo,
        string? supplierTourismTaxRegistrationNo,
        string supplierEmail,
        string supplierMSICCode,
        string supplierBizActivityDesc,
        string supplierAddress,
        string supplierContactNo,
        string buyerName,
        string buyerTIN,
        string buyerIdentificationNo,
        string? buyerSSTRegistrationNo,
        string buyerEmail,
        string buyerAddress,
        string buyerContactNo,
        string eInvoiceVersion,
        string eInvoiceType,
        string eInvoiceCode,
        string eInvoiceOriginalReferNo,
        DateTime eInvoiceDateTime,
        DateTime eInvoiceValidationDateTime,
        string issuerDigitalSignature,
        string currencyCode,
        decimal currencyExchangeRate,
        string frequencyOfBilling,
        string billingPeriod,
        string iRBMUniqueIdentifierNo,
        string classification,
        string productServiceDesc,
        decimal unitPrice,
        string taxType,
        decimal taxRate,
        decimal taxAmount,
        string? taxExemptionDetail,
        decimal? taxExemptedAmount,
        decimal subTotal,
        decimal totalExcludingTax,
        decimal totalIncludingTax,
        decimal? quantity,
        string? measurement,
        decimal? discountRate,
        decimal? discountAmount,
        string? paymentMode,
        string? supplierBankAccountNo,
        string? paymentTerms,
        decimal? paymentAmount,
        DateTime? paymentDate,
        string? paymentReferNo,
        string? billReferNo,
        string shippingRecipientName,
        string shippingRecipientAddress,
        string shippingRecipientTIN,
        string shippingRecipientIdentification,
        string customsForm1ReferenceNumber,
        string incoterms,
        string? productTariffCode,
        string? fTAInformation,
        string? authorisationNo,
        string? customsForm2ReferenceNumber,
        string? countryOfOrigin,
        string? otherCharges
    ) : base(id)
    {
        SupplierName = supplierName;
        SupplierTIN = supplierTIN;
        SupplierIdentificationNo = supplierIdentificationNo;
        SupplierSSTRegistrationNo = supplierSSTRegistrationNo;
        SupplierTourismTaxRegistrationNo = supplierTourismTaxRegistrationNo;
        SupplierEmail = supplierEmail;
        SupplierMSICCode = supplierMSICCode;
        SupplierBizActivityDesc = supplierBizActivityDesc;
        SupplierAddress = supplierAddress;
        SupplierContactNo = supplierContactNo;
        BuyerName = buyerName;
        BuyerTIN = buyerTIN;
        BuyerIdentificationNo = buyerIdentificationNo;
        BuyerSSTRegistrationNo = buyerSSTRegistrationNo;
        BuyerEmail = buyerEmail;
        BuyerAddress = buyerAddress;
        BuyerContactNo = buyerContactNo;
        EInvoiceVersion = eInvoiceVersion;
        EInvoiceType = eInvoiceType;
        EInvoiceCode = eInvoiceCode;
        EInvoiceOriginalReferNo = eInvoiceOriginalReferNo;
        EInvoiceDateTime = eInvoiceDateTime;
        EInvoiceValidationDateTime = eInvoiceValidationDateTime;
        IssuerDigitalSignature = issuerDigitalSignature;
        CurrencyCode = currencyCode;
        CurrencyExchangeRate = currencyExchangeRate;
        FrequencyOfBilling = frequencyOfBilling;
        BillingPeriod = billingPeriod;
        IRBMUniqueIdentifierNo = iRBMUniqueIdentifierNo;
        Classification = classification;
        ProductServiceDesc = productServiceDesc;
        UnitPrice = unitPrice;
        TaxType = taxType;
        TaxRate = taxRate;
        TaxAmount = taxAmount;
        TaxExemptionDetail = taxExemptionDetail;
        TaxExemptedAmount = taxExemptedAmount;
        SubTotal = subTotal;
        TotalExcludingTax = totalExcludingTax;
        TotalIncludingTax = totalIncludingTax;
        Quantity = quantity;
        Measurement = measurement;
        DiscountRate = discountRate;
        DiscountAmount = discountAmount;
        PaymentMode = paymentMode;
        SupplierBankAccountNo = supplierBankAccountNo;
        PaymentTerms = paymentTerms;
        PaymentAmount = paymentAmount;
        PaymentDate = paymentDate;
        PaymentReferNo = paymentReferNo;
        BillReferNo = billReferNo;
        ShippingRecipientName = shippingRecipientName;
        ShippingRecipientAddress = shippingRecipientAddress;
        ShippingRecipientTIN = shippingRecipientTIN;
        ShippingRecipientIdentification = shippingRecipientIdentification;
        CustomsForm1ReferenceNumber = customsForm1ReferenceNumber;
        Incoterms = incoterms;
        ProductTariffCode = productTariffCode;
        FTAInformation = fTAInformation;
        AuthorisationNo = authorisationNo;
        CustomsForm2ReferenceNumber = customsForm2ReferenceNumber;
        CountryOfOrigin = countryOfOrigin;
        OtherCharges = otherCharges;
    }
    }
}
