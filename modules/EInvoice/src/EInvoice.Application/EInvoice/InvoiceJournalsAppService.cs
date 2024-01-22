using System;
using System.Linq;
using System.Threading.Tasks;
using EInvoice.Permissions;
using EInvoice.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using EasyAbp.FileManagement.Files.Dtos;
using System.Collections.Generic;
using Volo.Abp.Content;
using JetBrains.Annotations;
using EasyAbp.FileManagement.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Sylvan.Data.Csv;
using System.IO;
using System.Text;
using System.Dynamic;
using System.Globalization;
using Volo.Abp;
using Volo.Abp.Uow;
using EasyAbp.FileManagement.Application;
using NPOI.SS.UserModel;

namespace EInvoice;


public class InvoiceJournalsAppService : CrudAppService<InvoiceJournals, InvoiceJournalsDto, Guid, InvoiceJournalsGetListInput, CreateUpdateInvoiceJournalsDto, CreateUpdateInvoiceJournalsDto>,
    IInvoiceJournalsAppService
{
    protected override string GetPolicyName { get; set; } = EInvoicePermissions.InvoiceJournals.Default;
    protected override string GetListPolicyName { get; set; } = EInvoicePermissions.InvoiceJournals.Default;
    protected override string CreatePolicyName { get; set; } = EInvoicePermissions.InvoiceJournals.Create;
    protected override string UpdatePolicyName { get; set; } = EInvoicePermissions.InvoiceJournals.Update;
    protected override string DeletePolicyName { get; set; } = EInvoicePermissions.InvoiceJournals.Delete;

    private readonly IFileAppService _service;

    public InvoiceJournalsAppService(
        IRepository<InvoiceJournals, Guid> repository,
        IFileAppService service
        ) : base(repository)
    {
        _service = service;
    }

    protected override async Task<IQueryable<InvoiceJournals>> CreateFilteredQueryAsync(InvoiceJournalsGetListInput input)
    {
        // TODO: AbpHelper generated
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(!input.SupplierName.IsNullOrWhiteSpace(), x => x.SupplierName.Contains(input.SupplierName))
            .WhereIf(!input.SupplierTIN.IsNullOrWhiteSpace(), x => x.SupplierTIN.Contains(input.SupplierTIN))
            .WhereIf(!input.SupplierIdentificationNo.IsNullOrWhiteSpace(), x => x.SupplierIdentificationNo.Contains(input.SupplierIdentificationNo))
            .WhereIf(input.SupplierSSTRegistrationNo != null, x => x.SupplierSSTRegistrationNo == input.SupplierSSTRegistrationNo)
            .WhereIf(input.SupplierTourismTaxRegistrationNo != null, x => x.SupplierTourismTaxRegistrationNo == input.SupplierTourismTaxRegistrationNo)
            .WhereIf(!input.SupplierEmail.IsNullOrWhiteSpace(), x => x.SupplierEmail.Contains(input.SupplierEmail))
            .WhereIf(!input.SupplierMSICCode.IsNullOrWhiteSpace(), x => x.SupplierMSICCode.Contains(input.SupplierMSICCode))
            .WhereIf(!input.SupplierBizActivityDesc.IsNullOrWhiteSpace(), x => x.SupplierBizActivityDesc.Contains(input.SupplierBizActivityDesc))
            .WhereIf(!input.SupplierAddress.IsNullOrWhiteSpace(), x => x.SupplierAddress.Contains(input.SupplierAddress))
            .WhereIf(!input.SupplierContactNo.IsNullOrWhiteSpace(), x => x.SupplierContactNo.Contains(input.SupplierContactNo))
            .WhereIf(!input.BuyerName.IsNullOrWhiteSpace(), x => x.BuyerName.Contains(input.BuyerName))
            .WhereIf(!input.BuyerTIN.IsNullOrWhiteSpace(), x => x.BuyerTIN.Contains(input.BuyerTIN))
            .WhereIf(!input.BuyerIdentificationNo.IsNullOrWhiteSpace(), x => x.BuyerIdentificationNo.Contains(input.BuyerIdentificationNo))
            .WhereIf(input.BuyerSSTRegistrationNo != null, x => x.BuyerSSTRegistrationNo == input.BuyerSSTRegistrationNo)
            .WhereIf(!input.BuyerEmail.IsNullOrWhiteSpace(), x => x.BuyerEmail.Contains(input.BuyerEmail))
            .WhereIf(!input.BuyerAddress.IsNullOrWhiteSpace(), x => x.BuyerAddress.Contains(input.BuyerAddress))
            .WhereIf(!input.BuyerContactNo.IsNullOrWhiteSpace(), x => x.BuyerContactNo.Contains(input.BuyerContactNo))
            .WhereIf(!input.EInvoiceVersion.IsNullOrWhiteSpace(), x => x.EInvoiceVersion.Contains(input.EInvoiceVersion))
            .WhereIf(!input.EInvoiceType.IsNullOrWhiteSpace(), x => x.EInvoiceType.Contains(input.EInvoiceType))
            .WhereIf(!input.EInvoiceCode.IsNullOrWhiteSpace(), x => x.EInvoiceCode.Contains(input.EInvoiceCode))
            .WhereIf(!input.EInvoiceOriginalReferNo.IsNullOrWhiteSpace(), x => x.EInvoiceOriginalReferNo.Contains(input.EInvoiceOriginalReferNo))
            .WhereIf(input.EInvoiceDateTime != null, x => x.EInvoiceDateTime == input.EInvoiceDateTime)
            .WhereIf(input.EInvoiceValidationDateTime != null, x => x.EInvoiceValidationDateTime == input.EInvoiceValidationDateTime)
            .WhereIf(!input.IssuerDigitalSignature.IsNullOrWhiteSpace(), x => x.IssuerDigitalSignature.Contains(input.IssuerDigitalSignature))
            .WhereIf(!input.CurrencyCode.IsNullOrWhiteSpace(), x => x.CurrencyCode.Contains(input.CurrencyCode))
            .WhereIf(input.CurrencyExchangeRate != null, x => x.CurrencyExchangeRate == input.CurrencyExchangeRate)
            .WhereIf(!input.FrequencyOfBilling.IsNullOrWhiteSpace(), x => x.FrequencyOfBilling.Contains(input.FrequencyOfBilling))
            .WhereIf(!input.BillingPeriod.IsNullOrWhiteSpace(), x => x.BillingPeriod.Contains(input.BillingPeriod))
            .WhereIf(!input.IRBMUniqueIdentifierNo.IsNullOrWhiteSpace(), x => x.IRBMUniqueIdentifierNo.Contains(input.IRBMUniqueIdentifierNo))
            .WhereIf(!input.Classification.IsNullOrWhiteSpace(), x => x.Classification.Contains(input.Classification))
            .WhereIf(!input.ProductServiceDesc.IsNullOrWhiteSpace(), x => x.ProductServiceDesc.Contains(input.ProductServiceDesc))
            .WhereIf(input.UnitPrice != null, x => x.UnitPrice == input.UnitPrice)
            .WhereIf(!input.TaxType.IsNullOrWhiteSpace(), x => x.TaxType.Contains(input.TaxType))
            .WhereIf(input.TaxRate != null, x => x.TaxRate == input.TaxRate)
            .WhereIf(input.TaxAmount != null, x => x.TaxAmount == input.TaxAmount)
            .WhereIf(input.TaxExemptionDetail != null, x => x.TaxExemptionDetail == input.TaxExemptionDetail)
            .WhereIf(input.TaxExemptedAmount != null, x => x.TaxExemptedAmount == input.TaxExemptedAmount)
            .WhereIf(input.SubTotal != null, x => x.SubTotal == input.SubTotal)
            .WhereIf(input.TotalExcludingTax != null, x => x.TotalExcludingTax == input.TotalExcludingTax)
            .WhereIf(input.TotalIncludingTax != null, x => x.TotalIncludingTax == input.TotalIncludingTax)
            .WhereIf(input.Quantity != null, x => x.Quantity == input.Quantity)
            .WhereIf(input.Measurement != null, x => x.Measurement == input.Measurement)
            .WhereIf(input.DiscountRate != null, x => x.DiscountRate == input.DiscountRate)
            .WhereIf(input.DiscountAmount != null, x => x.DiscountAmount == input.DiscountAmount)
            .WhereIf(input.PaymentMode != null, x => x.PaymentMode == input.PaymentMode)
            .WhereIf(input.SupplierBankAccountNo != null, x => x.SupplierBankAccountNo == input.SupplierBankAccountNo)
            .WhereIf(input.PaymentTerms != null, x => x.PaymentTerms == input.PaymentTerms)
            .WhereIf(input.PaymentAmount != null, x => x.PaymentAmount == input.PaymentAmount)
            .WhereIf(input.PaymentDate != null, x => x.PaymentDate == input.PaymentDate)
            .WhereIf(input.PaymentReferNo != null, x => x.PaymentReferNo == input.PaymentReferNo)
            .WhereIf(input.BillReferNo != null, x => x.BillReferNo == input.BillReferNo)
            .WhereIf(!input.ShippingRecipientName.IsNullOrWhiteSpace(), x => x.ShippingRecipientName.Contains(input.ShippingRecipientName))
            .WhereIf(!input.ShippingRecipientAddress.IsNullOrWhiteSpace(), x => x.ShippingRecipientAddress.Contains(input.ShippingRecipientAddress))
            .WhereIf(!input.ShippingRecipientTIN.IsNullOrWhiteSpace(), x => x.ShippingRecipientTIN.Contains(input.ShippingRecipientTIN))
            .WhereIf(!input.ShippingRecipientIdentification.IsNullOrWhiteSpace(), x => x.ShippingRecipientIdentification.Contains(input.ShippingRecipientIdentification))
            .WhereIf(!input.CustomsForm1ReferenceNumber.IsNullOrWhiteSpace(), x => x.CustomsForm1ReferenceNumber.Contains(input.CustomsForm1ReferenceNumber))
            .WhereIf(!input.Incoterms.IsNullOrWhiteSpace(), x => x.Incoterms.Contains(input.Incoterms))
            .WhereIf(input.ProductTariffCode != null, x => x.ProductTariffCode == input.ProductTariffCode)
            .WhereIf(input.FTAInformation != null, x => x.FTAInformation == input.FTAInformation)
            .WhereIf(input.AuthorisationNo != null, x => x.AuthorisationNo == input.AuthorisationNo)
            .WhereIf(input.CustomsForm2ReferenceNumber != null, x => x.CustomsForm2ReferenceNumber == input.CustomsForm2ReferenceNumber)
            .WhereIf(input.CountryOfOrigin != null, x => x.CountryOfOrigin == input.CountryOfOrigin)
            .WhereIf(input.OtherCharges != null, x => x.OtherCharges == input.OtherCharges)
            ;
    }


    public virtual async Task<CreateManyFileOutput> CreateManyWithStreamAsync(CreateManyFileWithStreamInput input)
    {
        try
        {
            foreach (var fileInfo in input.FileContents)
            {
                await AuthorizationService.CheckAsync(CreatePolicyName);

                var steam = fileInfo.GetStream(); // file steam in memory

                // handle csv upload
                if(fileInfo.FileName !=null && fileInfo.FileName.ToLower().IndexOf(".csv") != -1)
                {
                    var reader = new StreamReader(steam, Encoding.UTF8);

                    var opts = new CsvDataReaderOptions
                    {
                        HasHeaders = true,
                        Delimiter = '|'
                    };
                    var csv = await CsvDataReader.CreateAsync(reader, opts);

                    while (csv.Read())
                    {
                        var invoice = new InvoiceJournals();
                        invoice.SupplierName = csv.GetString(0);
                        invoice.BuyerName = csv.GetString(1);
                        invoice.SupplierTIN = csv.GetString(2);
                        invoice.SupplierIdentificationNo = csv.GetString(3);
                        invoice.SupplierSSTRegistrationNo = csv.GetString(4);
                        invoice.SupplierTourismTaxRegistrationNo = csv.GetString(5);
                        invoice.SupplierEmail = csv.GetString(6);
                        invoice.SupplierMSICCode = csv.GetString(7);
                        invoice.SupplierBizActivityDesc = csv.GetString(8);
                        invoice.BuyerTIN = csv.GetString(9);
                        invoice.BuyerIdentificationNo = csv.GetString(10);
                        invoice.BuyerSSTRegistrationNo = csv.GetString(11);
                        invoice.BuyerEmail = csv.GetString(12);
                        invoice.SupplierAddress = csv.GetString(13);
                        invoice.BuyerAddress = csv.GetString(14);
                        invoice.SupplierContactNo = csv.GetString(15);
                        invoice.BuyerContactNo = csv.GetString(16);
                        invoice.EInvoiceVersion = csv.GetString(17);
                        invoice.EInvoiceType = csv.GetString(18);
                        invoice.EInvoiceCode = csv.GetString(19);
                        invoice.EInvoiceOriginalReferNo = csv.GetString(20);

                        invoice.EInvoiceDateTime = convertMYdatetime(csv.GetString(21));
                        invoice.EInvoiceValidationDateTime = convertMYdatetime(csv.GetString(22));
                        invoice.IssuerDigitalSignature = csv.GetString(23);
                        invoice.CurrencyCode = csv.GetString(24);
                        invoice.CurrencyExchangeRate = convertDecimal(csv.GetString(25));
                        invoice.FrequencyOfBilling = csv.GetString(26);
                        invoice.BillingPeriod = csv.GetString(27);
                        invoice.IRBMUniqueIdentifierNo = csv.GetString(28);
                        invoice.Classification = csv.GetString(29);
                        invoice.ProductServiceDesc = csv.GetString(30);
                        invoice.UnitPrice = csv.GetDecimal(31);
                        invoice.TaxType = csv.GetString(32);
                        invoice.TaxRate = convertDecimal(csv.GetString(33));
                        invoice.TaxAmount = convertDecimal(csv.GetString(34));
                        invoice.TaxExemptionDetail = csv.GetString(35);
                        invoice.TaxExemptedAmount = convertDecimal(csv.GetString(36));
                        invoice.SubTotal = convertDecimal(csv.GetString(37));
                        invoice.TotalExcludingTax = convertDecimal(csv.GetString(38));
                        invoice.TotalIncludingTax = convertDecimal(csv.GetString(39));
                        invoice.Quantity = convertDecimal(csv.GetString(40));
                        invoice.Measurement = csv.GetString(41);
                        invoice.DiscountRate = convertDecimal(csv.GetString(42));
                        invoice.DiscountAmount = convertDecimal(csv.GetString(43));
                        invoice.PaymentMode = csv.GetString(44);
                        invoice.SupplierBankAccountNo = csv.GetString(45);
                        invoice.PaymentTerms = csv.GetString(46);
                        invoice.PaymentAmount = convertDecimal(csv.GetString(47));
                        invoice.PaymentDate = convertMYdatetime(csv.GetString(48));
                        invoice.PaymentReferNo = csv.GetString(49);
                        invoice.BillReferNo = csv.GetString(50);
                        invoice.CustomsForm1ReferenceNumber = "";
                        invoice.CustomsForm2ReferenceNumber = "";
                        invoice.Incoterms = "";
                        invoice.ShippingRecipientAddress = "";
                        invoice.ShippingRecipientIdentification = "";
                        invoice.ShippingRecipientName = "";
                        invoice.ShippingRecipientTIN = "";

                        await Repository.InsertAsync(invoice);
                        if (CurrentUnitOfWork != null)
                            await CurrentUnitOfWork.SaveChangesAsync();
                    }
                }

                //handle excel upload
                if(fileInfo.FileName !=null && fileInfo.FileName.ToLower().IndexOf(".xlsx") != -1)
                {

                    MemoryStream objMemoryStream = new MemoryStream();
                    fileInfo.GetStream().CopyTo(objMemoryStream);


                    var worksheet = CustomDtoExcelImporter.ImportEntityFromStream(fileInfo.FileName,objMemoryStream);

                    var rowEnumerator = worksheet.GetRowEnumerator();
                    rowEnumerator.Reset();

                    var rowIndex = 0;

                    while (rowEnumerator.MoveNext())
                    {
                        if (rowIndex == 0)
                        {
                            rowIndex++;
                            continue;
                        }

                        try
                        {
                            DataFormatter formatter = new DataFormatter();
                            var invoice = new InvoiceJournals();
                            var columnNameList = worksheet.GetRow(rowIndex).Cells;

                            invoice.SupplierName = formatter.FormatCellValue(columnNameList[0]);
                            invoice.BuyerName = formatter.FormatCellValue(columnNameList[1]);
                            invoice.SupplierTIN = formatter.FormatCellValue(columnNameList[2]);
                            invoice.SupplierIdentificationNo = formatter.FormatCellValue(columnNameList[3]);
                            invoice.SupplierSSTRegistrationNo = formatter.FormatCellValue(columnNameList[4]);
                            invoice.SupplierTourismTaxRegistrationNo = formatter.FormatCellValue(columnNameList[5]);
                            invoice.SupplierEmail = formatter.FormatCellValue(columnNameList[6]);
                            invoice.SupplierMSICCode = formatter.FormatCellValue(columnNameList[7]);
                            invoice.SupplierBizActivityDesc = formatter.FormatCellValue(columnNameList[8]);
                            invoice.BuyerTIN = formatter.FormatCellValue(columnNameList[9]);
                            invoice.BuyerIdentificationNo = formatter.FormatCellValue(columnNameList[10]);
                            invoice.BuyerSSTRegistrationNo = formatter.FormatCellValue(columnNameList[11]);
                            invoice.BuyerEmail = formatter.FormatCellValue(columnNameList[12]);
                            invoice.SupplierAddress = formatter.FormatCellValue(columnNameList[13]);
                            invoice.BuyerAddress = formatter.FormatCellValue(columnNameList[14]);
                            invoice.SupplierContactNo = formatter.FormatCellValue(columnNameList[15]);
                            invoice.BuyerContactNo = formatter.FormatCellValue(columnNameList[16]);
                            invoice.EInvoiceVersion = formatter.FormatCellValue(columnNameList[17]);
                            invoice.EInvoiceType = formatter.FormatCellValue(columnNameList[18]);
                            invoice.EInvoiceCode = formatter.FormatCellValue(columnNameList[19]);
                            invoice.EInvoiceOriginalReferNo = formatter.FormatCellValue(columnNameList[20]);

                            invoice.EInvoiceDateTime = excelDateFormatter(columnNameList[21].DateCellValue);
                            invoice.EInvoiceValidationDateTime = excelDateFormatter(columnNameList[22].DateCellValue);

                            invoice.IssuerDigitalSignature = formatter.FormatCellValue(columnNameList[23]);
                            invoice.CurrencyCode = formatter.FormatCellValue(columnNameList[24]);
                            invoice.CurrencyExchangeRate = convertDecimal(formatter.FormatCellValue(columnNameList[25]));
                            invoice.FrequencyOfBilling = formatter.FormatCellValue(columnNameList[26]);
                            invoice.BillingPeriod = formatter.FormatCellValue(columnNameList[27]);
                            invoice.IRBMUniqueIdentifierNo = formatter.FormatCellValue(columnNameList[28]);
                            invoice.Classification = formatter.FormatCellValue(columnNameList[29]);
                            invoice.ProductServiceDesc = formatter.FormatCellValue(columnNameList[30]);
                            invoice.UnitPrice = convertDecimal(formatter.FormatCellValue(columnNameList[30]));
                            invoice.TaxType = formatter.FormatCellValue(columnNameList[32]);
                            invoice.TaxRate = convertDecimal(formatter.FormatCellValue(columnNameList[33]));
                            invoice.TaxAmount = convertDecimal(formatter.FormatCellValue(columnNameList[34]));
                            invoice.TaxExemptionDetail = formatter.FormatCellValue(columnNameList[35]);
                            invoice.TaxExemptedAmount = convertDecimal(formatter.FormatCellValue(columnNameList[36]));
                            invoice.SubTotal = convertDecimal(formatter.FormatCellValue(columnNameList[37]));
                            invoice.TotalExcludingTax = convertDecimal(formatter.FormatCellValue(columnNameList[38]));
                            invoice.TotalIncludingTax = convertDecimal(formatter.FormatCellValue(columnNameList[39]));
                            invoice.Quantity = convertDecimal(formatter.FormatCellValue(columnNameList[40]));
                            invoice.Measurement = formatter.FormatCellValue(columnNameList[41]);
                            invoice.DiscountRate = convertDecimal(formatter.FormatCellValue(columnNameList[42]));
                            invoice.DiscountAmount = convertDecimal(formatter.FormatCellValue(columnNameList[43]));
                            invoice.PaymentMode = formatter.FormatCellValue(columnNameList[44]);
                            invoice.SupplierBankAccountNo = formatter.FormatCellValue(columnNameList[45]);
                            invoice.PaymentTerms = formatter.FormatCellValue(columnNameList[46]);
                            invoice.PaymentAmount = convertDecimal(formatter.FormatCellValue(columnNameList[47]));
                            invoice.PaymentDate = excelDateFormatter(columnNameList[48].DateCellValue);
                            invoice.PaymentReferNo = formatter.FormatCellValue(columnNameList[49]);
                            invoice.BillReferNo = formatter.FormatCellValue(columnNameList[50]);
                            invoice.CustomsForm1ReferenceNumber = "";
                            invoice.CustomsForm2ReferenceNumber = "";
                            invoice.Incoterms = "";
                            invoice.ShippingRecipientAddress = "";
                            invoice.ShippingRecipientIdentification = "";
                            invoice.ShippingRecipientName = "";
                            invoice.ShippingRecipientTIN = "";
                            await Repository.InsertAsync(invoice);
                            if (CurrentUnitOfWork != null)
                                await CurrentUnitOfWork.SaveChangesAsync();


                            rowIndex++;
                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            ex=ex.GetBaseException();
            throw new UserFriendlyException(ex.Message);
        }
        

        var fileReturn = await _service.CreateManyWithStreamAsync(input);

        return fileReturn;
    }

    private static decimal convertDecimal(string value)
    {
        var result = 0m;
        decimal.TryParse(value, out result);
        return result;
    }

    private DateTime convertMYdatetime(string strDatetime)
    {  
        if(strDatetime.IsNullOrEmpty())
            return DateTime.MinValue;
        DateTime dateTime;
        string[] enUKformats = { "dd-MM-yyyy" }; // malaysia,UK,SG
        bool success = DateTime.TryParseExact(strDatetime, enUKformats, new CultureInfo("en-UK"), DateTimeStyles.None, out dateTime);
        if(!success)
        {
            string format_uk2 = "d/M/yyyy"; // malaysia,UK,SG
            success = DateTime.TryParseExact(strDatetime, format_uk2, null, DateTimeStyles.None, out dateTime);
            if (!success)
            {
                string[] utcFormat = { "yyyy-MM-ddTHH:mm:ss" }; // international
                DateTimeStyles styles = DateTimeStyles.AdjustToUniversal;
                success = DateTime.TryParseExact(strDatetime, utcFormat, null, styles, out dateTime);
                if (!success)
                {
                    throw new Exception($"{strDatetime} is invalid input!");
                }
            }
        }
        return dateTime;
    }

    private DateTime excelDateFormatter(DateTime date)
    {
        if (date.Year == 1900)
            return DateTime.MinValue;

        return date;
    }
}
