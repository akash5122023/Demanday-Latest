
namespace AdvanceCRM.Sales
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Sales"), TableName("[dbo].[InvoiceConcession]")]
    [DisplayName("Invoice Concession"), InstanceName("Invoice Concession")]
    [ReadPermission("Proforma:Read")]
    [ModifyPermission("Proforma:Read")]
    public sealed class InvoiceConcessionRow : Row<InvoiceConcessionRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Concession"), NotNull, ForeignKey("[dbo].[AdditionalConcession]", "Id"), LeftJoin("jConcession"), TextualField("ConcessionName")]
        public Int32? ConcessionId
        {
            get { return Fields.ConcessionId[this]; }
            set { Fields.ConcessionId[this] = value; }
        }

        [DisplayName("Invoice"), NotNull, ForeignKey("[dbo].[Invoice]", "Id"), LeftJoin("jInvoice"), TextualField("InvoiceAdditionalInfo")]
        public Int32? InvoiceId
        {
            get { return Fields.InvoiceId[this]; }
            set { Fields.InvoiceId[this] = value; }
        }

        [DisplayName("Concession Name"), Expression("jConcession.[Name]")]
        public String ConcessionName
        {
            get { return Fields.ConcessionName[this]; }
            set { Fields.ConcessionName[this] = value; }
        }

        [DisplayName("Concession Percentage"), Expression("jConcession.[Percentage]")]
        public Double? ConcessionPercentage
        {
            get { return Fields.ConcessionPercentage[this]; }
            set { Fields.ConcessionPercentage[this] = value; }
        }

        [DisplayName("Invoice Contacts Id"), Expression("jInvoice.[ContactsId]")]
        public Int32? InvoiceContactsId
        {
            get { return Fields.InvoiceContactsId[this]; }
            set { Fields.InvoiceContactsId[this] = value; }
        }

        [DisplayName("Invoice Date"), Expression("jInvoice.[Date]")]
        public DateTime? InvoiceDate
        {
            get { return Fields.InvoiceDate[this]; }
            set { Fields.InvoiceDate[this] = value; }
        }

        [DisplayName("Invoice Status"), Expression("jInvoice.[Status]")]
        public Int32? InvoiceStatus
        {
            get { return Fields.InvoiceStatus[this]; }
            set { Fields.InvoiceStatus[this] = value; }
        }

        [DisplayName("Invoice Type"), Expression("jInvoice.[Type]")]
        public Int32? InvoiceType
        {
            get { return Fields.InvoiceType[this]; }
            set { Fields.InvoiceType[this] = value; }
        }

        [DisplayName("Invoice Additional Info"), Expression("jInvoice.[AdditionalInfo]")]
        public String InvoiceAdditionalInfo
        {
            get { return Fields.InvoiceAdditionalInfo[this]; }
            set { Fields.InvoiceAdditionalInfo[this] = value; }
        }

        [DisplayName("Invoice Source Id"), Expression("jInvoice.[SourceId]")]
        public Int32? InvoiceSourceId
        {
            get { return Fields.InvoiceSourceId[this]; }
            set { Fields.InvoiceSourceId[this] = value; }
        }

        [DisplayName("Invoice Stage Id"), Expression("jInvoice.[StageId]")]
        public Int32? InvoiceStageId
        {
            get { return Fields.InvoiceStageId[this]; }
            set { Fields.InvoiceStageId[this] = value; }
        }

        [DisplayName("Invoice Branch Id"), Expression("jInvoice.[BranchId]")]
        public Int32? InvoiceBranchId
        {
            get { return Fields.InvoiceBranchId[this]; }
            set { Fields.InvoiceBranchId[this] = value; }
        }

        [DisplayName("Invoice Owner Id"), Expression("jInvoice.[OwnerId]")]
        public Int32? InvoiceOwnerId
        {
            get { return Fields.InvoiceOwnerId[this]; }
            set { Fields.InvoiceOwnerId[this] = value; }
        }

        [DisplayName("Invoice Assigned Id"), Expression("jInvoice.[AssignedId]")]
        public Int32? InvoiceAssignedId
        {
            get { return Fields.InvoiceAssignedId[this]; }
            set { Fields.InvoiceAssignedId[this] = value; }
        }

        [DisplayName("Invoice Other Address"), Expression("jInvoice.[OtherAddress]")]
        public Boolean? InvoiceOtherAddress
        {
            get { return Fields.InvoiceOtherAddress[this]; }
            set { Fields.InvoiceOtherAddress[this] = value; }
        }

        [DisplayName("Invoice Shipping Address"), Expression("jInvoice.[ShippingAddress]")]
        public String InvoiceShippingAddress
        {
            get { return Fields.InvoiceShippingAddress[this]; }
            set { Fields.InvoiceShippingAddress[this] = value; }
        }

        [DisplayName("Invoice Packaging Charges"), Expression("jInvoice.[PackagingCharges]")]
        public Double? InvoicePackagingCharges
        {
            get { return Fields.InvoicePackagingCharges[this]; }
            set { Fields.InvoicePackagingCharges[this] = value; }
        }

        [DisplayName("Invoice Freight Charges"), Expression("jInvoice.[FreightCharges]")]
        public Double? InvoiceFreightCharges
        {
            get { return Fields.InvoiceFreightCharges[this]; }
            set { Fields.InvoiceFreightCharges[this] = value; }
        }

        [DisplayName("Invoice Advacne"), Expression("jInvoice.[Advacne]")]
        public Double? InvoiceAdvacne
        {
            get { return Fields.InvoiceAdvacne[this]; }
            set { Fields.InvoiceAdvacne[this] = value; }
        }

        [DisplayName("Invoice Due Date"), Expression("jInvoice.[DueDate]")]
        public DateTime? InvoiceDueDate
        {
            get { return Fields.InvoiceDueDate[this]; }
            set { Fields.InvoiceDueDate[this] = value; }
        }

        [DisplayName("Invoice Dispatch Details"), Expression("jInvoice.[DispatchDetails]")]
        public String InvoiceDispatchDetails
        {
            get { return Fields.InvoiceDispatchDetails[this]; }
            set { Fields.InvoiceDispatchDetails[this] = value; }
        }

        [DisplayName("Invoice Roundup"), Expression("jInvoice.[Roundup]")]
        public Double? InvoiceRoundup
        {
            get { return Fields.InvoiceRoundup[this]; }
            set { Fields.InvoiceRoundup[this] = value; }
        }

        [DisplayName("Invoice Subject"), Expression("jInvoice.[Subject]")]
        public String InvoiceSubject
        {
            get { return Fields.InvoiceSubject[this]; }
            set { Fields.InvoiceSubject[this] = value; }
        }

        [DisplayName("Invoice Reference"), Expression("jInvoice.[Reference]")]
        public String InvoiceReference
        {
            get { return Fields.InvoiceReference[this]; }
            set { Fields.InvoiceReference[this] = value; }
        }

        [DisplayName("Invoice Contact Person Id"), Expression("jInvoice.[ContactPersonId]")]
        public Int32? InvoiceContactPersonId
        {
            get { return Fields.InvoiceContactPersonId[this]; }
            set { Fields.InvoiceContactPersonId[this] = value; }
        }

        [DisplayName("Invoice Lines"), Expression("jInvoice.[Lines]")]
        public Int32? InvoiceLines
        {
            get { return Fields.InvoiceLines[this]; }
            set { Fields.InvoiceLines[this] = value; }
        }

        [DisplayName("Invoice Quotation No"), Expression("jInvoice.[QuotationNo]")]
        public Int32? InvoiceQuotationNo
        {
            get { return Fields.InvoiceQuotationNo[this]; }
            set { Fields.InvoiceQuotationNo[this] = value; }
        }

        [DisplayName("Invoice Quotation Date"), Expression("jInvoice.[QuotationDate]")]
        public DateTime? InvoiceQuotationDate
        {
            get { return Fields.InvoiceQuotationDate[this]; }
            set { Fields.InvoiceQuotationDate[this] = value; }
        }

        [DisplayName("Invoice Conversion"), Expression("jInvoice.[Conversion]")]
        public Double? InvoiceConversion
        {
            get { return Fields.InvoiceConversion[this]; }
            set { Fields.InvoiceConversion[this] = value; }
        }

        [DisplayName("Invoice Purchase Order No"), Expression("jInvoice.[PurchaseOrderNo]")]
        public String InvoicePurchaseOrderNo
        {
            get { return Fields.InvoicePurchaseOrderNo[this]; }
            set { Fields.InvoicePurchaseOrderNo[this] = value; }
        }

        [DisplayName("Invoice Closing Date"), Expression("jInvoice.[ClosingDate]")]
        public DateTime? InvoiceClosingDate
        {
            get { return Fields.InvoiceClosingDate[this]; }
            set { Fields.InvoiceClosingDate[this] = value; }
        }

        [DisplayName("Invoice Attachments"), Expression("jInvoice.[Attachments]")]
        public String InvoiceAttachments
        {
            get { return Fields.InvoiceAttachments[this]; }
            set { Fields.InvoiceAttachments[this] = value; }
        }
        [DisplayName("Concession Amount"), Expression("jConcession.[Amount]")]
        public Double? ConcessionAmount
        {
            get { return Fields.ConcessionAmount[this]; }
            set { Fields.ConcessionAmount[this] = value; }
        }

        [DisplayName("Invoice Currency Conversion"), Expression("jInvoice.[CurrencyConversion]")]
        public Boolean? InvoiceCurrencyConversion
        {
            get { return Fields.InvoiceCurrencyConversion[this]; }
            set { Fields.InvoiceCurrencyConversion[this] = value; }
        }

        [DisplayName("Invoice From Currency"), Expression("jInvoice.[FromCurrency]")]
        public Int32? InvoiceFromCurrency
        {
            get { return Fields.InvoiceFromCurrency[this]; }
            set { Fields.InvoiceFromCurrency[this] = value; }
        }

        [DisplayName("Invoice To Currency"), Expression("jInvoice.[ToCurrency]")]
        public Int32? InvoiceToCurrency
        {
            get { return Fields.InvoiceToCurrency[this]; }
            set { Fields.InvoiceToCurrency[this] = value; }
        }

        [DisplayName("Invoice Taxable"), Expression("jInvoice.[Taxable]")]
        public Boolean? InvoiceTaxable
        {
            get { return Fields.InvoiceTaxable[this]; }
            set { Fields.InvoiceTaxable[this] = value; }
        }

       
        public InvoiceConcessionRow()
            : base(Fields)
        {
        }
        
        public InvoiceConcessionRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ConcessionId;
            public Int32Field InvoiceId;

            public StringField ConcessionName;
            public DoubleField ConcessionAmount;
            public DoubleField ConcessionPercentage;

            public Int32Field InvoiceContactsId;
            public DateTimeField InvoiceDate;
            public Int32Field InvoiceStatus;
            public Int32Field InvoiceType;
            public StringField InvoiceAdditionalInfo;
            public Int32Field InvoiceSourceId;
            public Int32Field InvoiceStageId;
            public Int32Field InvoiceBranchId;
            public Int32Field InvoiceOwnerId;
            public Int32Field InvoiceAssignedId;
            public BooleanField InvoiceOtherAddress;
            public StringField InvoiceShippingAddress;
            public DoubleField InvoicePackagingCharges;
            public DoubleField InvoiceFreightCharges;
            public DoubleField InvoiceAdvacne;
            public DateTimeField InvoiceDueDate;
            public StringField InvoiceDispatchDetails;
            public DoubleField InvoiceRoundup;
            public StringField InvoiceSubject;
            public StringField InvoiceReference;
            public Int32Field InvoiceContactPersonId;
            public Int32Field InvoiceLines;
            
            public Int32Field InvoiceQuotationNo;
            public DateTimeField InvoiceQuotationDate;
            public DoubleField InvoiceConversion;
            public StringField InvoicePurchaseOrderNo;
            public DateTimeField InvoiceClosingDate;
            public StringField InvoiceAttachments;
            public BooleanField InvoiceCurrencyConversion;
            public Int32Field InvoiceFromCurrency;
            public Int32Field InvoiceToCurrency;
            public BooleanField InvoiceTaxable;
        }
    }
}
