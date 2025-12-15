
namespace AdvanceCRM.Enquiry
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

    [ConnectionKey("Default"), Module("Enquiry"), TableName("[dbo].[EnquiryAppointments]")]
    [DisplayName("Enquiry Appointments"), InstanceName("Enquiry Appointments")]
    [ReadPermission("?")]
    [ModifyPermission("Enquiry:Appointments")]
   
    public sealed class EnquiryAppointmentsRow : Row<EnquiryAppointmentsRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
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

        [DisplayName("Enquiry"), NotNull, ForeignKey("[dbo].[Enquiry]", "Id"), LeftJoin("jEnquiry"), TextualField("EnquiryAdditionalInfo")]
        public Int32? EnquiryId
        {
            get { return Fields.EnquiryId[this]; }
            set { Fields.EnquiryId[this] = value; }
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

        [DisplayName("Enquiry Contacts Id"), Expression("jEnquiry.[ContactsId]")]
        [ForeignKey("Contacts", "Id"), LeftJoin("jContacts")] //added
        public Int32? EnquiryContactsId
        {
            get { return Fields.EnquiryContactsId[this]; }
            set { Fields.EnquiryContactsId[this] = value; }
        }

        [DisplayName("Enquiry Date"), Expression("jEnquiry.[Date]")]
        public DateTime? EnquiryDate
        {
            get { return Fields.EnquiryDate[this]; }
            set { Fields.EnquiryDate[this] = value; }
        }

        [DisplayName("Enquiry Status"), Expression("jEnquiry.[Status]")]
        public Int32? EnquiryStatus
        {
            get { return Fields.EnquiryStatus[this]; }
            set { Fields.EnquiryStatus[this] = value; }
        }

        [DisplayName("Enquiry Type"), Expression("jEnquiry.[Type]")]
        public Int32? EnquiryType
        {
            get { return Fields.EnquiryType[this]; }
            set { Fields.EnquiryType[this] = value; }
        }

        [DisplayName("Enquiry Additional Info"), Expression("jEnquiry.[AdditionalInfo]")]
        public String EnquiryAdditionalInfo
        {
            get { return Fields.EnquiryAdditionalInfo[this]; }
            set { Fields.EnquiryAdditionalInfo[this] = value; }
        }

        [DisplayName("Enquiry Source Id"), Expression("jEnquiry.[SourceId]")]
        public Int32? EnquirySourceId
        {
            get { return Fields.EnquirySourceId[this]; }
            set { Fields.EnquirySourceId[this] = value; }
        }

        [DisplayName("Enquiry Stage Id"), Expression("jEnquiry.[StageId]")]
        public Int32? EnquiryStageId
        {
            get { return Fields.EnquiryStageId[this]; }
            set { Fields.EnquiryStageId[this] = value; }
        }

        [DisplayName("Enquiry Branch Id"), Expression("jEnquiry.[BranchId]")]
        public Int32? EnquiryBranchId
        {
            get { return Fields.EnquiryBranchId[this]; }
            set { Fields.EnquiryBranchId[this] = value; }
        }

        [DisplayName("Enquiry Owner Id"), Expression("jEnquiry.[OwnerId]")]
        public Int32? EnquiryOwnerId
        {
            get { return Fields.EnquiryOwnerId[this]; }
            set { Fields.EnquiryOwnerId[this] = value; }
        }

        [DisplayName("Enquiry Assigned Id"), Expression("jEnquiry.[AssignedId]")]
        public Int32? EnquiryAssignedId
        {
            get { return Fields.EnquiryAssignedId[this]; }
            set { Fields.EnquiryAssignedId[this] = value; }
        }

        [DisplayName("Enquiry Reference Name"), Expression("jEnquiry.[ReferenceName]")]
        public String EnquiryReferenceName
        {
            get { return Fields.EnquiryReferenceName[this]; }
            set { Fields.EnquiryReferenceName[this] = value; }
        }

        [DisplayName("Enquiry Reference Phone"), Expression("jEnquiry.[ReferencePhone]")]
        public String EnquiryReferencePhone
        {
            get { return Fields.EnquiryReferencePhone[this]; }
            set { Fields.EnquiryReferencePhone[this] = value; }
        }

        [DisplayName("Enquiry Closing Type"), Expression("jEnquiry.[ClosingType]")]
        public Int32? EnquiryClosingType
        {
            get { return Fields.EnquiryClosingType[this]; }
            set { Fields.EnquiryClosingType[this] = value; }
        }

        [DisplayName("Enquiry Lost Reason"), Expression("jEnquiry.[LostReason]")]
        public String EnquiryLostReason
        {
            get { return Fields.EnquiryLostReason[this]; }
            set { Fields.EnquiryLostReason[this] = value; }
        }

        [DisplayName("Enquiry Closing Date"), Expression("jEnquiry.[ClosingDate]")]
        public DateTime? EnquiryClosingDate
        {
            get { return Fields.EnquiryClosingDate[this]; }
            set { Fields.EnquiryClosingDate[this] = value; }
        }

        [DisplayName("Enquiry Contact Person Id"), Expression("jEnquiry.[ContactPersonId]")]
        public Int32? EnquiryContactPersonId
        {
            get { return Fields.EnquiryContactPersonId[this]; }
            set { Fields.EnquiryContactPersonId[this] = value; }
        }

        [DisplayName("Enquiry Attachments"), Expression("jEnquiry.[Attachments]")]
        public String EnquiryAttachments
        {
            get { return Fields.EnquiryAttachments[this]; }
            set { Fields.EnquiryAttachments[this] = value; }
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

        
        public EnquiryAppointmentsRow()
            : base(Fields)
        {
        }
        
        public EnquiryAppointmentsRow(RowFields fields)
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
            public Int32Field EnquiryId;
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

            public Int32Field EnquiryContactsId;
            public DateTimeField EnquiryDate;
            public Int32Field EnquiryStatus;
            public Int32Field EnquiryType;
            public StringField EnquiryAdditionalInfo;
            public Int32Field EnquirySourceId;
            public Int32Field EnquiryStageId;
            public Int32Field EnquiryBranchId;
            public Int32Field EnquiryOwnerId;
            public Int32Field EnquiryAssignedId;
            public StringField EnquiryReferenceName;
            public StringField EnquiryReferencePhone;
            public Int32Field EnquiryClosingType;
            public StringField EnquiryLostReason;
            public DateTimeField EnquiryClosingDate;
            public Int32Field EnquiryContactPersonId;
            public StringField EnquiryAttachments;
            
            public StringField ContactName;
            public StringField ContactPhone;
            public StringField ContactEmail;
            public StringField ContactAddress;
            public RowListField<NoteRow> NoteList;
        }
    }
}
