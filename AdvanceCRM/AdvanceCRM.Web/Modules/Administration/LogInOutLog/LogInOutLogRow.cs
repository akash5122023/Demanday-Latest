
namespace AdvanceCRM.Administration
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Administration"), TableName("[dbo].[LogInOutLog]")]
    [DisplayName("Log In Out Log"), InstanceName("Log In Out Log")]
    [ReadPermission(PermissionKeys.Logs)]

    public sealed class LogInOutLogRow : Row<LogInOutLogRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Date"), NotNull, SortOrder(1, true)]
        public DateTime? Date
        {
            get { return Fields.Date[this]; }
            set { Fields.Date[this] = value; }
        }

        [DisplayName("Type"), NotNull]
        public Masters.AttendanceTypeMaster? Type
        {
            get { return (Masters.AttendanceTypeMaster?)Fields.Type[this]; }
            set { Fields.Type[this] = (Int32?)value; }
        }

        [DisplayName("User"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jUser"), TextualField("UserUsername")]
        public Int32? UserId
        {
            get { return Fields.UserId[this]; }
            set { Fields.UserId[this] = value; }
        }

        [DisplayName("User Username"), Expression("jUser.[Username]")]
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

        [DisplayName("User SSL"), Expression("jUser.[SSL]")]
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

        [DisplayName("User Branch Id"), Expression("jUser.[BranchId]")]
        public Int32? UserBranchId
        {
            get { return Fields.UserBranchId[this]; }
            set { Fields.UserBranchId[this] = value; }
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

      

        public LogInOutLogRow()
            : base(Fields)
        {
        }
        public LogInOutLogRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public DateTimeField Date;
            public Int32Field Type;
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
            public Int32Field UserBranchId;
            public StringField UserUid;
            public BooleanField UserNonOperational;
        }
    }
}
