
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[Teams]")]
    [DisplayName("Teams"), InstanceName("Teams")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.Teams", Permission = "?")]
    public sealed class TeamsRow : Row<TeamsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Team"), Size(100), NotNull, QuickSearch,NameProperty]
        public String Team
        {
            get { return Fields.Team[this]; }
            set { Fields.Team[this] = value; }
        }

        [DisplayName("Manager/TeamLead"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUser"), TextualField("UserUsername")]
        [Administration.UserEditor]
        public Int32? UserId
        {
            get { return Fields.UserId[this]; }
            set { Fields.UserId[this] = value; }
        }

        [DisplayName("Manager/TeamLead"), Expression("jUser.[Username]")]
        public String UserUsername
        {
            get { return Fields.UserUsername[this]; }
            set { Fields.UserUsername[this] = value; }
        }

        [DisplayName("User Display Name"), Expression("jUser.[DisplayName]")]
        public String UserDisplayName
        {
            get { return Fields.UserDisplayName[this]; }
            set { Fields.UserDisplayName[this] = value; }
        }

        [DisplayName("User Email"), Expression("jUser.[Email]")]
        public String UserEmail
        {
            get { return Fields.UserEmail[this]; }
            set { Fields.UserEmail[this] = value; }
        }

        [DisplayName("User Source"), Expression("jUser.[Source]")]
        public String UserSource
        {
            get { return Fields.UserSource[this]; }
            set { Fields.UserSource[this] = value; }
        }

        [DisplayName("User Password Hash"), Expression("jUser.[PasswordHash]")]
        public String UserPasswordHash
        {
            get { return Fields.UserPasswordHash[this]; }
            set { Fields.UserPasswordHash[this] = value; }
        }

        [DisplayName("User Password Salt"), Expression("jUser.[PasswordSalt]")]
        public String UserPasswordSalt
        {
            get { return Fields.UserPasswordSalt[this]; }
            set { Fields.UserPasswordSalt[this] = value; }
        }

        [DisplayName("User Last Directory Update"), Expression("jUser.[LastDirectoryUpdate]")]
        public DateTime? UserLastDirectoryUpdate
        {
            get { return Fields.UserLastDirectoryUpdate[this]; }
            set { Fields.UserLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("User User Image"), Expression("jUser.[UserImage]")]
        public String UserUserImage
        {
            get { return Fields.UserUserImage[this]; }
            set { Fields.UserUserImage[this] = value; }
        }

        [DisplayName("User Insert Date"), Expression("jUser.[InsertDate]")]
        public DateTime? UserInsertDate
        {
            get { return Fields.UserInsertDate[this]; }
            set { Fields.UserInsertDate[this] = value; }
        }

        [DisplayName("User Insert User Id"), Expression("jUser.[InsertUserId]")]
        public Int32? UserInsertUserId
        {
            get { return Fields.UserInsertUserId[this]; }
            set { Fields.UserInsertUserId[this] = value; }
        }

        [DisplayName("User Update Date"), Expression("jUser.[UpdateDate]")]
        public DateTime? UserUpdateDate
        {
            get { return Fields.UserUpdateDate[this]; }
            set { Fields.UserUpdateDate[this] = value; }
        }

        [DisplayName("User Update User Id"), Expression("jUser.[UpdateUserId]")]
        public Int32? UserUpdateUserId
        {
            get { return Fields.UserUpdateUserId[this]; }
            set { Fields.UserUpdateUserId[this] = value; }
        }

        [DisplayName("User Is Active"), Expression("jUser.[IsActive]")]
        public Int16? UserIsActive
        {
            get { return Fields.UserIsActive[this]; }
            set { Fields.UserIsActive[this] = value; }
        }

        [DisplayName("User Upper Level"), Expression("jUser.[UpperLevel]")]
        public Int32? UserUpperLevel
        {
            get { return Fields.UserUpperLevel[this]; }
            set { Fields.UserUpperLevel[this] = value; }
        }

        [DisplayName("User Upper Level2"), Expression("jUser.[UpperLevel2]")]
        public Int32? UserUpperLevel2
        {
            get { return Fields.UserUpperLevel2[this]; }
            set { Fields.UserUpperLevel2[this] = value; }
        }

        [DisplayName("User Upper Level3"), Expression("jUser.[UpperLevel3]")]
        public Int32? UserUpperLevel3
        {
            get { return Fields.UserUpperLevel3[this]; }
            set { Fields.UserUpperLevel3[this] = value; }
        }

        [DisplayName("User Upper Level4"), Expression("jUser.[UpperLevel4]")]
        public Int32? UserUpperLevel4
        {
            get { return Fields.UserUpperLevel4[this]; }
            set { Fields.UserUpperLevel4[this] = value; }
        }

        [DisplayName("User Upper Level5"), Expression("jUser.[UpperLevel5]")]
        public Int32? UserUpperLevel5
        {
            get { return Fields.UserUpperLevel5[this]; }
            set { Fields.UserUpperLevel5[this] = value; }
        }

        [DisplayName("User Host"), Expression("jUser.[Host]")]
        public String UserHost
        {
            get { return Fields.UserHost[this]; }
            set { Fields.UserHost[this] = value; }
        }

        [DisplayName("User Port"), Expression("jUser.[Port]")]
        public Int32? UserPort
        {
            get { return Fields.UserPort[this]; }
            set { Fields.UserPort[this] = value; }
        }

        [DisplayName("User Ssl"), Expression("jUser.[SSL]")]
        public Boolean? UserSsl
        {
            get { return Fields.UserSsl[this]; }
            set { Fields.UserSsl[this] = value; }
        }

        [DisplayName("User Email Id"), Expression("jUser.[EmailId]")]
        public String UserEmailId
        {
            get { return Fields.UserEmailId[this]; }
            set { Fields.UserEmailId[this] = value; }
        }

        [DisplayName("User Email Password"), Expression("jUser.[EmailPassword]")]
        public String UserEmailPassword
        {
            get { return Fields.UserEmailPassword[this]; }
            set { Fields.UserEmailPassword[this] = value; }
        }

        [DisplayName("User Phone"), Expression("jUser.[Phone]")]
        public String UserPhone
        {
            get { return Fields.UserPhone[this]; }
            set { Fields.UserPhone[this] = value; }
        }

        [DisplayName("User Mcsmtp Server"), Expression("jUser.[MCSMTPServer]")]
        public String UserMcsmtpServer
        {
            get { return Fields.UserMcsmtpServer[this]; }
            set { Fields.UserMcsmtpServer[this] = value; }
        }

        [DisplayName("User Mcsmtp Port"), Expression("jUser.[MCSMTPPort]")]
        public Int32? UserMcsmtpPort
        {
            get { return Fields.UserMcsmtpPort[this]; }
            set { Fields.UserMcsmtpPort[this] = value; }
        }

        [DisplayName("User Mcimap Server"), Expression("jUser.[MCIMAPServer]")]
        public String UserMcimapServer
        {
            get { return Fields.UserMcimapServer[this]; }
            set { Fields.UserMcimapServer[this] = value; }
        }

        [DisplayName("User Mcimap Port"), Expression("jUser.[MCIMAPPort]")]
        public Int32? UserMcimapPort
        {
            get { return Fields.UserMcimapPort[this]; }
            set { Fields.UserMcimapPort[this] = value; }
        }

        [DisplayName("User Mc Username"), Expression("jUser.[MCUsername]")]
        public String UserMcUsername
        {
            get { return Fields.UserMcUsername[this]; }
            set { Fields.UserMcUsername[this] = value; }
        }

        [DisplayName("User Mc Password"), Expression("jUser.[MCPassword]")]
        public String UserMcPassword
        {
            get { return Fields.UserMcPassword[this]; }
            set { Fields.UserMcPassword[this] = value; }
        }

        [DisplayName("User Start Time"), Expression("jUser.[StartTime]")]
        public String UserStartTime
        {
            get { return Fields.UserStartTime[this]; }
            set { Fields.UserStartTime[this] = value; }
        }

        [DisplayName("User End Time"), Expression("jUser.[EndTime]")]
        public String UserEndTime
        {
            get { return Fields.UserEndTime[this]; }
            set { Fields.UserEndTime[this] = value; }
        }

        [DisplayName("User Uid"), Expression("jUser.[UID]")]
        public String UserUid
        {
            get { return Fields.UserUid[this]; }
            set { Fields.UserUid[this] = value; }
        }

        [DisplayName("User Non Operational"), Expression("jUser.[NonOperational]")]
        public Boolean? UserNonOperational
        {
            get { return Fields.UserNonOperational[this]; }
            set { Fields.UserNonOperational[this] = value; }
        }

        [DisplayName("User Branch Id"), Expression("jUser.[BranchId]")]
        public Int32? UserBranchId
        {
            get { return Fields.UserBranchId[this]; }
            set { Fields.UserBranchId[this] = value; }
        }

        [DisplayName("User Company Id"), Expression("jUser.[CompanyId]")]
        public Int32? UserCompanyId
        {
            get { return Fields.UserCompanyId[this]; }
            set { Fields.UserCompanyId[this] = value; }
        }

        [DisplayName("User Enquiry"), Expression("jUser.[Enquiry]")]
        public Boolean? UserEnquiry
        {
            get { return Fields.UserEnquiry[this]; }
            set { Fields.UserEnquiry[this] = value; }
        }

        [DisplayName("User Quotation"), Expression("jUser.[Quotation]")]
        public Boolean? UserQuotation
        {
            get { return Fields.UserQuotation[this]; }
            set { Fields.UserQuotation[this] = value; }
        }

        [DisplayName("User Tasks"), Expression("jUser.[Tasks]")]
        public Boolean? UserTasks
        {
            get { return Fields.UserTasks[this]; }
            set { Fields.UserTasks[this] = value; }
        }

        [DisplayName("User Contacts"), Expression("jUser.[Contacts]")]
        public Boolean? UserContacts
        {
            get { return Fields.UserContacts[this]; }
            set { Fields.UserContacts[this] = value; }
        }

        [DisplayName("User Purchase"), Expression("jUser.[Purchase]")]
        public Boolean? UserPurchase
        {
            get { return Fields.UserPurchase[this]; }
            set { Fields.UserPurchase[this] = value; }
        }

        [DisplayName("User Sales"), Expression("jUser.[Sales]")]
        public Boolean? UserSales
        {
            get { return Fields.UserSales[this]; }
            set { Fields.UserSales[this] = value; }
        }

        [DisplayName("User Cms"), Expression("jUser.[CMS]")]
        public Boolean? UserCms
        {
            get { return Fields.UserCms[this]; }
            set { Fields.UserCms[this] = value; }
        }

        [DisplayName("User Location"), Expression("jUser.[Location]")]
        public String UserLocation
        {
            get { return Fields.UserLocation[this]; }
            set { Fields.UserLocation[this] = value; }
        }

        [DisplayName("User Coordinates"), Expression("jUser.[Coordinates]")]
        public String UserCoordinates
        {
            get { return Fields.UserCoordinates[this]; }
            set { Fields.UserCoordinates[this] = value; }
        }

        [DisplayName("User Teams Id"), Expression("jUser.[TeamsId]")]
        public Int32? UserTeamsId
        {
            get { return Fields.UserTeamsId[this]; }
            set { Fields.UserTeamsId[this] = value; }
        }

     
        public TeamsRow()
            : base(Fields)
        {
        }
        
        public TeamsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Team;
            public Int32Field UserId;

            public StringField UserUsername;
            public StringField UserDisplayName;
            public StringField UserEmail;
            public StringField UserSource;
            public StringField UserPasswordHash;
            public StringField UserPasswordSalt;
            public DateTimeField UserLastDirectoryUpdate;
            public StringField UserUserImage;
            public DateTimeField UserInsertDate;
            public Int32Field UserInsertUserId;
            public DateTimeField UserUpdateDate;
            public Int32Field UserUpdateUserId;
            public Int16Field UserIsActive;
            public Int32Field UserUpperLevel;
            public Int32Field UserUpperLevel2;
            public Int32Field UserUpperLevel3;
            public Int32Field UserUpperLevel4;
            public Int32Field UserUpperLevel5;
            public StringField UserHost;
            public Int32Field UserPort;
            public BooleanField UserSsl;
            public StringField UserEmailId;
            public StringField UserEmailPassword;
            public StringField UserPhone;
            public StringField UserMcsmtpServer;
            public Int32Field UserMcsmtpPort;
            public StringField UserMcimapServer;
            public Int32Field UserMcimapPort;
            public StringField UserMcUsername;
            public StringField UserMcPassword;
            public StringField UserStartTime;
            public StringField UserEndTime;
            public StringField UserUid;
            public BooleanField UserNonOperational;
            public Int32Field UserBranchId;
            public Int32Field UserCompanyId;
            public BooleanField UserEnquiry;
            public BooleanField UserQuotation;
            public BooleanField UserTasks;
            public BooleanField UserContacts;
            public BooleanField UserPurchase;
            public BooleanField UserSales;
            public BooleanField UserCms;
            public StringField UserLocation;
            public StringField UserCoordinates;
            public Int32Field UserTeamsId;
        }
    }
}
