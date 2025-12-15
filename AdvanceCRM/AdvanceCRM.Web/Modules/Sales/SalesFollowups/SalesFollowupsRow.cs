
namespace AdvanceCRM.Sales
{
    using AdvanceCRM.Common;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Sales"), TableName("[dbo].[SalesFollowups]")]
    [DisplayName("Sales Followups"), InstanceName("Sales Followups")]
    [ReadPermission("?")]
    [ModifyPermission("Sales:Followups")]
    public sealed class SalesFollowupsRow : Row<SalesFollowupsRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Followup Note"), Size(200), NotNull, QuickSearch,NameProperty]
        public String FollowupNote
        {
            get { return Fields.FollowupNote[this]; }
            set { Fields.FollowupNote[this] = value; }
        }

        [DisplayName("Details"), Size(2000), NotNull, TextAreaEditor(Rows = 4)]
        public String Details
        {
            get { return Fields.Details[this]; }
            set { Fields.Details[this] = value; }
        }

        [DisplayName("Followup Date"), NotNull, DateTimeEditor(IntervalMinutes = 15, StartHour = 8)]
        public DateTime? FollowupDate
        {
            get { return Fields.FollowupDate[this]; }
            set { Fields.FollowupDate[this] = value; }
        }

        [DisplayName("Status"), NotNull, DefaultValue("1")]
        public Masters.StatusMaster? Status
        {
            get { return (Masters.StatusMaster?)Fields.Status[this]; }
            set { Fields.Status[this] = (Int32?)value; }
        }

        [DisplayName("Sales"), NotNull, ForeignKey("[dbo].[Sales]", "Id"), LeftJoin("jSales"), TextualField("SalesAdditionalInfo")]
        public Int32? SalesId
        {
            get { return Fields.SalesId[this]; }
            set { Fields.SalesId[this] = value; }
        }

        [DisplayName("Followup Done By"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jRepresentative"), TextualField("RepresentativeDisplayName")]
        [Administration.UserEditor]
        public Int32? RepresentativeId
        {
            get { return Fields.RepresentativeId[this]; }
            set { Fields.RepresentativeId[this] = value; }
        }

        [DisplayName("Closing Date"), Required(true)]
        public DateTime? ClosingDate
        {
            get { return Fields.ClosingDate[this]; }
            set { Fields.ClosingDate[this] = value; }
        }

        [Expression("jSales.[ContactsId]")]
        [ForeignKey("Contacts", "Id"), LeftJoin("jContacts")] //added
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

        [ForeignKey("SubContacts", "Id"), LeftJoin("jSubContacts")] //added
        [DisplayName("Sales Contact Person Id"), Expression("jSales.[ContactPersonId]")]
        public Int32? SalesContactPersonId
        {
            get { return Fields.SalesContactPersonId[this]; }
            set { Fields.SalesContactPersonId[this] = value; }
        }

        //[DisplayName("Sales Contact Person Phone"), Expression("jSales.[ContactPersonPhone]")]
        //public String SalesContactPersonPhone
        //{
        //    get { return Fields.SalesContactPersonPhone[this]; }
        //    set { Fields.SalesContactPersonPhone[this] = value; }
        //}

        [DisplayName("Sales Lines"), Expression("jSales.[Lines]")]
        public Int32? SalesLines
        {
            get { return Fields.SalesLines[this]; }
            set { Fields.SalesLines[this] = value; }
        }

        //[DisplayName("Sales Terms"), Expression("jSales.[Terms]")]
        //public String SalesTerms
        //{
        //    get { return Fields.SalesTerms[this]; }
        //    set { Fields.SalesTerms[this] = value; }
        //}

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

        //[DisplayName("Sales Unit"), Expression("jSales.[Unit]")]
        //public String SalesUnit
        //{
        //    get { return Fields.SalesUnit[this]; }
        //    set { Fields.SalesUnit[this] = value; }
        //}

        //[DisplayName("Sales Enquiry Id"), Expression("jSales.[EnquiryId]")]
        //public Int32? SalesEnquiryId
        //{
        //    get { return Fields.SalesEnquiryId[this]; }
        //    set { Fields.SalesEnquiryId[this] = value; }
        //}

        //[DisplayName("Sales Quotation Id"), Expression("jSales.[QuotationId]")]
        //public Int32? SalesQuotationId
        //{
        //    get { return Fields.SalesQuotationId[this]; }
        //    set { Fields.SalesQuotationId[this] = value; }
        //}

        //[DisplayName("Sales Invoice Id"), Expression("jSales.[InvoiceId]")]
        //public Int32? SalesInvoiceId
        //{
        //    get { return Fields.SalesInvoiceId[this]; }
        //    set { Fields.SalesInvoiceId[this] = value; }
        //}

        //[DisplayName("Sales Project Name"), Expression("jSales.[ProjectName]")]
        //public String SalesProjectName
        //{
        //    get { return Fields.SalesProjectName[this]; }
        //    set { Fields.SalesProjectName[this] = value; }
        //}

        //[DisplayName("Sales Purchase Order No"), Expression("jSales.[PurchaseOrderNo]")]
        //public String SalesPurchaseOrderNo
        //{
        //    get { return Fields.SalesPurchaseOrderNo[this]; }
        //    set { Fields.SalesPurchaseOrderNo[this] = value; }
        //}

        //[DisplayName("Sales Contact Person Project"), Expression("jSales.[ContactPersonProject]")]
        //public String SalesContactPersonProject
        //{
        //    get { return Fields.SalesContactPersonProject[this]; }
        //    set { Fields.SalesContactPersonProject[this] = value; }
        //}

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

        [DisplayName("Representative Username"), Expression("jRepresentative.[Username]")]
        public String RepresentativeUsername
        {
            get { return Fields.RepresentativeUsername[this]; }
            set { Fields.RepresentativeUsername[this] = value; }
        }

        [DisplayName("Representative"), Expression("jRepresentative.[DisplayName]")]
        public String RepresentativeDisplayName
        {
            get { return Fields.RepresentativeDisplayName[this]; }
            set { Fields.RepresentativeDisplayName[this] = value; }
        }

        [DisplayName("Representative Email"), Expression("jRepresentative.[Email]")]
        public String RepresentativeEmail
        {
            get { return Fields.RepresentativeEmail[this]; }
            set { Fields.RepresentativeEmail[this] = value; }
        }

        [DisplayName("Representative Source"), Expression("jRepresentative.[Source]")]
        public String RepresentativeSource
        {
            get { return Fields.RepresentativeSource[this]; }
            set { Fields.RepresentativeSource[this] = value; }
        }

        [DisplayName("Representative Password Hash"), Expression("jRepresentative.[PasswordHash]")]
        public String RepresentativePasswordHash
        {
            get { return Fields.RepresentativePasswordHash[this]; }
            set { Fields.RepresentativePasswordHash[this] = value; }
        }

        [DisplayName("Representative Password Salt"), Expression("jRepresentative.[PasswordSalt]")]
        public String RepresentativePasswordSalt
        {
            get { return Fields.RepresentativePasswordSalt[this]; }
            set { Fields.RepresentativePasswordSalt[this] = value; }
        }

        [DisplayName("Representative Last Directory Update"), Expression("jRepresentative.[LastDirectoryUpdate]")]
        public DateTime? RepresentativeLastDirectoryUpdate
        {
            get { return Fields.RepresentativeLastDirectoryUpdate[this]; }
            set { Fields.RepresentativeLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Representative User Image"), Expression("jRepresentative.[UserImage]")]
        public String RepresentativeUserImage
        {
            get { return Fields.RepresentativeUserImage[this]; }
            set { Fields.RepresentativeUserImage[this] = value; }
        }

        [DisplayName("Representative Insert Date"), Expression("jRepresentative.[InsertDate]")]
        public DateTime? RepresentativeInsertDate
        {
            get { return Fields.RepresentativeInsertDate[this]; }
            set { Fields.RepresentativeInsertDate[this] = value; }
        }

        [DisplayName("Representative Insert User Id"), Expression("jRepresentative.[InsertUserId]")]
        public Int32? RepresentativeInsertUserId
        {
            get { return Fields.RepresentativeInsertUserId[this]; }
            set { Fields.RepresentativeInsertUserId[this] = value; }
        }

        [DisplayName("Representative Update Date"), Expression("jRepresentative.[UpdateDate]")]
        public DateTime? RepresentativeUpdateDate
        {
            get { return Fields.RepresentativeUpdateDate[this]; }
            set { Fields.RepresentativeUpdateDate[this] = value; }
        }

        [DisplayName("Representative Update User Id"), Expression("jRepresentative.[UpdateUserId]")]
        public Int32? RepresentativeUpdateUserId
        {
            get { return Fields.RepresentativeUpdateUserId[this]; }
            set { Fields.RepresentativeUpdateUserId[this] = value; }
        }

        [DisplayName("Representative Is Active"), Expression("jRepresentative.[IsActive]")]
        public Int16? RepresentativeIsActive
        {
            get { return Fields.RepresentativeIsActive[this]; }
            set { Fields.RepresentativeIsActive[this] = value; }
        }

        [DisplayName("Representative Upper Level"), Expression("jRepresentative.[UpperLevel]")]
        public Int32? RepresentativeUpperLevel
        {
            get { return Fields.RepresentativeUpperLevel[this]; }
            set { Fields.RepresentativeUpperLevel[this] = value; }
        }

        [DisplayName("Representative Upper Level2"), Expression("jRepresentative.[UpperLevel2]")]
        public Int32? RepresentativeUpperLevel2
        {
            get { return Fields.RepresentativeUpperLevel2[this]; }
            set { Fields.RepresentativeUpperLevel2[this] = value; }
        }

        [DisplayName("Representative Upper Level3"), Expression("jRepresentative.[UpperLevel3]")]
        public Int32? RepresentativeUpperLevel3
        {
            get { return Fields.RepresentativeUpperLevel3[this]; }
            set { Fields.RepresentativeUpperLevel3[this] = value; }
        }

        [DisplayName("Representative Upper Level4"), Expression("jRepresentative.[UpperLevel4]")]
        public Int32? RepresentativeUpperLevel4
        {
            get { return Fields.RepresentativeUpperLevel4[this]; }
            set { Fields.RepresentativeUpperLevel4[this] = value; }
        }

        [DisplayName("Representative Upper Level5"), Expression("jRepresentative.[UpperLevel5]")]
        public Int32? RepresentativeUpperLevel5
        {
            get { return Fields.RepresentativeUpperLevel5[this]; }
            set { Fields.RepresentativeUpperLevel5[this] = value; }
        }

        [DisplayName("Representative Host"), Expression("jRepresentative.[Host]")]
        public String RepresentativeHost
        {
            get { return Fields.RepresentativeHost[this]; }
            set { Fields.RepresentativeHost[this] = value; }
        }

        [DisplayName("Representative Port"), Expression("jRepresentative.[Port]")]
        public Int32? RepresentativePort
        {
            get { return Fields.RepresentativePort[this]; }
            set { Fields.RepresentativePort[this] = value; }
        }

        [DisplayName("Representative SSL"), Expression("jRepresentative.[SSL]")]
        public Boolean? RepresentativeSsl
        {
            get { return Fields.RepresentativeSsl[this]; }
            set { Fields.RepresentativeSsl[this] = value; }
        }

        [DisplayName("Representative Email Id"), Expression("jRepresentative.[EmailId]")]
        public String RepresentativeEmailId
        {
            get { return Fields.RepresentativeEmailId[this]; }
            set { Fields.RepresentativeEmailId[this] = value; }
        }

        [DisplayName("Representative Email Password"), Expression("jRepresentative.[EmailPassword]")]
        public String RepresentativeEmailPassword
        {
            get { return Fields.RepresentativeEmailPassword[this]; }
            set { Fields.RepresentativeEmailPassword[this] = value; }
        }

        [DisplayName("Representative Phone"), Expression("jRepresentative.[Phone]")]
        public String RepresentativePhone
        {
            get { return Fields.RepresentativePhone[this]; }
            set { Fields.RepresentativePhone[this] = value; }
        }

        [DisplayName("Representative Mcsmtp Server"), Expression("jRepresentative.[MCSMTPServer]")]
        public String RepresentativeMcsmtpServer
        {
            get { return Fields.RepresentativeMcsmtpServer[this]; }
            set { Fields.RepresentativeMcsmtpServer[this] = value; }
        }

        [DisplayName("Representative Mcsmtp Port"), Expression("jRepresentative.[MCSMTPPort]")]
        public Int32? RepresentativeMcsmtpPort
        {
            get { return Fields.RepresentativeMcsmtpPort[this]; }
            set { Fields.RepresentativeMcsmtpPort[this] = value; }
        }

        [DisplayName("Representative Mcimap Server"), Expression("jRepresentative.[MCIMAPServer]")]
        public String RepresentativeMcimapServer
        {
            get { return Fields.RepresentativeMcimapServer[this]; }
            set { Fields.RepresentativeMcimapServer[this] = value; }
        }

        [DisplayName("Representative Mcimap Port"), Expression("jRepresentative.[MCIMAPPort]")]
        public Int32? RepresentativeMcimapPort
        {
            get { return Fields.RepresentativeMcimapPort[this]; }
            set { Fields.RepresentativeMcimapPort[this] = value; }
        }

        [DisplayName("Representative Mc Username"), Expression("jRepresentative.[MCUsername]")]
        public String RepresentativeMcUsername
        {
            get { return Fields.RepresentativeMcUsername[this]; }
            set { Fields.RepresentativeMcUsername[this] = value; }
        }

        [DisplayName("Representative Mc Password"), Expression("jRepresentative.[MCPassword]")]
        public String RepresentativeMcPassword
        {
            get { return Fields.RepresentativeMcPassword[this]; }
            set { Fields.RepresentativeMcPassword[this] = value; }
        }

        [DisplayName("Representative Start Time"), Expression("jRepresentative.[StartTime]")]
        public String RepresentativeStartTime
        {
            get { return Fields.RepresentativeStartTime[this]; }
            set { Fields.RepresentativeStartTime[this] = value; }
        }

        [DisplayName("Representative End Time"), Expression("jRepresentative.[EndTime]")]
        public String RepresentativeEndTime
        {
            get { return Fields.RepresentativeEndTime[this]; }
            set { Fields.RepresentativeEndTime[this] = value; }
        }

        [DisplayName("Representative Branch Id"), Expression("jRepresentative.[BranchId]")]
        public Int32? RepresentativeBranchId
        {
            get { return Fields.RepresentativeBranchId[this]; }
            set { Fields.RepresentativeBranchId[this] = value; }
        }

        [DisplayName("Representative Uid"), Expression("jRepresentative.[UID]")]
        public String RepresentativeUid
        {
            get { return Fields.RepresentativeUid[this]; }
            set { Fields.RepresentativeUid[this] = value; }
        }

        [DisplayName("Representative Non Operational"), Expression("jRepresentative.[NonOperational]")]
        public Boolean? RepresentativeNonOperational
        {
            get { return Fields.RepresentativeNonOperational[this]; }
            set { Fields.RepresentativeNonOperational[this] = value; }
        }

        [DisplayName("Name"), Expression("jContacts.[Name]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactName
        {
            get { return Fields.ContactName[this]; }
            set { Fields.ContactName[this] = value; }
        }

        [DisplayName("Email"), Expression("jContacts.[Email]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactEmail
        {
            get { return Fields.ContactEmail[this]; }
            set { Fields.ContactEmail[this] = value; }
        }

        [DisplayName("Phone"), Expression("jContacts.[Phone]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactPhone
        {
            get { return Fields.ContactPhone[this]; }
            set { Fields.ContactPhone[this] = value; }
        }

        [DisplayName("Address"), Expression("jContacts.[Address]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactAddress
        {
            get { return Fields.ContactAddress[this]; }
            set { Fields.ContactAddress[this] = value; }
        }

        [DisplayName("Contact Person Name"), Expression("jSubContacts.[Name]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactPersonName
        {
            get { return Fields.ContactPersonName[this]; }
            set { Fields.ContactPersonName[this] = value; }
        }

        [DisplayName("Contact Person Phone"), Expression("jSubContacts.[Phone]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactPersonPhone
        {
            get { return Fields.ContactPersonPhone[this]; }
            set { Fields.ContactPersonPhone[this] = value; }
        }

        [DisplayName("Contact Person Email"), Expression("jSubContacts.[Email]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactPersonEmail
        {
            get { return Fields.ContactPersonEmail[this]; }
            set { Fields.ContactPersonEmail[this] = value; }
        }

        //[DisplayName("Sales Total"), Expression("(SELECT SUM((((Price * Quantity) - ((DiscountAmount) + ((Price * Quantity) * (Discount / 100)))) + (((Price * Quantity) - ((DiscountAmount) + ((Price * Quantity) * (Discount / 100)))) * (Percentage1 / 100)) + (((Price * Quantity) - ((DiscountAmount) + ((Price * Quantity) * (Discount / 100)))) * (Percentage2 / 100)))) FROM SalesProducts WHERE SalesId=t0.[SalesId])"), AlignRight]
        //public Double? SalesGrandTotal
        //{
        //    get { return Fields.SalesGrandTotal[this]; }
        //    set { Fields.SalesGrandTotal[this] = value; }
        //}

        [NotesEditor, NotMapped]
        [DisplayName("")]
        public List<NoteRow> NoteList
        {
            get { return Fields.NoteList[this]; }
            set { Fields.NoteList[this] = value; }
        }

       

        public SalesFollowupsRow()
            : base(Fields)
        {
        }
        public SalesFollowupsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField FollowupNote;
            public StringField Details;
            public DateTimeField FollowupDate;
            public Int32Field Status;
            public Int32Field SalesId;
            public Int32Field RepresentativeId;
            public DateTimeField ClosingDate;

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
            //public StringField SalesContactPersonPhone;
            public Int32Field SalesLines;
            //public DoubleField SalesGrandTotal;
            public Int32Field SalesInvoiceNo;
            public BooleanField SalesReverseCharge;
            public Int32Field SalesEcomType;
            public Int32Field SalesInvoiceType;
            
            public Int32Field SalesTrasportationId;
            public Int32Field SalesQuotationNo;
            public DateTimeField SalesQuotationDate;
            public DoubleField SalesConversion;
            //public StringField SalesUnit;
            //public Int32Field SalesEnquiryId;
            //public Int32Field SalesQuotationId;
            //public Int32Field SalesInvoiceId;
            //public StringField SalesProjectName;
            //public StringField SalesPurchaseOrderNo;
            //public StringField SalesContactPersonProject;
            public DateTimeField SalesClosingDate;
            public StringField SalesAttachments;
            public BooleanField SalesCurrencyConversion;
            public Int32Field SalesFromCurrency;
            public Int32Field SalesToCurrency;
            public BooleanField SalesTaxable;

            public StringField RepresentativeUsername;
            public StringField RepresentativeDisplayName;
            public StringField RepresentativeEmail;
            public StringField RepresentativeSource;
            public StringField RepresentativePasswordHash;
            public StringField RepresentativePasswordSalt;
            public DateTimeField RepresentativeLastDirectoryUpdate;
            public StringField RepresentativeUserImage;
            public DateTimeField RepresentativeInsertDate;
            public Int32Field RepresentativeInsertUserId;
            public DateTimeField RepresentativeUpdateDate;
            public Int32Field RepresentativeUpdateUserId;
            public Int16Field RepresentativeIsActive;
            public Int32Field RepresentativeUpperLevel;
            public Int32Field RepresentativeUpperLevel2;
            public Int32Field RepresentativeUpperLevel3;
            public Int32Field RepresentativeUpperLevel4;
            public Int32Field RepresentativeUpperLevel5;
            public StringField RepresentativeHost;
            public Int32Field RepresentativePort;
            public BooleanField RepresentativeSsl;
            public StringField RepresentativeEmailId;
            public StringField RepresentativeEmailPassword;
            public StringField RepresentativePhone;
            public StringField RepresentativeMcsmtpServer;
            public Int32Field RepresentativeMcsmtpPort;
            public StringField RepresentativeMcimapServer;
            public Int32Field RepresentativeMcimapPort;
            public StringField RepresentativeMcUsername;
            public StringField RepresentativeMcPassword;
            public StringField RepresentativeStartTime;
            public StringField RepresentativeEndTime;
            public Int32Field RepresentativeBranchId;
            public StringField RepresentativeUid;
            public BooleanField RepresentativeNonOperational;

            public StringField ContactName;
            public StringField ContactPhone;
            public StringField ContactEmail;
            public StringField ContactAddress;
            public StringField ContactPersonName;
            public StringField ContactPersonPhone;
            public StringField ContactPersonEmail;
            public RowListField<NoteRow> NoteList;
        }
    }
}
