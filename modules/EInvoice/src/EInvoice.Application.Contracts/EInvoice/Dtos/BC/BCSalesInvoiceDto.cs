﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace EInvoice.EInvoice.Dtos.BC
{
    public class BCSalesInvoiceDto
    {
        public int Document_Type { get; set; }
        public string No { get; set; }
        public string Sell_to_Customer_No { get; set; }
        public string Bill_to_Customer_No { get; set; }
        public string Bill_to_Name { get; set; }
        public string Bill_to_Name_2 { get; set; }
        public string Bill_to_Address { get; set; }
        public string Bill_to_Address_2 { get; set; }
        public string Bill_to_City { get; set; }
        public string Bill_to_Contact { get; set; }
        public string Your_Reference { get; set; }
        public string Ship_to_Code { get; set; }
        public string Ship_to_Name { get; set; }
        public string Ship_to_Name_2 { get; set; }
        public string Ship_to_Address { get; set; }
        public string Ship_to_Address_2 { get; set; }
        public string Ship_to_City { get; set; }
        public string Ship_to_Contact { get; set; }
        public DateTime? Order_Date { get; set; }
        public DateTime? Posting_Date { get; set; }
        public DateTime? Shipment_Date { get; set; }
        public string Posting_Description { get; set; }
        public string Payment_Terms_Code { get; set; }
        public DateTime? Due_Date { get; set; }
        public decimal Payment_Discount { get; set; }
        public DateTime? Pmt_Discount_Date { get; set; }
        public string Shipment_Method_Code { get; set; }
        public string Location_Code { get; set; }
        public string Shortcut_Dimension_1_Code { get; set; }
        public string Shortcut_Dimension_2_Code { get; set; }
        public string Customer_Posting_Group { get; set; }
        public string Currency_Code { get; set; }
        public int Currency_Factor { get; set; }
        public string Customer_Price_Group { get; set; }
        public bool Prices_Including_VAT { get; set; }
        public string Invoice_Disc_Code { get; set; }
        public string Customer_Disc_Group { get; set; }
        public string Language_Code { get; set; }
        public string Format_Region { get; set; }
        public string Salesperson_Code { get; set; }
        public string Order_Class { get; set; }
        public int No_Printed { get; set; }
        public string On_Hold { get; set; }
        public int Applies_to_Doc_Type { get; set; }
        public string Applies_to_Doc_No { get; set; }
        public string Bal_Account_No { get; set; }
        public bool Ship { get; set; }
        public bool Invoice { get; set; }
        public bool Print_Posted_Documents { get; set; }
        public string Shipping_No { get; set; }
        public string Posting_No { get; set; }
        public string Last_Shipping_No { get; set; }
        public string Last_Posting_No { get; set; }
        public string Prepayment_No { get; set; }
        public string Last_Prepayment_No { get; set; }
        public string Prepmt_Cr_Memo_No { get; set; }
        public string Last_Prepmt_Cr_Memo_No { get; set; }
        public string VAT_Registration_No { get; set; }
        public bool Combine_Shipments { get; set; }
        public string Reason_Code { get; set; }
        public string Gen_Bus_Posting_Group { get; set; }
        public bool EU_3_Party_Trade { get; set; }
        public string Transaction_Type { get; set; }
        public string Transport_Method { get; set; }
        public string VAT_Country_Region_Code { get; set; }
        public string Sell_to_Customer_Name { get; set; }
        public string Sell_to_Customer_Name_2 { get; set; }
        public string Sell_to_Address { get; set; }
        public string Sell_to_Address_2 { get; set; }
        public string Sell_to_City { get; set; }
        public string Sell_to_Contact { get; set; }
        public string Bill_to_Post_Code { get; set; }
        public string Bill_to_County { get; set; }
        public string Bill_to_Country_Region_Code { get; set; }
        public string Sell_to_Post_Code { get; set; }
        public string Sell_to_County { get; set; }
        public string Sell_to_Country_Region_Code { get; set; }
        public string Ship_to_Post_Code { get; set; }
        public string Ship_to_County { get; set; }
        public string Ship_to_Country_Region_Code { get; set; }
        public int Bal_Account_Type { get; set; }
        public string Exit_Point { get; set; }
        public bool Correction { get; set; }
        public DateTime? Document_Date { get; set; }
        public string External_Document_No { get; set; }
        public string Area { get; set; }
        public string Transaction_Specification { get; set; }
        public string Payment_Method_Code { get; set; }
        public string Shipping_Agent_Code { get; set; }
        public string Package_Tracking_No { get; set; }
        public string No_Series { get; set; }
        public string Posting_No_Series { get; set; }
        public string Shipping_No_Series { get; set; }
        public string Tax_Area_Code { get; set; }
        public bool Tax_Liable { get; set; }
        public string VAT_Bus_Posting_Group { get; set; }
        public string Reserve { get; set; }
        public string Applies_to_ID { get; set; }
        public string VAT_Base_Discount { get; set; }
        public string Status { get; set; }
        public string Invoice_Discount_Calculation { get; set; }
        public string Invoice_Discount_Value { get; set; }
        public bool Send_IC_Document { get; set; }
        public string IC_Status { get; set; }
        public string Sell_to_IC_Partner_Code { get; set; }
        public string Bill_to_IC_Partner_Code { get; set; }
        public string IC_Reference_Document_No { get; set; }
        public string IC_Direction { get; set; }
        public string Prepayment { get; set; }
        public string Prepayment_No_Series { get; set; }
        public bool Compress_Prepayment { get; set; }
        public DateTime? Prepayment_Due_Date { get; set; }
        public string Prepmt_Cr_Memo_No_Series { get; set; }
        public string Prepmt_Posting_Description { get; set; }
        public DateTime? Prepmt_Pmt_Discount_Date { get; set; }
        public string Prepmt_Payment_Terms_Code { get; set; }
        public string Prepmt_Payment_Discount { get; set; }
        public string Quote_No { get; set; }
        public DateTime? Quote_Valid_Until_Date { get; set; }
        public DateTime? Quote_Sent_to_Customer { get; set; }
        public bool Quote_Accepted { get; set; }
        public DateTime? Quote_Accepted_Date { get; set; }
        public string Job_Queue_Status { get; set; }
        public Guid Job_Queue_Entry_ID { get; set; }
        public string Company_Bank_Account_Code { get; set; }
        public string Incoming_Document_Entry_No { get; set; }
        public bool IsTest { get; set; }
        public string Sell_to_Phone_No { get; set; }
        public string Sell_to_E_Mail { get; set; }
        public string Journal_Templ_Name { get; set; }
        public DateTime? VAT_Reporting_Date { get; set; }
        public string Rcvd_from_Count_Region_Code { get; set; }
        public string Work_Description { get; set; }
        public string Dimension_Set_ID { get; set; }
        public string Payment_Service_Set_ID { get; set; }
        public bool Coupled_to_CRM { get; set; }
        public string Direct_Debit_Mandate_ID { get; set; }
        public string Doc_No_Occurrence { get; set; }
        public string Campaign_No { get; set; }
        public string Sell_to_Contact_No { get; set; }
        public string Bill_to_Contact_No { get; set; }
        public string Opportunity_No { get; set; }
        public string Sell_to_Customer_Templ_Code { get; set; }
        public string Bill_to_Customer_Templ_Code { get; set; }
        public string Responsibility_Center { get; set; }
        public string Shipping_Advice { get; set; }
        public string Posting_from_Whse_Ref { get; set; }
        public DateTime? Requested_Delivery_Date { get; set; }
        public DateTime? Promised_Delivery_Date { get; set; }
        public string Shipping_Time { get; set; }
        public string Outbound_Whse_Handling_Time { get; set; }
        public string Shipping_Agent_Service_Code { get; set; }
        public bool Receive { get; set; }
        public string Return_Receipt_No { get; set; }
        public string Return_Receipt_No_Series { get; set; }
        public string Last_Return_Receipt_No { get; set; }
        public int Price_Calculation_Method { get; set; }
        public bool Allow_Line_Disc { get; set; }
        public bool Get_Shipment_Used { get; set; }
        public string Assigned_User_ID { get; set; }
        public string Shpfy_Order_Id { get; set; }
        public string Shpfy_Order_No { get; set; }
        public string Shpfy_Refund_Id { get; set; }
        public string IRBM_Code { get; set; }
        public bool Comment { get; set; }
        public bool Recalculate_Invoice_Disc { get; set; }
        public decimal Amount { get; set; }
        public decimal Amount_Including_VAT { get; set; }
        public DateTime? Last_Email_Sent_Time { get; set; }
        public string Last_Email_Sent_Status { get; set; }
        public bool Sent_as_Email { get; set; }
        public bool Last_Email_Notif_Cleared { get; set; }
        public string Amt_Ship_Not_Inv__LCY { get; set; }
        public string Amt_Ship_Not_Inv__LCY_Base { get; set; }
        public bool Coupled_to_Dataverse { get; set; }
        public string Invoice_Discount_Amount { get; set; }
        public string No_of_Archived_Versions { get; set; }
        public bool Shipped_Not_Invoiced { get; set; }
        public bool Completely_Shipped { get; set; }
        public bool Shipped { get; set; }
        public DateTime? Last_Shipment_Date { get; set; }
        public bool Late_Order_Shipping { get; set; }
        public string Shpfy_Risk_Level { get; set; }
    }
}
