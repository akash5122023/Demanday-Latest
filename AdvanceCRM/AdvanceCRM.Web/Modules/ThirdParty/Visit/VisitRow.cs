
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[Visit]")]
    [DisplayName("Visit"), InstanceName("Visit")]
    [ReadPermission("Visit:Inbox")]
    [ModifyPermission("Visit:Inbox")]
    public sealed class VisitRow : Row<VisitRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }
        [DisplayName("Feedback"), Size(500),QuickSearch]
        public String Feedback
        {
            get { return Fields.Feedback[this]; }
            set { Fields.Feedback[this] = value; }
        }

        [DisplayName("Company Name"), Size(255), NotNull, QuickSearch,NameProperty]
        public String CompanyName
        {
            get { return Fields.CompanyName[this]; }
            set { Fields.CompanyName[this] = value; }
        }

        [DisplayName("Name"), Size(255),QuickSearch]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Address"), Size(1000)]
        public String Address
        {
            get { return Fields.Address[this]; }
            set { Fields.Address[this] = value; }
        }

        [DisplayName("Email"), Size(255), QuickSearch]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("Mobile No"), Size(255), QuickSearch]
        public String MobileNo
        {
            get { return Fields.MobileNo[this]; }
            set { Fields.MobileNo[this] = value; }
        }

        [DisplayName("Location"), Size(500)]
        public String Location
        {
            get { return Fields.Location[this]; }
            set { Fields.Location[this] = value; }
        }
        [DisplayName("Date"), NotNull]
        public DateTime? DateNTime
        {
            get { return Fields.DateNTime[this]; }
            set { Fields.DateNTime[this] = value; }
        }

        [DisplayName("Requirements"), Size(1000), NotNull]
        public String Requirements
        {
            get { return Fields.Requirements[this]; }
            set { Fields.Requirements[this] = value; }
        }

        [DisplayName("Purpose"), Size(200), NotNull]
       // [LookupEditor(typeof(Masters.PurposeRow),InplaceAdd =false)]
        public String Purpose
        {
            get { return Fields.Purpose[this]; }
            set { Fields.Purpose[this] = value; }
        }

        [DisplayName("Attachments"), Size(2000), NotNull]
        [MultipleImageUploadEditor(FilenameFormat = "Visit/~", CopyToHistory = true, AllowNonImage = true)]
        public String Attachments
        {
            get { return Fields.Attachments[this]; }
            set { Fields.Attachments[this] = value; }
        }

        [DisplayName("Is Moved"), NotNull]

        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }
        [DisplayName("Created By"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jCreatedBy"), TextualField("CreatedByUsername")]
        [Administration.UserEditor]
        public Int32? CreatedBy
        {
            get { return Fields.CreatedBy[this]; }
            set { Fields.CreatedBy[this] = value; }
        }

        [DisplayName("Created By Username"), Expression("jCreatedBy.[Username]")]
        public String CreatedByUsername
        {
            get { return Fields.CreatedByUsername[this]; }
            set { Fields.CreatedByUsername[this] = value; }
        }

        [DisplayName("CreatedBy"), Expression("jCreatedBy.[DisplayName]")]
        public String CreatedByDisplayName
        {
            get { return Fields.CreatedByDisplayName[this]; }
            set { Fields.CreatedByDisplayName[this] = value; }
        }

        [DisplayName("Created By Email"), Expression("jCreatedBy.[Email]")]
        public String CreatedByEmail
        {
            get { return Fields.CreatedByEmail[this]; }
            set { Fields.CreatedByEmail[this] = value; }
        }

        [DisplayName("Created By Source"), Expression("jCreatedBy.[Source]")]
        public String CreatedBySource
        {
            get { return Fields.CreatedBySource[this]; }
            set { Fields.CreatedBySource[this] = value; }
        }

        [DisplayName("Created By Password Hash"), Expression("jCreatedBy.[PasswordHash]")]
        public String CreatedByPasswordHash
        {
            get { return Fields.CreatedByPasswordHash[this]; }
            set { Fields.CreatedByPasswordHash[this] = value; }
        }

        [DisplayName("Created By Password Salt"), Expression("jCreatedBy.[PasswordSalt]")]
        public String CreatedByPasswordSalt
        {
            get { return Fields.CreatedByPasswordSalt[this]; }
            set { Fields.CreatedByPasswordSalt[this] = value; }
        }

        [DisplayName("Created By Last Directory Update"), Expression("jCreatedBy.[LastDirectoryUpdate]")]
        public DateTime? CreatedByLastDirectoryUpdate
        {
            get { return Fields.CreatedByLastDirectoryUpdate[this]; }
            set { Fields.CreatedByLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Created By User Image"), Expression("jCreatedBy.[UserImage]")]
        public String CreatedByUserImage
        {
            get { return Fields.CreatedByUserImage[this]; }
            set { Fields.CreatedByUserImage[this] = value; }
        }

        [DisplayName("Created By Insert Date"), Expression("jCreatedBy.[InsertDate]")]
        public DateTime? CreatedByInsertDate
        {
            get { return Fields.CreatedByInsertDate[this]; }
            set { Fields.CreatedByInsertDate[this] = value; }
        }

        [DisplayName("Created By Insert User Id"), Expression("jCreatedBy.[InsertUserId]")]
        public Int32? CreatedByInsertUserId
        {
            get { return Fields.CreatedByInsertUserId[this]; }
            set { Fields.CreatedByInsertUserId[this] = value; }
        }

        [DisplayName("Created By Update Date"), Expression("jCreatedBy.[UpdateDate]")]
        public DateTime? CreatedByUpdateDate
        {
            get { return Fields.CreatedByUpdateDate[this]; }
            set { Fields.CreatedByUpdateDate[this] = value; }
        }

        [DisplayName("Created By Update User Id"), Expression("jCreatedBy.[UpdateUserId]")]
        public Int32? CreatedByUpdateUserId
        {
            get { return Fields.CreatedByUpdateUserId[this]; }
            set { Fields.CreatedByUpdateUserId[this] = value; }
        }

        [DisplayName("Created By Is Active"), Expression("jCreatedBy.[IsActive]")]
        public Int16? CreatedByIsActive
        {
            get { return Fields.CreatedByIsActive[this]; }
            set { Fields.CreatedByIsActive[this] = value; }
        }

        [DisplayName("Created By Upper Level"), Expression("jCreatedBy.[UpperLevel]")]
        public Int32? CreatedByUpperLevel
        {
            get { return Fields.CreatedByUpperLevel[this]; }
            set { Fields.CreatedByUpperLevel[this] = value; }
        }

        [DisplayName("Created By Upper Level2"), Expression("jCreatedBy.[UpperLevel2]")]
        public Int32? CreatedByUpperLevel2
        {
            get { return Fields.CreatedByUpperLevel2[this]; }
            set { Fields.CreatedByUpperLevel2[this] = value; }
        }

        [DisplayName("Created By Upper Level3"), Expression("jCreatedBy.[UpperLevel3]")]
        public Int32? CreatedByUpperLevel3
        {
            get { return Fields.CreatedByUpperLevel3[this]; }
            set { Fields.CreatedByUpperLevel3[this] = value; }
        }

        [DisplayName("Created By Upper Level4"), Expression("jCreatedBy.[UpperLevel4]")]
        public Int32? CreatedByUpperLevel4
        {
            get { return Fields.CreatedByUpperLevel4[this]; }
            set { Fields.CreatedByUpperLevel4[this] = value; }
        }

        [DisplayName("Created By Upper Level5"), Expression("jCreatedBy.[UpperLevel5]")]
        public Int32? CreatedByUpperLevel5
        {
            get { return Fields.CreatedByUpperLevel5[this]; }
            set { Fields.CreatedByUpperLevel5[this] = value; }
        }

        [DisplayName("Created By Host"), Expression("jCreatedBy.[Host]")]
        public String CreatedByHost
        {
            get { return Fields.CreatedByHost[this]; }
            set { Fields.CreatedByHost[this] = value; }
        }

        [DisplayName("Created By Port"), Expression("jCreatedBy.[Port]")]
        public Int32? CreatedByPort
        {
            get { return Fields.CreatedByPort[this]; }
            set { Fields.CreatedByPort[this] = value; }
        }

        [DisplayName("Created By Ssl"), Expression("jCreatedBy.[SSL]")]
        public Boolean? CreatedBySsl
        {
            get { return Fields.CreatedBySsl[this]; }
            set { Fields.CreatedBySsl[this] = value; }
        }

        [DisplayName("Created By Email Id"), Expression("jCreatedBy.[EmailId]")]
        public String CreatedByEmailId
        {
            get { return Fields.CreatedByEmailId[this]; }
            set { Fields.CreatedByEmailId[this] = value; }
        }

        [DisplayName("Created By Email Password"), Expression("jCreatedBy.[EmailPassword]")]
        public String CreatedByEmailPassword
        {
            get { return Fields.CreatedByEmailPassword[this]; }
            set { Fields.CreatedByEmailPassword[this] = value; }
        }

        [DisplayName("Created By Phone"), Expression("jCreatedBy.[Phone]")]
        public String CreatedByPhone
        {
            get { return Fields.CreatedByPhone[this]; }
            set { Fields.CreatedByPhone[this] = value; }
        }

        [DisplayName("Created By Mcsmtp Server"), Expression("jCreatedBy.[MCSMTPServer]")]
        public String CreatedByMcsmtpServer
        {
            get { return Fields.CreatedByMcsmtpServer[this]; }
            set { Fields.CreatedByMcsmtpServer[this] = value; }
        }

        [DisplayName("Created By Mcsmtp Port"), Expression("jCreatedBy.[MCSMTPPort]")]
        public Int32? CreatedByMcsmtpPort
        {
            get { return Fields.CreatedByMcsmtpPort[this]; }
            set { Fields.CreatedByMcsmtpPort[this] = value; }
        }

        [DisplayName("Created By Mcimap Server"), Expression("jCreatedBy.[MCIMAPServer]")]
        public String CreatedByMcimapServer
        {
            get { return Fields.CreatedByMcimapServer[this]; }
            set { Fields.CreatedByMcimapServer[this] = value; }
        }

        [DisplayName("Created By Mcimap Port"), Expression("jCreatedBy.[MCIMAPPort]")]
        public Int32? CreatedByMcimapPort
        {
            get { return Fields.CreatedByMcimapPort[this]; }
            set { Fields.CreatedByMcimapPort[this] = value; }
        }

        [DisplayName("Created By Mc Username"), Expression("jCreatedBy.[MCUsername]")]
        public String CreatedByMcUsername
        {
            get { return Fields.CreatedByMcUsername[this]; }
            set { Fields.CreatedByMcUsername[this] = value; }
        }

        [DisplayName("Created By Mc Password"), Expression("jCreatedBy.[MCPassword]")]
        public String CreatedByMcPassword
        {
            get { return Fields.CreatedByMcPassword[this]; }
            set { Fields.CreatedByMcPassword[this] = value; }
        }

        [DisplayName("Created By Start Time"), Expression("jCreatedBy.[StartTime]")]
        public String CreatedByStartTime
        {
            get { return Fields.CreatedByStartTime[this]; }
            set { Fields.CreatedByStartTime[this] = value; }
        }

        [DisplayName("Created By End Time"), Expression("jCreatedBy.[EndTime]")]
        public String CreatedByEndTime
        {
            get { return Fields.CreatedByEndTime[this]; }
            set { Fields.CreatedByEndTime[this] = value; }
        }

        [DisplayName("Created By Uid"), Expression("jCreatedBy.[UID]")]
        public String CreatedByUid
        {
            get { return Fields.CreatedByUid[this]; }
            set { Fields.CreatedByUid[this] = value; }
        }

        [DisplayName("Created By Non Operational"), Expression("jCreatedBy.[NonOperational]")]
        public Boolean? CreatedByNonOperational
        {
            get { return Fields.CreatedByNonOperational[this]; }
            set { Fields.CreatedByNonOperational[this] = value; }
        }

        [DisplayName("Created By Branch Id"), Expression("jCreatedBy.[BranchId]")]
        public Int32? CreatedByBranchId
        {
            get { return Fields.CreatedByBranchId[this]; }
            set { Fields.CreatedByBranchId[this] = value; }
        }

        [DisplayName("Created By Company Id"), Expression("jCreatedBy.[CompanyId]")]
        public Int32? CreatedByCompanyId
        {
            get { return Fields.CreatedByCompanyId[this]; }
            set { Fields.CreatedByCompanyId[this] = value; }
        }

      
        public VisitRow()
            : base(Fields)
        {
        }
        
        public VisitRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField CompanyName;
            public StringField Name;
            public StringField Address;
            public StringField Email;
            public StringField MobileNo;
            public StringField Location;
            public DateTimeField DateNTime;
            public StringField Requirements;
            public StringField Purpose;
            public StringField Attachments;
            public BooleanField IsMoved;
            public Int32Field CreatedBy;
            public StringField Feedback;

            public StringField CreatedByUsername;
            public StringField CreatedByDisplayName;
            public StringField CreatedByEmail;
            public StringField CreatedBySource;
            public StringField CreatedByPasswordHash;
            public StringField CreatedByPasswordSalt;
            public DateTimeField CreatedByLastDirectoryUpdate;
            public StringField CreatedByUserImage;
            public DateTimeField CreatedByInsertDate;
            public Int32Field CreatedByInsertUserId;
            public DateTimeField CreatedByUpdateDate;
            public Int32Field CreatedByUpdateUserId;
            public Int16Field CreatedByIsActive;
            public Int32Field CreatedByUpperLevel;
            public Int32Field CreatedByUpperLevel2;
            public Int32Field CreatedByUpperLevel3;
            public Int32Field CreatedByUpperLevel4;
            public Int32Field CreatedByUpperLevel5;
            public StringField CreatedByHost;
            public Int32Field CreatedByPort;
            public BooleanField CreatedBySsl;
            public StringField CreatedByEmailId;
            public StringField CreatedByEmailPassword;
            public StringField CreatedByPhone;
            public StringField CreatedByMcsmtpServer;
            public Int32Field CreatedByMcsmtpPort;
            public StringField CreatedByMcimapServer;
            public Int32Field CreatedByMcimapPort;
            public StringField CreatedByMcUsername;
            public StringField CreatedByMcPassword;
            public StringField CreatedByStartTime;
            public StringField CreatedByEndTime;
            public StringField CreatedByUid;
            public BooleanField CreatedByNonOperational;
            public Int32Field CreatedByBranchId;
            public Int32Field CreatedByCompanyId;
        }
    }
}
