
namespace AdvanceCRM.Services
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

    [ConnectionKey("Default"), Module("Services"), TableName("[dbo].[TeleCallingAppointments]")]
    [DisplayName("Tele Calling Appointments"), InstanceName("Tele Calling Appointments")]
    [ReadPermission("?")]
    [ModifyPermission("TeleCalling:Appointments")]

    public sealed class TeleCallingAppointmentsRow : Row<TeleCallingAppointmentsRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
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

        [DisplayName("Tele Calling"), NotNull, ForeignKey("[dbo].[TeleCalling]", "Id"), LeftJoin("jTeleCalling"), TextualField("TeleCallingFeedback")]
        public Int32? TeleCallingId
        {
            get { return Fields.TeleCallingId[this]; }
            set { Fields.TeleCallingId[this] = value; }
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

        [DisplayName("Tele Calling Contacts Id"), Expression("jTeleCalling.[ContactsId]")]
        [ForeignKey("Contacts", "Id"), LeftJoin("jContacts")] //added
        public Int32? TeleCallingContactsId
        {
            get { return Fields.TeleCallingContactsId[this]; }
            set { Fields.TeleCallingContactsId[this] = value; }
        }

        [DisplayName("Tele Calling Products Id"), Expression("jTeleCalling.[ProductsId]")]
        public Int32? TeleCallingProductsId
        {
            get { return Fields.TeleCallingProductsId[this]; }
            set { Fields.TeleCallingProductsId[this] = value; }
        }

        [DisplayName("Tele Calling Status"), Expression("jTeleCalling.[Status]")]
        public Int32? TeleCallingStatus
        {
            get { return Fields.TeleCallingStatus[this]; }
            set { Fields.TeleCallingStatus[this] = value; }
        }

        [DisplayName("Tele Calling Feedback"), Expression("jTeleCalling.[Feedback]")]
        public String TeleCallingFeedback
        {
            get { return Fields.TeleCallingFeedback[this]; }
            set { Fields.TeleCallingFeedback[this] = value; }
        }

        [DisplayName("Tele Calling Source Id"), Expression("jTeleCalling.[SourceId]")]
        public Int32? TeleCallingSourceId
        {
            get { return Fields.TeleCallingSourceId[this]; }
            set { Fields.TeleCallingSourceId[this] = value; }
        }

        [DisplayName("Tele Calling Stage Id"), Expression("jTeleCalling.[StageId]")]
        public Int32? TeleCallingStageId
        {
            get { return Fields.TeleCallingStageId[this]; }
            set { Fields.TeleCallingStageId[this] = value; }
        }

        [DisplayName("Tele Calling Branch Id"), Expression("jTeleCalling.[BranchId]")]
        public Int32? TeleCallingBranchId
        {
            get { return Fields.TeleCallingBranchId[this]; }
            set { Fields.TeleCallingBranchId[this] = value; }
        }

        [DisplayName("Tele Calling Representative"), Expression("jTeleCalling.[Representative]")]
        public Int32? TeleCallingRepresentative
        {
            get { return Fields.TeleCallingRepresentative[this]; }
            set { Fields.TeleCallingRepresentative[this] = value; }
        }

        [DisplayName("Tele Calling Date"), Expression("jTeleCalling.[Date]")]
        public DateTime? TeleCallingDate
        {
            get { return Fields.TeleCallingDate[this]; }
            set { Fields.TeleCallingDate[this] = value; }
        }

        [DisplayName("Tele Calling Date"), Expression("jTeleCalling.[AppointmentDate]")]
        public DateTime? TeleCallingAppointmentDate
        {
            get { return Fields.TeleCallingAppointmentDate[this]; }
            set { Fields.TeleCallingAppointmentDate[this] = value; }
        }

        [DisplayName("Tele Calling Details"), Expression("jTeleCalling.[Details]")]
        public String TeleCallingDetails
        {
            get { return Fields.TeleCallingDetails[this]; }
            set { Fields.TeleCallingDetails[this] = value; }
        }

        [DisplayName("Tele Calling Assigned To"), Expression("jTeleCalling.[AssignedTo]")]
        public Int32? TeleCallingAssignedTo
        {
            get { return Fields.TeleCallingAssignedTo[this]; }
            set { Fields.TeleCallingAssignedTo[this] = value; }
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

        


        public TeleCallingAppointmentsRow()
            : base(Fields)
        {
        }

        public TeleCallingAppointmentsRow(RowFields fields)
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
            public Int32Field TeleCallingId;
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
            public Int32Field RepresentativeBranchId;
            public StringField RepresentativeUid;
            public BooleanField RepresentativeNonOperational;

            public Int32Field TeleCallingContactsId;
            public Int32Field TeleCallingProductsId;
            public Int32Field TeleCallingStatus;
            public StringField TeleCallingFeedback;
            public Int32Field TeleCallingSourceId;
            public Int32Field TeleCallingStageId;
            public Int32Field TeleCallingBranchId;
            public Int32Field TeleCallingRepresentative;
            public DateTimeField TeleCallingDate;
            public DateTimeField TeleCallingAppointmentDate;
            public StringField TeleCallingDetails;
            public Int32Field TeleCallingAssignedTo;
            
            public StringField ContactName;
            public StringField ContactPhone;
            public StringField ContactEmail;
            public StringField ContactAddress;
            public RowListField<NoteRow> NoteList;
        }
    }
}
