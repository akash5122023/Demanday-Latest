
namespace AdvanceCRM.Sales
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Sales"), TableName("[dbo].[SalesTerms]")]
    [DisplayName("Sales Terms"), InstanceName("Sales Terms")]
    [ReadPermission("Sales:Read")]
    [ModifyPermission("Sales:Read")]
    public sealed class SalesTermsRow : Row<SalesTermsRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Terms"), NotNull, ForeignKey("[dbo].[QuotationTermsMaster]", "Id"), LeftJoin("jTerms"), TextualField("Terms")]
        public Int32? TermsId
        {
            get { return Fields.TermsId[this]; }
            set { Fields.TermsId[this] = value; }
        }

        [DisplayName("Sales"), NotNull, ForeignKey("[dbo].[Sales]", "Id"), LeftJoin("jSales"), TextualField("SalesAdditionalInfo")]
        public Int32? SalesId
        {
            get { return Fields.SalesId[this]; }
            set { Fields.SalesId[this] = value; }
        }

        [DisplayName("Terms"), Expression("jTerms.[Terms]")]
        public String Terms
        {
            get { return Fields.Terms[this]; }
            set { Fields.Terms[this] = value; }
        }

        [DisplayName("Sales Contacts Id"), Expression("jSales.[ContactsId]")]
        public Int32? SalesContactsId
        {
            get { return Fields.SalesContactsId[this]; }
            set { Fields.SalesContactsId[this] = value; }
        }

        [DisplayName("Sales Date"), Expression("jSales.[Date]")]
        public DateTime? SalesDate
        {
            get { return Fields.SalesDate[this]; }
            set { Fields.SalesDate[this] = value; }
        }

        [DisplayName("Sales Status"), Expression("jSales.[Status]")]
        public Int32? SalesStatus
        {
            get { return Fields.SalesStatus[this]; }
            set { Fields.SalesStatus[this] = value; }
        }

        [DisplayName("Sales Type"), Expression("jSales.[Type]")]
        public Int32? SalesType
        {
            get { return Fields.SalesType[this]; }
            set { Fields.SalesType[this] = value; }
        }

        [DisplayName("Sales Additional Info"), Expression("jSales.[AdditionalInfo]")]
        public String SalesAdditionalInfo
        {
            get { return Fields.SalesAdditionalInfo[this]; }
            set { Fields.SalesAdditionalInfo[this] = value; }
        }

        [DisplayName("Sales Source Id"), Expression("jSales.[SourceId]")]
        public Int32? SalesSourceId
        {
            get { return Fields.SalesSourceId[this]; }
            set { Fields.SalesSourceId[this] = value; }
        }

        [DisplayName("Sales Stage Id"), Expression("jSales.[StageId]")]
        public Int32? SalesStageId
        {
            get { return Fields.SalesStageId[this]; }
            set { Fields.SalesStageId[this] = value; }
        }

        [DisplayName("Sales Branch Id"), Expression("jSales.[BranchId]")]
        public Int32? SalesBranchId
        {
            get { return Fields.SalesBranchId[this]; }
            set { Fields.SalesBranchId[this] = value; }
        }

        [DisplayName("Sales Owner Id"), Expression("jSales.[OwnerId]")]
        public Int32? SalesOwnerId
        {
            get { return Fields.SalesOwnerId[this]; }
            set { Fields.SalesOwnerId[this] = value; }
        }

        [DisplayName("Sales Assigned Id"), Expression("jSales.[AssignedId]")]
        public Int32? SalesAssignedId
        {
            get { return Fields.SalesAssignedId[this]; }
            set { Fields.SalesAssignedId[this] = value; }
        }

        [DisplayName("Sales Other Address"), Expression("jSales.[OtherAddress]")]
        public Boolean? SalesOtherAddress
        {
            get { return Fields.SalesOtherAddress[this]; }
            set { Fields.SalesOtherAddress[this] = value; }
        }

        [DisplayName("Sales Shipping Address"), Expression("jSales.[ShippingAddress]")]
        public String SalesShippingAddress
        {
            get { return Fields.SalesShippingAddress[this]; }
            set { Fields.SalesShippingAddress[this] = value; }
        }

        [DisplayName("Sales Packaging Charges"), Expression("jSales.[PackagingCharges]")]
        public Double? SalesPackagingCharges
        {
            get { return Fields.SalesPackagingCharges[this]; }
            set { Fields.SalesPackagingCharges[this] = value; }
        }

        [DisplayName("Sales Freight Charges"), Expression("jSales.[FreightCharges]")]
        public Double? SalesFreightCharges
        {
            get { return Fields.SalesFreightCharges[this]; }
            set { Fields.SalesFreightCharges[this] = value; }
        }

        [DisplayName("Sales Advacne"), Expression("jSales.[Advacne]")]
        public Double? SalesAdvacne
        {
            get { return Fields.SalesAdvacne[this]; }
            set { Fields.SalesAdvacne[this] = value; }
        }

        [DisplayName("Sales Due Date"), Expression("jSales.[DueDate]")]
        public DateTime? SalesDueDate
        {
            get { return Fields.SalesDueDate[this]; }
            set { Fields.SalesDueDate[this] = value; }
        }

        [DisplayName("Sales Dispatch Details"), Expression("jSales.[DispatchDetails]")]
        public String SalesDispatchDetails
        {
            get { return Fields.SalesDispatchDetails[this]; }
            set { Fields.SalesDispatchDetails[this] = value; }
        }

        [DisplayName("Sales Roundup"), Expression("jSales.[Roundup]")]
        public Double? SalesRoundup
        {
            get { return Fields.SalesRoundup[this]; }
            set { Fields.SalesRoundup[this] = value; }
        }

        [DisplayName("Sales Contact Person Id"), Expression("jSales.[ContactPersonId]")]
        public Int32? SalesContactPersonId
        {
            get { return Fields.SalesContactPersonId[this]; }
            set { Fields.SalesContactPersonId[this] = value; }
        }

        [DisplayName("Sales Lines"), Expression("jSales.[Lines]")]
        public Int32? SalesLines
        {
            get { return Fields.SalesLines[this]; }
            set { Fields.SalesLines[this] = value; }
        }

        [DisplayName("Sales Invoice No"), Expression("jSales.[InvoiceNo]")]
        public Int32? SalesInvoiceNo
        {
            get { return Fields.SalesInvoiceNo[this]; }
            set { Fields.SalesInvoiceNo[this] = value; }
        }

        [DisplayName("Sales Reverse Charge"), Expression("jSales.[ReverseCharge]")]
        public Boolean? SalesReverseCharge
        {
            get { return Fields.SalesReverseCharge[this]; }
            set { Fields.SalesReverseCharge[this] = value; }
        }

        [DisplayName("Sales Ecom Type"), Expression("jSales.[EcomType]")]
        public Int32? SalesEcomType
        {
            get { return Fields.SalesEcomType[this]; }
            set { Fields.SalesEcomType[this] = value; }
        }

        [DisplayName("Sales Invoice Type"), Expression("jSales.[InvoiceType]")]
        public Int32? SalesInvoiceType
        {
            get { return Fields.SalesInvoiceType[this]; }
            set { Fields.SalesInvoiceType[this] = value; }
        }

        [DisplayName("Sales Trasportation Id"), Expression("jSales.[TrasportationId]")]
        public Int32? SalesTrasportationId
        {
            get { return Fields.SalesTrasportationId[this]; }
            set { Fields.SalesTrasportationId[this] = value; }
        }

        [DisplayName("Sales Quotation No"), Expression("jSales.[QuotationNo]")]
        public Int32? SalesQuotationNo
        {
            get { return Fields.SalesQuotationNo[this]; }
            set { Fields.SalesQuotationNo[this] = value; }
        }

        [DisplayName("Sales Quotation Date"), Expression("jSales.[QuotationDate]")]
        public DateTime? SalesQuotationDate
        {
            get { return Fields.SalesQuotationDate[this]; }
            set { Fields.SalesQuotationDate[this] = value; }
        }

        [DisplayName("Sales Conversion"), Expression("jSales.[Conversion]")]
        public Double? SalesConversion
        {
            get { return Fields.SalesConversion[this]; }
            set { Fields.SalesConversion[this] = value; }
        }

        [DisplayName("Sales Purchase Order No"), Expression("jSales.[PurchaseOrderNo]")]
        public String SalesPurchaseOrderNo
        {
            get { return Fields.SalesPurchaseOrderNo[this]; }
            set { Fields.SalesPurchaseOrderNo[this] = value; }
        }

        [DisplayName("Sales Closing Date"), Expression("jSales.[ClosingDate]")]
        public DateTime? SalesClosingDate
        {
            get { return Fields.SalesClosingDate[this]; }
            set { Fields.SalesClosingDate[this] = value; }
        }

        [DisplayName("Sales Attachments"), Expression("jSales.[Attachments]")]
        public String SalesAttachments
        {
            get { return Fields.SalesAttachments[this]; }
            set { Fields.SalesAttachments[this] = value; }
        }

        [DisplayName("Sales Currency Conversion"), Expression("jSales.[CurrencyConversion]")]
        public Boolean? SalesCurrencyConversion
        {
            get { return Fields.SalesCurrencyConversion[this]; }
            set { Fields.SalesCurrencyConversion[this] = value; }
        }

        [DisplayName("Sales From Currency"), Expression("jSales.[FromCurrency]")]
        public Int32? SalesFromCurrency
        {
            get { return Fields.SalesFromCurrency[this]; }
            set { Fields.SalesFromCurrency[this] = value; }
        }

        [DisplayName("Sales To Currency"), Expression("jSales.[ToCurrency]")]
        public Int32? SalesToCurrency
        {
            get { return Fields.SalesToCurrency[this]; }
            set { Fields.SalesToCurrency[this] = value; }
        }

        [DisplayName("Sales Taxable"), Expression("jSales.[Taxable]")]
        public Boolean? SalesTaxable
        {
            get { return Fields.SalesTaxable[this]; }
            set { Fields.SalesTaxable[this] = value; }
        }

     
        public SalesTermsRow()
            : base(Fields)
        {
        }
        public SalesTermsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field TermsId;
            public Int32Field SalesId;

            public StringField Terms;

            public Int32Field SalesContactsId;
            public DateTimeField SalesDate;
            public Int32Field SalesStatus;
            public Int32Field SalesType;
            public StringField SalesAdditionalInfo;
            public Int32Field SalesSourceId;
            public Int32Field SalesStageId;
            public Int32Field SalesBranchId;
            public Int32Field SalesOwnerId;
            public Int32Field SalesAssignedId;
            public BooleanField SalesOtherAddress;
            public StringField SalesShippingAddress;
            public DoubleField SalesPackagingCharges;
            public DoubleField SalesFreightCharges;
            public DoubleField SalesAdvacne;
            public DateTimeField SalesDueDate;
            public StringField SalesDispatchDetails;
            public DoubleField SalesRoundup;
            public Int32Field SalesContactPersonId;
            public Int32Field SalesLines;
            public Int32Field SalesInvoiceNo;
            public BooleanField SalesReverseCharge;
            public Int32Field SalesEcomType;
            public Int32Field SalesInvoiceType;
            
            public Int32Field SalesTrasportationId;
            public Int32Field SalesQuotationNo;
            public DateTimeField SalesQuotationDate;
            public DoubleField SalesConversion;
            public StringField SalesPurchaseOrderNo;
            public DateTimeField SalesClosingDate;
            public StringField SalesAttachments;
            public BooleanField SalesCurrencyConversion;
            public Int32Field SalesFromCurrency;
            public Int32Field SalesToCurrency;
            public BooleanField SalesTaxable;
        }
    }
}
