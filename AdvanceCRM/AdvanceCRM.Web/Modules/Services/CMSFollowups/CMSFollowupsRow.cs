
namespace AdvanceCRM.Services
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Products;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Services"), TableName("[dbo].[CMSFollowups]")]
    [DisplayName("CMS Followups"), InstanceName("CMS Followups")]
    [ReadPermission("?")]
    [ModifyPermission("CMS:Followups")]

    public sealed class CMSFollowupsRow : Row<CMSFollowupsRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Followup Note"), Size(500), NotNull, QuickSearch,NameProperty]
        public String FollowupNote
        {
            get { return Fields.FollowupNote[this]; }
            set { Fields.FollowupNote[this] = value; }
        }

        [DisplayName("Details"), Size(500), NotNull, TextAreaEditor(Rows = 4)]
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

        [DisplayName("Representative"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jRepresentative"), TextualField("RepresentativeUsername")]
        [LookupEditor(typeof(UserRow))]
        public Int32? RepresentativeId
        {
            get { return Fields.RepresentativeId[this]; }
            set { Fields.RepresentativeId[this] = value; }
        }

        [DisplayName("CMS"), Column("CMSId"), NotNull, ForeignKey("[dbo].[CMS]", "Id"), LeftJoin("jCMS"), TextualField("CMSSerialNo")]
        public Int32? CMSId
        {
            get { return Fields.CMSId[this]; }
            set { Fields.CMSId[this] = value; }
        }

        [DisplayName("Closing Date"), Required(true)]
        public DateTime? ClosingDate
        {
            get { return Fields.ClosingDate[this]; }
            set { Fields.ClosingDate[this] = value; }
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
        
        [DisplayName("Contacts Id"), Expression("jCMS.[ContactsId]")]
        [ForeignKey("Contacts", "Id"), LeftJoin("jContacts")] //added
        public Int32? CMSContactsId
        {
            get { return Fields.CMSContactsId[this]; }
            set { Fields.CMSContactsId[this] = value; }
        }

        [DisplayName("CMS Date"), Expression("jCMS.[Date]")]
        public DateTime? CMSDate
        {
            get { return Fields.CMSDate[this]; }
            set { Fields.CMSDate[this] = value; }
        }

        [DisplayName("Products Id"), Expression("jCMS.[ProductsId]")]
        [ForeignKey("Products", "Id"), LeftJoin("jProducts")] //added
        public Int32? CMSProductsId
        {
            get { return Fields.CMSProductsId[this]; }
            set { Fields.CMSProductsId[this] = value; }
        }

        [DisplayName("CMS Serial No"), Expression("jCMS.[SerialNo]")]
        public String CMSSerialNo
        {
            get { return Fields.CMSSerialNo[this]; }
            set { Fields.CMSSerialNo[this] = value; }
        }

        [DisplayName("CMS Complaint Id"), Expression("jCMS.[ComplaintId]")]
        [ForeignKey("ComplaintType", "Id"), LeftJoin("jComplaintType")] //added
        public Int32? CMSComplaintId 
        {
            get { return Fields.CMSComplaintId [this]; }
            set { Fields.CMSComplaintId [this] = value; }
        }

        [DisplayName("CMS Category"), Expression("jCMS.[Category]")]
        public Int32? CMSCategory
        {
            get { return Fields.CMSCategory[this]; }
            set { Fields.CMSCategory[this] = value; }
        }

        [DisplayName("CMS Amount"), Expression("jCMS.[Amount]")]
        public Double? CMSAmount
        {
            get { return Fields.CMSAmount[this]; }
            set { Fields.CMSAmount[this] = value; }
        }

        [DisplayName("CMS Expected Completion"), Expression("jCMS.[ExpectedCompletion]")]
        public DateTime? CMSExpectedCompletion
        {
            get { return Fields.CMSExpectedCompletion[this]; }
            set { Fields.CMSExpectedCompletion[this] = value; }
        }

        [DisplayName("CMS Created By"), Expression("jCMS.[AssignedBy]")]
        public Int32? CMSAssignedBy
        {
            get { return Fields.CMSAssignedBy[this]; }
            set { Fields.CMSAssignedBy[this] = value; }
        }

        [DisplayName("CMS Assigned To"), Expression("jCMS.[AssignedTo]")]
        public Int32? CMSAssignedTo
        {
            get { return Fields.CMSAssignedTo[this]; }
            set { Fields.CMSAssignedTo[this] = value; }
        }

        [DisplayName("CMS Instructions"), Expression("jCMS.[Instructions]")]
        public String CMSInstructions
        {
            get { return Fields.CMSInstructions[this]; }
            set { Fields.CMSInstructions[this] = value; }
        }

        [DisplayName("CMS Branch Id"), Expression("jCMS.[BranchId]")]
        public Int32? CMSBranchId
        {
            get { return Fields.CMSBranchId[this]; }
            set { Fields.CMSBranchId[this] = value; }
        }

        [DisplayName("CMS Status"), Expression("jCMS.[Status]")]
        public Int32? CMSStatus
        {
            get { return Fields.CMSStatus[this]; }
            set { Fields.CMSStatus[this] = value; }
        }

        [DisplayName("CMS Completion Date"), Expression("jCMS.[CompletionDate]")]
        public DateTime? CMSCompletionDate
        {
            get { return Fields.CMSCompletionDate[this]; }
            set { Fields.CMSCompletionDate[this] = value; }
        }

        [DisplayName("CMS Feedback"), Expression("jCMS.[Feedback]")]
        public String CMSFeedback
        {
            get { return Fields.CMSFeedback[this]; }
            set { Fields.CMSFeedback[this] = value; }
        }

        [DisplayName("CMS Additional Info"), Expression("jCMS.[AdditionalInfo]")]
        public String CMSAdditionalInfo
        {
            get { return Fields.CMSAdditionalInfo[this]; }
            set { Fields.CMSAdditionalInfo[this] = value; }
        }

        [DisplayName("CMS Image"), Expression("jCMS.[Image]")]
        public String CMSImage
        {
            get { return Fields.CMSImage[this]; }
            set { Fields.CMSImage[this] = value; }
        }

        [DisplayName("CMS Phone"), Expression("jCMS.[Phone]")]
        public String CMSPhone
        {
            get { return Fields.CMSPhone[this]; }
            set { Fields.CMSPhone[this] = value; }
        }

        [DisplayName("CMS Address"), Expression("jCMS.[Address]")]
        public String CMSAddress
        {
            get { return Fields.CMSAddress[this]; }
            set { Fields.CMSAddress[this] = value; }
        }

        [DisplayName("CMS Stage Id"), Expression("jCMS.[StageId]")]
        public Int32? CMSStageId
        {
            get { return Fields.CMSStageId[this]; }
            set { Fields.CMSStageId[this] = value; }
        }

        [DisplayName("CMS Attachment"), Expression("jCMS.[Attachment]")]
        public String CMSAttachment
        {
            get { return Fields.CMSAttachment[this]; }
            set { Fields.CMSAttachment[this] = value; }
        }

        //[DisplayName("CMS Air Filter"), Expression("jCMS.[AirFilter]")]
        //public Double? CMSAirFilter
        //{
        //    get { return Fields.CMSAirFilter[this]; }
        //    set { Fields.CMSAirFilter[this] = value; }
        //}

        //[DisplayName("CMS Oil Filter"), Expression("jCMS.[OilFilter]")]
        //public Double? CMSOilFilter
        //{
        //    get { return Fields.CMSOilFilter[this]; }
        //    set { Fields.CMSOilFilter[this] = value; }
        //}

        //[DisplayName("CMS Oil Seperator"), Expression("jCMS.[OilSeperator]")]
        //public Double? CMSOilSeperator
        //{
        //    get { return Fields.CMSOilSeperator[this]; }
        //    set { Fields.CMSOilSeperator[this] = value; }
        //}

        //[DisplayName("CMS Oil Change"), Expression("jCMS.[OilChange]")]
        //public Double? CMSOilChange
        //{
        //    get { return Fields.CMSOilChange[this]; }
        //    set { Fields.CMSOilChange[this] = value; }
        //}

        //[DisplayName("CMS Date Of Reading"), Expression("jCMS.[DateOfReading]")]
        //public DateTime? CMSDateOfReading
        //{
        //    get { return Fields.CMSDateOfReading[this]; }
        //    set { Fields.CMSDateOfReading[this] = value; }
        //}

        //[DisplayName("CMS Hmr"), Expression("jCMS.[HMR]")]
        //public Double? CMSHmr
        //{
        //    get { return Fields.CMSHmr[this]; }
        //    set { Fields.CMSHmr[this] = value; }
        //}

        //[DisplayName("CMS Afct"), Expression("jCMS.[AFCT]")]
        //public Double? CMSAfct
        //{
        //    get { return Fields.CMSAfct[this]; }
        //    set { Fields.CMSAfct[this] = value; }
        //}

        //[DisplayName("CMS Ofct"), Expression("jCMS.[OFCT]")]
        //public Double? CMSOfct
        //{
        //    get { return Fields.CMSOfct[this]; }
        //    set { Fields.CMSOfct[this] = value; }
        //}

        //[DisplayName("CMS Osct"), Expression("jCMS.[OSCT]")]
        //public Double? CMSOsct
        //{
        //    get { return Fields.CMSOsct[this]; }
        //    set { Fields.CMSOsct[this] = value; }
        //}

        //[DisplayName("CMS Oct"), Expression("jCMS.[OCT]")]
        //public Double? CMSOct
        //{
        //    get { return Fields.CMSOct[this]; }
        //    set { Fields.CMSOct[this] = value; }
        //}

        //[DisplayName("CMS Daily Working Hours"), Expression("jCMS.[DailyWorkingHours]")]
        //public Double? CMSDailyWorkingHours
        //{
        //    get { return Fields.CMSDailyWorkingHours[this]; }
        //    set { Fields.CMSDailyWorkingHours[this] = value; }
        //}

        [DisplayName("CMS Priority"), Expression("jCMS.[Priority]")]
        public Int32? CMSPriority
        {
            get { return Fields.CMSPriority[this]; }
            set { Fields.CMSPriority[this] = value; }
        }

        [DisplayName("CMS Pmr Closed"), Expression("jCMS.[PMRClosed]")]
        public Boolean? CMSPmrClosed
        {
            get { return Fields.CMSPmrClosed[this]; }
            set { Fields.CMSPmrClosed[this] = value; }
        }

        [DisplayName("CMS Investigation By"), Expression("jCMS.[InvestigationBy]")]
        public Int32? CMSInvestigationBy
        {
            get { return Fields.CMSInvestigationBy[this]; }
            set { Fields.CMSInvestigationBy[this] = value; }
        }

        [DisplayName("CMS Action By"), Expression("jCMS.[ActionBy]")]
        public Int32? CMSActionBy
        {
            get { return Fields.CMSActionBy[this]; }
            set { Fields.CMSActionBy[this] = value; }
        }

        [DisplayName("CMS Supervised By"), Expression("jCMS.[SupervisedBy]")]
        public Int32? CMSSupervisedBy
        {
            get { return Fields.CMSSupervisedBy[this]; }
            set { Fields.CMSSupervisedBy[this] = value; }
        }

        [DisplayName("CMS Observation"), Expression("jCMS.[Observation]")]
        public String CMSObservation
        {
            get { return Fields.CMSObservation[this]; }
            set { Fields.CMSObservation[this] = value; }
        }

        [DisplayName("CMS Action"), Expression("jCMS.[Action]")]
        public String CMSAction
        {
            get { return Fields.CMSAction[this]; }
            set { Fields.CMSAction[this] = value; }
        }

        [DisplayName("CMS Comments"), Expression("jCMS.[Comments]")]
        public String CMSComments
        {
            get { return Fields.CMSComments[this]; }
            set { Fields.CMSComments[this] = value; }
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

        [DisplayName("Name"), Expression("jProducts.[Name]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ProductsName 
        { 
            get { return Fields.ProductsName[this]; } 
            set { Fields.ProductsName[this] = value; } 
        }

        [DisplayName("Complaint"), Expression("jComplaintType.[ComplaintType]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String ComplaintType 
        { 
            get { return Fields.ComplaintType[this]; } 
            set { Fields.ComplaintType[this] = value; } 
        }


        [NotesEditor, NotMapped]
        [DisplayName("")]
        public List<NoteRow> NoteList
        {
            get { return Fields.NoteList[this]; }
            set { Fields.NoteList[this] = value; }
        }
        
       

        public CMSFollowupsRow()
            : base(Fields)
        {
        }

        public CMSFollowupsRow(RowFields fields)
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
            public Int32Field RepresentativeId;
            public Int32Field CMSId;
            public DateTimeField ClosingDate;

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

            public Int32Field CMSContactsId;
            public DateTimeField CMSDate;
            public Int32Field CMSProductsId;
            public StringField CMSSerialNo;
            public Int32Field CMSComplaintId;
            public Int32Field CMSCategory;
            public DoubleField CMSAmount;
            public DateTimeField CMSExpectedCompletion;
            public Int32Field CMSAssignedBy;
            public Int32Field CMSAssignedTo;
            public StringField CMSInstructions;
            public Int32Field CMSBranchId;
            public Int32Field CMSStatus;
            public DateTimeField CMSCompletionDate;
            public StringField CMSFeedback;
            public StringField CMSAdditionalInfo;
            public StringField CMSImage;
            public StringField CMSPhone;
            public StringField CMSAddress;
            public Int32Field CMSStageId;
            public Int32Field CMSPriority;
          public StringField CMSAttachment;
            public BooleanField CMSPmrClosed;
            public Int32Field CMSInvestigationBy;
            public Int32Field CMSActionBy;
            public Int32Field CMSSupervisedBy;
            public StringField CMSObservation;
            public StringField CMSAction;
            public StringField CMSComments;
            //public Int32Field CMSCMSNo;
            //public Int32Field CMSDealerId;
            //public DateTimeField CMSPurchaseDate;
            //public StringField CMSInvoiceNo;
            //public Int32Field CMSEmployeeId;
            //public Int32Field CMSProjectId;
            //public StringField CMSCMSn;



            public StringField ContactName;
            public StringField ContactPhone;
            public StringField ContactEmail;
            public StringField ContactAddress;
            public StringField ProductsName;
            public StringField ComplaintType;
            public RowListField<NoteRow> NoteList;
        }
    }
}
