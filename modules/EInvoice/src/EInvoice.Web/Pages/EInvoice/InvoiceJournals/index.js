$(function () {

    $("#InvoiceJournalsFilter :input").on('input', function () {
        dataTable.ajax.reload();
    });

    //After abp v7.2 use dynamicForm 'column-size' instead of the following settings
    //$('#InvoiceJournalsCollapse div').addClass('col-sm-3').parent().addClass('row');

    var getFilter = function () {
        var input = {};
        $("#InvoiceJournalsFilter")
            .serializeArray()
            .forEach(function (data) {
                if (data.value != '') {
                    input[abp.utils.toCamelCase(data.name.replace(/InvoiceJournalsFilter./g, ''))] = data.value;
                }
            })
        return input;
    };

    var l = abp.localization.getResource('EInvoice');

    var service = eInvoice.invoiceJournals;
    var createModal = new abp.ModalManager(abp.appPath + 'EInvoice/InvoiceJournals/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EInvoice/InvoiceJournals/EditModal');
    var uploadModal = new abp.ModalManager(abp.appPath + 'EInvoice/InvoiceJournals/FileImportModal');

    var dataTable = $('#InvoiceJournalsTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,//disable default searchbox
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList,getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('EInvoice.InvoiceJournals.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('EInvoice.InvoiceJournals.Delete'),
                                confirmMessage: function (data) {
                                    return l('InvoiceJournalsDeletionConfirmationMessage', data.record.id);
                                },
                                action: function (data) {
                                    service.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            {
                title: 'ProcessingStatus',
                data: "processingStatus",
                render: function (data) {
                    return processingStatusFormatter(data);
                }
            },
            {
                title: "Import Date",
                data: "creationTime",
                render: function (data) {
                    return luxon
                        .DateTime
                        .fromISO(data, {
                            locale: abp.localization.currentCulture.name
                        }).toFormat("dd-MM-yyyy HH:mm");
                }
            },
            {
                title: l('InvoiceJournalsSupplierName'),
                data: "supplierName"
            },
            {
                title: l('InvoiceJournalsSupplierTIN'),
                data: "supplierTIN"
            },
            {
                title: l('InvoiceJournalsSupplierIdentificationNo'),
                data: "supplierIdentificationNo"
            },
            {
                title: l('InvoiceJournalsSupplierSSTRegistrationNo'),
                data: "supplierSSTRegistrationNo"
            },
            {
                title: l('InvoiceJournalsSupplierTourismTaxRegistrationNo'),
                data: "supplierTourismTaxRegistrationNo"
            },
            {
                title: l('InvoiceJournalsSupplierEmail'),
                data: "supplierEmail"
            },
            {
                title: l('InvoiceJournalsSupplierMSICCode'),
                data: "supplierMSICCode"
            },
            {
                title: l('InvoiceJournalsSupplierBizActivityDesc'),
                data: "supplierBizActivityDesc"
            },
            {
                title: l('InvoiceJournalsSupplierAddress'),
                data: "supplierAddress"
            },
            {
                title: l('InvoiceJournalsSupplierContactNo'),
                data: "supplierContactNo"
            },
            {
                title: l('InvoiceJournalsBuyerName'),
                data: "buyerName"
            },
            {
                title: l('InvoiceJournalsBuyerTIN'),
                data: "buyerTIN"
            },
            {
                title: l('InvoiceJournalsBuyerIdentificationNo'),
                data: "buyerIdentificationNo"
            },
            {
                title: l('InvoiceJournalsBuyerSSTRegistrationNo'),
                data: "buyerSSTRegistrationNo"
            },
            {
                title: l('InvoiceJournalsBuyerEmail'),
                data: "buyerEmail"
            },
            {
                title: l('InvoiceJournalsBuyerAddress'),
                data: "buyerAddress"
            },
            {
                title: l('InvoiceJournalsBuyerContactNo'),
                data: "buyerContactNo"
            },
            {
                title: l('InvoiceJournalsEInvoiceVersion'),
                data: "eInvoiceVersion"
            },
            {
                title: l('InvoiceJournalsEInvoiceType'),
                data: "eInvoiceType"
            },
            {
                title: l('InvoiceJournalsEInvoiceCode'),
                data: "eInvoiceCode"
            },
            {
                title: l('InvoiceJournalsEInvoiceOriginalReferNo'),
                data: "eInvoiceOriginalReferNo"
            },
            {
                title: l('InvoiceJournalsEInvoiceDateTime'),
                data: "eInvoiceDateTime",
                render: function (data) {
                    return luxon
                        .DateTime
                        .fromISO(data, {
                            locale: abp.localization.currentCulture.name
                        }).toFormat("dd-MM-yyyy HH:mm");
                }
            },
            {
                title: l('InvoiceJournalsEInvoiceValidationDateTime'),
                data: "eInvoiceValidationDateTime",
                render: function (data) {
                    return luxon
                        .DateTime
                        .fromISO(data, {
                            locale: abp.localization.currentCulture.name
                        }).toFormat("dd-MM-yyyy HH:mm");
                }
            },
            {
                title: l('InvoiceJournalsIssuerDigitalSignature'),
                data: "issuerDigitalSignature"
            },
            {
                title: l('InvoiceJournalsCurrencyCode'),
                data: "currencyCode"
            },
            {
                title: l('InvoiceJournalsCurrencyExchangeRate'),
                data: "currencyExchangeRate"
            },
            {
                title: l('InvoiceJournalsFrequencyOfBilling'),
                data: "frequencyOfBilling"
            },
            {
                title: l('InvoiceJournalsBillingPeriod'),
                data: "billingPeriod"
            },
            {
                title: l('InvoiceJournalsIRBMUniqueIdentifierNo'),
                data: "irbmUniqueIdentifierNo"
            },
            {
                title: l('InvoiceJournalsClassification'),
                data: "classification"
            },
            {
                title: l('InvoiceJournalsProductServiceDesc'),
                data: "productServiceDesc"
            },
            {
                title: l('InvoiceJournalsUnitPrice'),
                data: "unitPrice"
            },
            {
                title: l('InvoiceJournalsTaxType'),
                data: "taxType"
            },
            {
                title: l('InvoiceJournalsTaxRate'),
                data: "taxRate"
            },
            {
                title: l('InvoiceJournalsTaxAmount'),
                data: "taxAmount"
            },
            {
                title: l('InvoiceJournalsTaxExemptionDetail'),
                data: "taxExemptionDetail"
            },
            {
                title: l('InvoiceJournalsTaxExemptedAmount'),
                data: "taxExemptedAmount"
            },
            {
                title: l('InvoiceJournalsSubTotal'),
                data: "subTotal"
            },
            {
                title: l('InvoiceJournalsTotalExcludingTax'),
                data: "totalExcludingTax"
            },
            {
                title: l('InvoiceJournalsTotalIncludingTax'),
                data: "totalIncludingTax"
            },
            {
                title: l('InvoiceJournalsQuantity'),
                data: "quantity"
            },
            {
                title: l('InvoiceJournalsMeasurement'),
                data: "measurement"
            },
            {
                title: l('InvoiceJournalsDiscountRate'),
                data: "discountRate"
            },
            {
                title: l('InvoiceJournalsDiscountAmount'),
                data: "discountAmount"
            },
            {
                title: l('InvoiceJournalsPaymentMode'),
                data: "paymentMode"
            },
            {
                title: l('InvoiceJournalsSupplierBankAccountNo'),
                data: "supplierBankAccountNo"
            },
            {
                title: l('InvoiceJournalsPaymentTerms'),
                data: "paymentTerms"
            },
            {
                title: l('InvoiceJournalsPaymentAmount'),
                data: "paymentAmount"
            },
            {
                title: l('InvoiceJournalsPaymentDate'),
                data: "paymentDate",
                render: function (data) {
                    return luxon
                        .DateTime
                        .fromISO(data, {
                            locale: abp.localization.currentCulture.name
                        }).toFormat("dd-MM-yyyy HH:mm");
                }
            },
            {
                title: l('InvoiceJournalsPaymentReferNo'),
                data: "paymentReferNo"
            },
            {
                title: l('InvoiceJournalsBillReferNo'),
                data: "billReferNo"
            },
            {
                title: l('InvoiceJournalsShippingRecipientName'),
                data: "shippingRecipientName"
            },
            {
                title: l('InvoiceJournalsShippingRecipientAddress'),
                data: "shippingRecipientAddress"
            },
            {
                title: l('InvoiceJournalsShippingRecipientTIN'),
                data: "shippingRecipientTIN"
            },
            {
                title: l('InvoiceJournalsShippingRecipientIdentification'),
                data: "shippingRecipientIdentification"
            },
            {
                title: l('InvoiceJournalsCustomsForm1ReferenceNumber'),
                data: "customsForm1ReferenceNumber"
            },
            {
                title: l('InvoiceJournalsIncoterms'),
                data: "incoterms"
            },
            {
                title: l('InvoiceJournalsProductTariffCode'),
                data: "productTariffCode"
            },
            {
                title: l('InvoiceJournalsFTAInformation'),
                data: "ftaInformation"
            },
            {
                title: l('InvoiceJournalsAuthorisationNo'),
                data: "authorisationNo"
            },
            {
                title: l('InvoiceJournalsCustomsForm2ReferenceNumber'),
                data: "customsForm2ReferenceNumber"
            },
            {
                title: l('InvoiceJournalsCountryOfOrigin'),
                data: "countryOfOrigin"
            },
            {
                title: l('InvoiceJournalsOtherCharges'),
                data: "otherCharges"
            },
        ]
    }));

    //createModal.onResult(function () {
    //    dataTable.ajax.reload();
    //});

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    //$('#NewInvoiceJournalsButton').click(function (e) {
    //    e.preventDefault();
    //    createModal.open();
    //});

    $('#UploadFileButton').click(function (e) {
        e.preventDefault();
        uploadModal.open({ fileContainerName: fileContainerName, ownerUserId: ownerUserId, parentId: parentId });
    });

    uploadModal.onResult(function () {
        dataTable.ajax.reload();
    });

    var processingStatusFormatter = function (value) {
        console.log(value);
        switch (value) {
            case 5:
                return 'OnHold';
            case 4:
                return 'Error';
            case 3:
                return 'Completed';
            case 2:
                return 'InProgress';
            case 1:
                return 'Ready';
            case 0:
                return 'Imported';
            default:
                return '';
        }
    }
});
