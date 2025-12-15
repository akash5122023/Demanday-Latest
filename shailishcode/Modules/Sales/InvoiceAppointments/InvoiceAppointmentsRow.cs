
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

    [ConnectionKey("Default"), Module("Sales"), TableName("[dbo].[InvoiceAppointments]")]
    [DisplayName("Invoice Appointments"), InstanceName("Invoice Appointments")]
    [ReadPermission("?")]
    [ModifyPermission("Proforma:Appointments")]

    public sealed class InvoiceAppointmentsRow : Row<InvoiceAppointmentsRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Agenda"), Size(2000), NotNull, QuickSearch, TextAreaEditor(Rows = 4),NameProperty]
        public String Details
        {
            get { return Fields.Details[this]; }
            set { Fields.Details[this] = value; }
        }

        [DisplayName("Date"), NotNull, DateTimeEditor]
        public DateTime? AppointmentDate
        {
            get { return Fields.AppointmentDate[this]; }
            set { Fields.AppointmentDate[this] = value; }
        }

        [DisplayName("Status"), NotNull, DefaultValue("1")]
        public Masters.StatusMaster? Status
        {
            get { return (Masters.StatusMaster ? )Fields.Status[this]; }
            set { Fields.Status[this] = (Int32?)value; }
        }

        [DisplayName("Representative"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jRepresentative"), TextualField("RepresentativeDisplayName")]
        [Administration.UserEditor]
        public Int32? RepresentativeId
        {
            get { return Fields.RepresentativeId[this]; }
            set { Fields.RepresentativeId[this] = value; }
        }

        [DisplayName("Invoice"), NotNull, ForeignKey("[dbo].[Invoice]", "Id"), LeftJoin("jInvoice"), TextualField("InvoiceAdditionalInfo")]
        public Int32? InvoiceId
        {
            get { return Fields.InvoiceId[this]; }
            set { Fields.InvoiceId[this] = value; }
        }

         [DisplayName("Minutes Of Meeting"), Size(1024), Required(true), TextAreaEditor(Rows = 4)]
        public String MinutesOfMeeting
        {
            get { return Fields.MinutesOfMeeting[this]; }
            set { Fields.MinutesOfMeeting[this] = value; }
        }

        [DisplayName("Outcome/Reason"), Size(1024), Required(true), TextAreaEditor(Rows = 4)]
        public String Reason
        {
            get { return Fields.Reason[this]; }
            set { Fields.Reason[this] = value; }
        }

        [DisplayName("Representative"), Expression("jRepresentative.[Username]")]
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

        [DisplayName("Representative Branch Id"), Expression("jRepresentative.[BranchId]")]
        public Int32? RepresentativeBranchId
        {
            get { return Fields.RepresentativeBranchId[this]; }
            set { Fields.RepresentativeBranchId[this] = value; }
        }

        [DisplayName("Invoice Contacts Id"), Expression("jInvoice.[ContactsId]")]
        [ForeignKey("Contacts", "Id"), LeftJoin("jContacts")] //added
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
        
        [DisplayName("Name"), Expression("jContacts.[Name]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactName 
        { 
            get { return Fields.ContactName[this]; } 
            set { Fields.ContactName[this] = value; } 
        }

        [DisplayName("Phone"), Expression("jContacts.[Phone]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactPhone 
        { 
            get { return Fields.ContactPhone[this]; } 
            set { Fields.ContactPhone[this] = value; }
        }

        [DisplayName("Email"), Expression("jContacts.[Email]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactEmail
        {
            get { return Fields.ContactEmail[this]; }
            set { Fields.ContactEmail[this] = value; }
        }

        [DisplayName("Address"), Expression("jContacts.[Address]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactAddress 
        { 
            get { return Fields.ContactAddress[this]; } 
            set { Fields.ContactAddress[this] = value; } 
        }

        [NotesEditor, NotMapped]
        [DisplayName("")]
        public List<NoteRow> NoteList
        {
            get { return Fields.NoteList[this]; }
            set { Fields.NoteList[this] = value; }
        }

      
        public InvoiceAppointmentsRow()
            : base(Fields)
        {
        }
        
        public InvoiceAppointmentsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Details;
            public DateTimeField AppointmentDate;
            public Int32Field Status;
            public Int32Field RepresentativeId;
            public Int32Field InvoiceId;
            public StringField MinutesOfMeeting;
            public StringField Reason;

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
            public StringField RepresentativeUid;
            public BooleanField RepresentativeNonOperational;
            public Int32Field RepresentativeBranchId;

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
            
            public StringField ContactName;
            public StringField ContactPhone;
            public StringField ContactEmail;
            public StringField ContactAddress;
            public RowListField<NoteRow> NoteList;
        }
    }
}
