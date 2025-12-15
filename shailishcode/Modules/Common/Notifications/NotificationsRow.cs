
namespace AdvanceCRM.Common
{
    using AdvanceCRM.Administration;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Common"), TableName("[dbo].[Notifications]")]
    [DisplayName("Notifications"), InstanceName("Notifications")]
    [ReadPermission("")]
    [ModifyPermission("")]
    public sealed class NotificationsRow : Row<NotificationsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Module"), NotNull]
        public Masters.NotificationModules? Module
        {
            get { return (Masters.NotificationModules?)Fields.Module[this]; }
            set { Fields.Module[this] = (Int32?)value; }
        }

        [DisplayName("Text"), NotNull, QuickSearch,NameProperty]
        public String Text
        {
            get { return Fields.Text[this]; }
            set { Fields.Text[this] = value; }
        }

        [DisplayName("Url"), NotNull]
        public String Url
        {
            get { return Fields.Url[this]; }
            set { Fields.Url[this] = value; }
        }

        [DisplayName("Insert User"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jInsertUser"), TextualField("InsertUserUsername")]
        public Int32? InsertUserId
        {
            get { return Fields.InsertUserId[this]; }
            set { Fields.InsertUserId[this] = value; }
        }

        [DisplayName("Insert Date"), NotNull]
        public DateTime? InsertDate
        {
            get { return Fields.InsertDate[this]; }
            set { Fields.InsertDate[this] = value; }
        }

        [DisplayName("Insert User Username"), Expression("jInsertUser.[Username]")]
        public String InsertUserUsername
        {
            get { return Fields.InsertUserUsername[this]; }
            set { Fields.InsertUserUsername[this] = value; }
        }

        [DisplayName("Insert User Display Name"), Expression("jInsertUser.[DisplayName]")]
        public String InsertUserDisplayName
        {
            get { return Fields.InsertUserDisplayName[this]; }
            set { Fields.InsertUserDisplayName[this] = value; }
        }

        [DisplayName("Insert User Email"), Expression("jInsertUser.[Email]")]
        public String InsertUserEmail
        {
            get { return Fields.InsertUserEmail[this]; }
            set { Fields.InsertUserEmail[this] = value; }
        }

        [DisplayName("Insert User Source"), Expression("jInsertUser.[Source]")]
        public String InsertUserSource
        {
            get { return Fields.InsertUserSource[this]; }
            set { Fields.InsertUserSource[this] = value; }
        }

        [DisplayName("Insert User Password Hash"), Expression("jInsertUser.[PasswordHash]")]
        public String InsertUserPasswordHash
        {
            get { return Fields.InsertUserPasswordHash[this]; }
            set { Fields.InsertUserPasswordHash[this] = value; }
        }

        [DisplayName("Insert User Password Salt"), Expression("jInsertUser.[PasswordSalt]")]
        public String InsertUserPasswordSalt
        {
            get { return Fields.InsertUserPasswordSalt[this]; }
            set { Fields.InsertUserPasswordSalt[this] = value; }
        }

        [DisplayName("Insert User Last Directory Update"), Expression("jInsertUser.[LastDirectoryUpdate]")]
        public DateTime? InsertUserLastDirectoryUpdate
        {
            get { return Fields.InsertUserLastDirectoryUpdate[this]; }
            set { Fields.InsertUserLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Insert User User Image"), Expression("jInsertUser.[UserImage]")]
        public String InsertUserUserImage
        {
            get { return Fields.InsertUserUserImage[this]; }
            set { Fields.InsertUserUserImage[this] = value; }
        }

        [DisplayName("Insert User Insert Date"), Expression("jInsertUser.[InsertDate]")]
        public DateTime? InsertUserInsertDate
        {
            get { return Fields.InsertUserInsertDate[this]; }
            set { Fields.InsertUserInsertDate[this] = value; }
        }

        [DisplayName("Insert User Insert User Id"), Expression("jInsertUser.[InsertUserId]")]
        public Int32? InsertUserInsertUserId
        {
            get { return Fields.InsertUserInsertUserId[this]; }
            set { Fields.InsertUserInsertUserId[this] = value; }
        }

        [DisplayName("Insert User Update Date"), Expression("jInsertUser.[UpdateDate]")]
        public DateTime? InsertUserUpdateDate
        {
            get { return Fields.InsertUserUpdateDate[this]; }
            set { Fields.InsertUserUpdateDate[this] = value; }
        }

        [DisplayName("Insert User Update User Id"), Expression("jInsertUser.[UpdateUserId]")]
        public Int32? InsertUserUpdateUserId
        {
            get { return Fields.InsertUserUpdateUserId[this]; }
            set { Fields.InsertUserUpdateUserId[this] = value; }
        }

        [DisplayName("Insert User Is Active"), Expression("jInsertUser.[IsActive]")]
        public Int16? InsertUserIsActive
        {
            get { return Fields.InsertUserIsActive[this]; }
            set { Fields.InsertUserIsActive[this] = value; }
        }

        [DisplayName("Insert User Upper Level"), Expression("jInsertUser.[UpperLevel]")]
        public Int32? InsertUserUpperLevel
        {
            get { return Fields.InsertUserUpperLevel[this]; }
            set { Fields.InsertUserUpperLevel[this] = value; }
        }

        [DisplayName("Insert User Upper Level2"), Expression("jInsertUser.[UpperLevel2]")]
        public Int32? InsertUserUpperLevel2
        {
            get { return Fields.InsertUserUpperLevel2[this]; }
            set { Fields.InsertUserUpperLevel2[this] = value; }
        }

        [DisplayName("Insert User Upper Level3"), Expression("jInsertUser.[UpperLevel3]")]
        public Int32? InsertUserUpperLevel3
        {
            get { return Fields.InsertUserUpperLevel3[this]; }
            set { Fields.InsertUserUpperLevel3[this] = value; }
        }

        [DisplayName("Insert User Upper Level4"), Expression("jInsertUser.[UpperLevel4]")]
        public Int32? InsertUserUpperLevel4
        {
            get { return Fields.InsertUserUpperLevel4[this]; }
            set { Fields.InsertUserUpperLevel4[this] = value; }
        }

        [DisplayName("Insert User Upper Level5"), Expression("jInsertUser.[UpperLevel5]")]
        public Int32? InsertUserUpperLevel5
        {
            get { return Fields.InsertUserUpperLevel5[this]; }
            set { Fields.InsertUserUpperLevel5[this] = value; }
        }

        [DisplayName("Insert User Host"), Expression("jInsertUser.[Host]")]
        public String InsertUserHost
        {
            get { return Fields.InsertUserHost[this]; }
            set { Fields.InsertUserHost[this] = value; }
        }

        [DisplayName("Insert User Port"), Expression("jInsertUser.[Port]")]
        public Int32? InsertUserPort
        {
            get { return Fields.InsertUserPort[this]; }
            set { Fields.InsertUserPort[this] = value; }
        }

        [DisplayName("Insert User SSL"), Expression("jInsertUser.[SSL]")]
        public Boolean? InsertUserSsl
        {
            get { return Fields.InsertUserSsl[this]; }
            set { Fields.InsertUserSsl[this] = value; }
        }

        [DisplayName("Insert User Email Id"), Expression("jInsertUser.[EmailId]")]
        public String InsertUserEmailId
        {
            get { return Fields.InsertUserEmailId[this]; }
            set { Fields.InsertUserEmailId[this] = value; }
        }

        [DisplayName("Insert User Email Password"), Expression("jInsertUser.[EmailPassword]")]
        public String InsertUserEmailPassword
        {
            get { return Fields.InsertUserEmailPassword[this]; }
            set { Fields.InsertUserEmailPassword[this] = value; }
        }

        [DisplayName("Insert User Phone"), Expression("jInsertUser.[Phone]")]
        public String InsertUserPhone
        {
            get { return Fields.InsertUserPhone[this]; }
            set { Fields.InsertUserPhone[this] = value; }
        }

        [DisplayName("Insert User Mcsmtp Server"), Expression("jInsertUser.[MCSMTPServer]")]
        public String InsertUserMcsmtpServer
        {
            get { return Fields.InsertUserMcsmtpServer[this]; }
            set { Fields.InsertUserMcsmtpServer[this] = value; }
        }

        [DisplayName("Insert User Mcsmtp Port"), Expression("jInsertUser.[MCSMTPPort]")]
        public Int32? InsertUserMcsmtpPort
        {
            get { return Fields.InsertUserMcsmtpPort[this]; }
            set { Fields.InsertUserMcsmtpPort[this] = value; }
        }

        [DisplayName("Insert User Mcimap Server"), Expression("jInsertUser.[MCIMAPServer]")]
        public String InsertUserMcimapServer
        {
            get { return Fields.InsertUserMcimapServer[this]; }
            set { Fields.InsertUserMcimapServer[this] = value; }
        }

        [DisplayName("Insert User Mcimap Port"), Expression("jInsertUser.[MCIMAPPort]")]
        public Int32? InsertUserMcimapPort
        {
            get { return Fields.InsertUserMcimapPort[this]; }
            set { Fields.InsertUserMcimapPort[this] = value; }
        }

        [DisplayName("Insert User Mc Username"), Expression("jInsertUser.[MCUsername]")]
        public String InsertUserMcUsername
        {
            get { return Fields.InsertUserMcUsername[this]; }
            set { Fields.InsertUserMcUsername[this] = value; }
        }

        [DisplayName("Insert User Mc Password"), Expression("jInsertUser.[MCPassword]")]
        public String InsertUserMcPassword
        {
            get { return Fields.InsertUserMcPassword[this]; }
            set { Fields.InsertUserMcPassword[this] = value; }
        }

        [DisplayName("Insert User Start Time"), Expression("jInsertUser.[StartTime]")]
        public String InsertUserStartTime
        {
            get { return Fields.InsertUserStartTime[this]; }
            set { Fields.InsertUserStartTime[this] = value; }
        }

        [DisplayName("Insert User End Time"), Expression("jInsertUser.[EndTime]")]
        public String InsertUserEndTime
        {
            get { return Fields.InsertUserEndTime[this]; }
            set { Fields.InsertUserEndTime[this] = value; }
        }

        [DisplayName("Insert User Uid"), Expression("jInsertUser.[UID]")]
        public String InsertUserUid
        {
            get { return Fields.InsertUserUid[this]; }
            set { Fields.InsertUserUid[this] = value; }
        }

        [DisplayName("Insert User Non Operational"), Expression("jInsertUser.[NonOperational]")]
        public Boolean? InsertUserNonOperational
        {
            get { return Fields.InsertUserNonOperational[this]; }
            set { Fields.InsertUserNonOperational[this] = value; }
        }

        [DisplayName("Insert User Branch Id"), Expression("jInsertUser.[BranchId]")]
        public Int32? InsertUserBranchId
        {
            get { return Fields.InsertUserBranchId[this]; }
            set { Fields.InsertUserBranchId[this] = value; }
        }

        [DisplayName("Users")]
        [LookupEditor(typeof(UserRow), Multiple = true), NotMapped]
        [LinkingSetRelation(typeof(NotificationUsersRow), "NotificationsId", "UserId")]
        public List<Int32> UserList
        {
            get { return Fields.UserList[this]; }
            set { Fields.UserList[this] = value; }
        }

      

        public NotificationsRow()
            : base(Fields)
        {
        }
        
        public NotificationsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field Module;
            public StringField Text;
            public StringField Url;
            public Int32Field InsertUserId;
            public DateTimeField InsertDate;

            public StringField InsertUserUsername;
            public StringField InsertUserDisplayName;
            public StringField InsertUserEmail;
            public StringField InsertUserSource;
            public StringField InsertUserPasswordHash;
            public StringField InsertUserPasswordSalt;
            public DateTimeField InsertUserLastDirectoryUpdate;
            public StringField InsertUserUserImage;
            public DateTimeField InsertUserInsertDate;
            public Int32Field InsertUserInsertUserId;
            public DateTimeField InsertUserUpdateDate;
            public Int32Field InsertUserUpdateUserId;
            public Int16Field InsertUserIsActive;
            public Int32Field InsertUserUpperLevel;
            public Int32Field InsertUserUpperLevel2;
            public Int32Field InsertUserUpperLevel3;
            public Int32Field InsertUserUpperLevel4;
            public Int32Field InsertUserUpperLevel5;
            public StringField InsertUserHost;
            public Int32Field InsertUserPort;
            public BooleanField InsertUserSsl;
            public StringField InsertUserEmailId;
            public StringField InsertUserEmailPassword;
            public StringField InsertUserPhone;
            public StringField InsertUserMcsmtpServer;
            public Int32Field InsertUserMcsmtpPort;
            public StringField InsertUserMcimapServer;
            public Int32Field InsertUserMcimapPort;
            public StringField InsertUserMcUsername;
            public StringField InsertUserMcPassword;
            public StringField InsertUserStartTime;
            public StringField InsertUserEndTime;
            public StringField InsertUserUid;
            public BooleanField InsertUserNonOperational;
            public Int32Field InsertUserBranchId;

            public ListField<Int32> UserList;
        }
    }
}
