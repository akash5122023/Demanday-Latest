
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

    [ConnectionKey("Default"), Module("Services"), TableName("[dbo].[AMCVisitPlanner]")]
    [DisplayName("AMC Visit Planner"), InstanceName("AMC Visit Planner")]
    [ReadPermission("?")]
    [ModifyPermission("AMC:Visits")]

    public sealed class AMCVisitPlannerRow : Row<AMCVisitPlannerRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Visit Date"), NotNull]
        public DateTime? VisitDate
        {
            get { return Fields.VisitDate[this]; }
            set { Fields.VisitDate[this] = value; }
        }

        [DisplayName("Assigned To"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jAssignedTo"), TextualField("AssignedToUsername")]
        [Administration.UserEditor]
        public Int32? AssignedTo
        {
            get { return Fields.AssignedTo[this]; }
            set { Fields.AssignedTo[this] = value; }
        }

        [DisplayName("Status"), NotNull]
        public Masters.StatusMaster? Status
        {
            get { return (Masters.StatusMaster?)Fields.Status[this]; }
            set { Fields.Status[this] = (Int32?)value; }
        }

        [DisplayName("Completion Date"), Required(true)]
        public DateTime? CompletionDate
        {
            get { return Fields.CompletionDate[this]; }
            set { Fields.CompletionDate[this] = value; }
        }

        [DisplayName("Visit Details"), Size(700), QuickSearch, TextAreaEditor(Rows = 4),NameProperty]
        public String VisitDetails
        {
            get { return Fields.VisitDetails[this]; }
            set { Fields.VisitDetails[this] = value; }
        }

        [DisplayName("AMC"), Column("AMCId"), NotNull, ForeignKey("[dbo].[AMC]", "Id"), LeftJoin("jAMC"), TextualField("AMCAdditionalInfo")]
        public Int32? AMCId
        {
            get { return Fields.AMCId[this]; }
            set { Fields.AMCId[this] = value; }
        }

        [DisplayName("Serial No"), Size(200)]
        public String Serial
        {
            get { return Fields.Serial[this]; }
            set { Fields.Serial[this] = value; }
        }

        [DisplayName("Attachment"), Size(1000)]
        [MultipleImageUploadEditor(FilenameFormat = "AMCVisits/~", CopyToHistory = true, AllowNonImage = true)]
        public String Attachment
        {
            get { return Fields.Attachment[this]; }
            set { Fields.Attachment[this] = value; }
        }

        [DisplayName("Representative"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jRepresentative"), TextualField("RepresentativeUsername")]
        [Administration.UserEditor]
        public Int32? RepresentativeId
        {
            get { return Fields.RepresentativeId[this]; }
            set { Fields.RepresentativeId[this] = value; }
        }

        [DisplayName("Assigned To"), Expression("jAssignedTo.[Username]")]
        public String AssignedToUsername
        {
            get { return Fields.AssignedToUsername[this]; }
            set { Fields.AssignedToUsername[this] = value; }
        }

        [DisplayName("Assigned To Display Name"), Expression("jAssignedTo.[DisplayName]")]
        public String AssignedToDisplayName
        {
            get { return Fields.AssignedToDisplayName[this]; }
            set { Fields.AssignedToDisplayName[this] = value; }
        }

        [DisplayName("Assigned To Email"), Expression("jAssignedTo.[Email]")]
        public String AssignedToEmail
        {
            get { return Fields.AssignedToEmail[this]; }
            set { Fields.AssignedToEmail[this] = value; }
        }

        [DisplayName("Assigned To Source"), Expression("jAssignedTo.[Source]")]
        public String AssignedToSource
        {
            get { return Fields.AssignedToSource[this]; }
            set { Fields.AssignedToSource[this] = value; }
        }

        [DisplayName("Assigned To Password Hash"), Expression("jAssignedTo.[PasswordHash]")]
        public String AssignedToPasswordHash
        {
            get { return Fields.AssignedToPasswordHash[this]; }
            set { Fields.AssignedToPasswordHash[this] = value; }
        }

        [DisplayName("Assigned To Password Salt"), Expression("jAssignedTo.[PasswordSalt]")]
        public String AssignedToPasswordSalt
        {
            get { return Fields.AssignedToPasswordSalt[this]; }
            set { Fields.AssignedToPasswordSalt[this] = value; }
        }

        [DisplayName("Assigned To Last Directory Update"), Expression("jAssignedTo.[LastDirectoryUpdate]")]
        public DateTime? AssignedToLastDirectoryUpdate
        {
            get { return Fields.AssignedToLastDirectoryUpdate[this]; }
            set { Fields.AssignedToLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Assigned To User Image"), Expression("jAssignedTo.[UserImage]")]
        public String AssignedToUserImage
        {
            get { return Fields.AssignedToUserImage[this]; }
            set { Fields.AssignedToUserImage[this] = value; }
        }

        [DisplayName("Assigned To Insert Date"), Expression("jAssignedTo.[InsertDate]")]
        public DateTime? AssignedToInsertDate
        {
            get { return Fields.AssignedToInsertDate[this]; }
            set { Fields.AssignedToInsertDate[this] = value; }
        }

        [DisplayName("Assigned To Insert User Id"), Expression("jAssignedTo.[InsertUserId]")]
        public Int32? AssignedToInsertUserId
        {
            get { return Fields.AssignedToInsertUserId[this]; }
            set { Fields.AssignedToInsertUserId[this] = value; }
        }

        [DisplayName("Assigned To Update Date"), Expression("jAssignedTo.[UpdateDate]")]
        public DateTime? AssignedToUpdateDate
        {
            get { return Fields.AssignedToUpdateDate[this]; }
            set { Fields.AssignedToUpdateDate[this] = value; }
        }

        [DisplayName("Assigned To Update User Id"), Expression("jAssignedTo.[UpdateUserId]")]
        public Int32? AssignedToUpdateUserId
        {
            get { return Fields.AssignedToUpdateUserId[this]; }
            set { Fields.AssignedToUpdateUserId[this] = value; }
        }

        [DisplayName("Assigned To Is Active"), Expression("jAssignedTo.[IsActive]")]
        public Int16? AssignedToIsActive
        {
            get { return Fields.AssignedToIsActive[this]; }
            set { Fields.AssignedToIsActive[this] = value; }
        }

        [DisplayName("Assigned To Upper Level"), Expression("jAssignedTo.[UpperLevel]")]
        public Int32? AssignedToUpperLevel
        {
            get { return Fields.AssignedToUpperLevel[this]; }
            set { Fields.AssignedToUpperLevel[this] = value; }
        }

        [DisplayName("Assigned To Upper Level2"), Expression("jAssignedTo.[UpperLevel2]")]
        public Int32? AssignedToUpperLevel2
        {
            get { return Fields.AssignedToUpperLevel2[this]; }
            set { Fields.AssignedToUpperLevel2[this] = value; }
        }

        [DisplayName("Assigned To Upper Level3"), Expression("jAssignedTo.[UpperLevel3]")]
        public Int32? AssignedToUpperLevel3
        {
            get { return Fields.AssignedToUpperLevel3[this]; }
            set { Fields.AssignedToUpperLevel3[this] = value; }
        }

        [DisplayName("Assigned To Upper Level4"), Expression("jAssignedTo.[UpperLevel4]")]
        public Int32? AssignedToUpperLevel4
        {
            get { return Fields.AssignedToUpperLevel4[this]; }
            set { Fields.AssignedToUpperLevel4[this] = value; }
        }

        [DisplayName("Assigned To Upper Level5"), Expression("jAssignedTo.[UpperLevel5]")]
        public Int32? AssignedToUpperLevel5
        {
            get { return Fields.AssignedToUpperLevel5[this]; }
            set { Fields.AssignedToUpperLevel5[this] = value; }
        }

        [DisplayName("Assigned To Host"), Expression("jAssignedTo.[Host]")]
        public String AssignedToHost
        {
            get { return Fields.AssignedToHost[this]; }
            set { Fields.AssignedToHost[this] = value; }
        }

        [DisplayName("Assigned To Port"), Expression("jAssignedTo.[Port]")]
        public Int32? AssignedToPort
        {
            get { return Fields.AssignedToPort[this]; }
            set { Fields.AssignedToPort[this] = value; }
        }

        [DisplayName("Assigned To SSL"), Expression("jAssignedTo.[SSL]")]
        public Boolean? AssignedToSsl
        {
            get { return Fields.AssignedToSsl[this]; }
            set { Fields.AssignedToSsl[this] = value; }
        }

        [DisplayName("Assigned To Email Id"), Expression("jAssignedTo.[EmailId]")]
        public String AssignedToEmailId
        {
            get { return Fields.AssignedToEmailId[this]; }
            set { Fields.AssignedToEmailId[this] = value; }
        }

        [DisplayName("Assigned To Email Password"), Expression("jAssignedTo.[EmailPassword]")]
        public String AssignedToEmailPassword
        {
            get { return Fields.AssignedToEmailPassword[this]; }
            set { Fields.AssignedToEmailPassword[this] = value; }
        }

        [DisplayName("Assigned To Phone"), Expression("jAssignedTo.[Phone]")]
        public String AssignedToPhone
        {
            get { return Fields.AssignedToPhone[this]; }
            set { Fields.AssignedToPhone[this] = value; }
        }

        [DisplayName("Assigned To Mcsmtp Server"), Expression("jAssignedTo.[MCSMTPServer]")]
        public String AssignedToMcsmtpServer
        {
            get { return Fields.AssignedToMcsmtpServer[this]; }
            set { Fields.AssignedToMcsmtpServer[this] = value; }
        }

        [DisplayName("Assigned To Mcsmtp Port"), Expression("jAssignedTo.[MCSMTPPort]")]
        public Int32? AssignedToMcsmtpPort
        {
            get { return Fields.AssignedToMcsmtpPort[this]; }
            set { Fields.AssignedToMcsmtpPort[this] = value; }
        }

        [DisplayName("Assigned To Mcimap Server"), Expression("jAssignedTo.[MCIMAPServer]")]
        public String AssignedToMcimapServer
        {
            get { return Fields.AssignedToMcimapServer[this]; }
            set { Fields.AssignedToMcimapServer[this] = value; }
        }

        [DisplayName("Assigned To Mcimap Port"), Expression("jAssignedTo.[MCIMAPPort]")]
        public Int32? AssignedToMcimapPort
        {
            get { return Fields.AssignedToMcimapPort[this]; }
            set { Fields.AssignedToMcimapPort[this] = value; }
        }

        [DisplayName("Assigned To Mc Username"), Expression("jAssignedTo.[MCUsername]")]
        public String AssignedToMcUsername
        {
            get { return Fields.AssignedToMcUsername[this]; }
            set { Fields.AssignedToMcUsername[this] = value; }
        }

        [DisplayName("Assigned To Mc Password"), Expression("jAssignedTo.[MCPassword]")]
        public String AssignedToMcPassword
        {
            get { return Fields.AssignedToMcPassword[this]; }
            set { Fields.AssignedToMcPassword[this] = value; }
        }

        [DisplayName("Assigned To Start Time"), Expression("jAssignedTo.[StartTime]")]
        public String AssignedToStartTime
        {
            get { return Fields.AssignedToStartTime[this]; }
            set { Fields.AssignedToStartTime[this] = value; }
        }

        [DisplayName("Assigned To End Time"), Expression("jAssignedTo.[EndTime]")]
        public String AssignedToEndTime
        {
            get { return Fields.AssignedToEndTime[this]; }
            set { Fields.AssignedToEndTime[this] = value; }
        }

        [DisplayName("Assigned To Branch Id"), Expression("jAssignedTo.[BranchId]")]
        public Int32? AssignedToBranchId
        {
            get { return Fields.AssignedToBranchId[this]; }
            set { Fields.AssignedToBranchId[this] = value; }
        }

        [DisplayName("Assigned To Uid"), Expression("jAssignedTo.[UID]")]
        public String AssignedToUid
        {
            get { return Fields.AssignedToUid[this]; }
            set { Fields.AssignedToUid[this] = value; }
        }

        [DisplayName("Assigned To Non Operational"), Expression("jAssignedTo.[NonOperational]")]
        public Boolean? AssignedToNonOperational
        {
            get { return Fields.AssignedToNonOperational[this]; }
            set { Fields.AssignedToNonOperational[this] = value; }
        }

        [DisplayName("AMC Date"), Expression("jAMC.[Date]")]
        public DateTime? AMCDate
        {
            get { return Fields.AMCDate[this]; }
            set { Fields.AMCDate[this] = value; }
        }

        [DisplayName("AMC Contacts Id"), Expression("jAMC.[ContactsId]")]
        [ForeignKey("Contacts", "Id"), LeftJoin("jContacts")] //added
        public Int32? AMCContactsId
        {
            get { return Fields.AMCContactsId[this]; }
            set { Fields.AMCContactsId[this] = value; }
        }

        [DisplayName("AMC Status"), Expression("jAMC.[Status]")]
        public Int32? AMCStatus
        {
            get { return Fields.AMCStatus[this]; }
            set { Fields.AMCStatus[this] = value; }
        }

        [DisplayName("AMC Start Date"), Expression("jAMC.[StartDate]")]
        public DateTime? AMCStartDate
        {
            get { return Fields.AMCStartDate[this]; }
            set { Fields.AMCStartDate[this] = value; }
        }

        [DisplayName("AMC End Date"), Expression("jAMC.[EndDate]")]
        public DateTime? AMCEndDate
        {
            get { return Fields.AMCEndDate[this]; }
            set { Fields.AMCEndDate[this] = value; }
        }

        [DisplayName("AMC Additional Info"), Expression("jAMC.[AdditionalInfo]")]
        public String AMCAdditionalInfo
        {
            get { return Fields.AMCAdditionalInfo[this]; }
            set { Fields.AMCAdditionalInfo[this] = value; }
        }

        [DisplayName("AMC Owner Id"), Expression("jAMC.[OwnerId]")]
        public Int32? AMCOwnerId
        {
            get { return Fields.AMCOwnerId[this]; }
            set { Fields.AMCOwnerId[this] = value; }
        }

        [DisplayName("AMC Assigned Id"), Expression("jAMC.[AssignedId]")]
        public Int32? AMCAssignedId
        {
            get { return Fields.AMCAssignedId[this]; }
            set { Fields.AMCAssignedId[this] = value; }
        }

        [DisplayName("AMC Attachment"), Expression("jAMC.[Attachment]")]
        public String AMCAttachment
        {
            get { return Fields.AMCAttachment[this]; }
            set { Fields.AMCAttachment[this] = value; }
        }
        [DisplayName("AMC Lines"), Expression("jAMC.[Lines]")]
        public Int32? AMCLines
        {
            get { return Fields.AMCLines[this]; }
            set { Fields.AMCLines[this] = value; }
        }

        //[DisplayName("AMC Terms"), Expression("jAMC.[Terms]")]
        //public String AMCTerms
        //{
        //    get { return Fields.AMCTerms[this]; }
        //    set { Fields.AMCTerms[this] = value; }
        //}

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
        
        [DisplayName("Name"), Expression("jContacts.[Name]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactName { get { return Fields.ContactName[this]; } set { Fields.ContactName[this] = value; } }

        [DisplayName("Phone"), Expression("jContacts.[Phone]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactPhone { get { return Fields.ContactPhone[this]; } set { Fields.ContactPhone[this] = value; } }

        [DisplayName("Email"), Expression("jContacts.[Email]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactEmail
        {
            get { return Fields.ContactEmail[this]; }
            set { Fields.ContactEmail[this] = value; }
        }

        [DisplayName("Address"), Expression("jContacts.[Address]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ContactAddress { get { return Fields.ContactAddress[this]; } set { Fields.ContactAddress[this] = value; } }
        
        [NotesEditor, NotMapped]
        [DisplayName("")]
        public List<NoteRow> NoteList
        {
            get { return Fields.NoteList[this]; }
            set { Fields.NoteList[this] = value; }
        }

       
        public AMCVisitPlannerRow()
            : base(Fields)
        {
        }
        
       
        public AMCVisitPlannerRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public DateTimeField VisitDate;
            public Int32Field AssignedTo;
            public Int32Field Status;
            public DateTimeField CompletionDate;
            public StringField VisitDetails;
            public Int32Field AMCId;
            public StringField Serial;
            public StringField Attachment;
            public Int32Field RepresentativeId;

            public StringField AssignedToUsername;
            public StringField AssignedToDisplayName;
            public StringField AssignedToEmail;
            public StringField AssignedToSource;
            public StringField AssignedToPasswordHash;
            public StringField AssignedToPasswordSalt;
            public DateTimeField AssignedToLastDirectoryUpdate;
            public StringField AssignedToUserImage;
            public DateTimeField AssignedToInsertDate;
            public Int32Field AssignedToInsertUserId;
            public DateTimeField AssignedToUpdateDate;
            public Int32Field AssignedToUpdateUserId;
            public Int16Field AssignedToIsActive;
            public Int32Field AssignedToUpperLevel;
            public Int32Field AssignedToUpperLevel2;
            public Int32Field AssignedToUpperLevel3;
            public Int32Field AssignedToUpperLevel4;
            public Int32Field AssignedToUpperLevel5;
            public StringField AssignedToHost;
            public Int32Field AssignedToPort;
            public BooleanField AssignedToSsl;
            public StringField AssignedToEmailId;
            public StringField AssignedToEmailPassword;
            public StringField AssignedToPhone;
            public StringField AssignedToMcsmtpServer;
            public Int32Field AssignedToMcsmtpPort;
            public StringField AssignedToMcimapServer;
            public Int32Field AssignedToMcimapPort;
            public StringField AssignedToMcUsername;
            public StringField AssignedToMcPassword;
            public StringField AssignedToStartTime;
            public StringField AssignedToEndTime;
            public Int32Field AssignedToBranchId;
            public StringField AssignedToUid;
            public BooleanField AssignedToNonOperational;

            public DateTimeField AMCDate;
            public Int32Field AMCContactsId;
            public Int32Field AMCStatus;
            public DateTimeField AMCStartDate;
            public DateTimeField AMCEndDate;
            public StringField AMCAdditionalInfo;
            public Int32Field AMCOwnerId;
            public Int32Field AMCAssignedId;
            public StringField AMCAttachment;
            
            public Int32Field AMCLines;
            //public StringField AMCTerms;

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
            public RowListField<NoteRow> NoteList;
        }
    }
}
