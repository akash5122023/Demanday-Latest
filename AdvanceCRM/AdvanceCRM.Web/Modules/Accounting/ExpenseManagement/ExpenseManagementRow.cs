
namespace AdvanceCRM.Accounting
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Masters;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Accounting"), TableName("[dbo].[ExpenseManagement]")]
    [DisplayName("Expense Management"), InstanceName("Expense Management")]
    [ReadPermission("ExpenseManagement:Read")]
    [InsertPermission("ExpenseManagement:Insert")]
    [UpdatePermission("ExpenseManagement:Update")]
    [DeletePermission("ExpenseManagement:Delete")]

    public sealed class ExpenseManagementRow : Row<ExpenseManagementRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Representative"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jRepresentative"), TextualField("RepresentativeUsername")]
        [UserEditor]
        public Int32? RepresentativeId
        {
            get { return Fields.RepresentativeId[this]; }
            set { Fields.RepresentativeId[this] = value; }
        }

        [DisplayName("Head"), NotNull, ForeignKey("[dbo].[AccountingHeads]", "Id"), LeftJoin("jHead"), TextualField("Head")]
        [LookupEditor(typeof(AccountingHeadsRow), FilterField = "Type", FilterValue = "2")]
        public Int32? HeadId
        {
            get { return Fields.HeadId[this]; }
            set { Fields.HeadId[this] = value; }
        }

        [DisplayName("Amount"), NotNull]
        public Double? Amount
        {
            get { return Fields.Amount[this]; }
            set { Fields.Amount[this] = value; }
        }

        [DisplayName("Attachment"), Size(1000), QuickSearch,NameProperty]
        [MultipleImageUploadEditor(FilenameFormat = "Expenses/~", CopyToHistory = true, AllowNonImage = true)]
        public String Attachment
        {
            get { return Fields.Attachment[this]; }
            set { Fields.Attachment[this] = value; }
        }

        [DisplayName("Additional Info"), Size(1000), TextAreaEditor(Rows = 4)]
        public String AdditionalInfo
        {
            get { return Fields.AdditionalInfo[this]; }
            set { Fields.AdditionalInfo[this] = value; }
        }

        [DisplayName("Approved By"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jApprovedBy"), TextualField("ApprovedByUsername")]
        [UserEditor, QuickFilter]
        public Int32? ApprovedBy
        {
            get { return Fields.ApprovedBy[this]; }
            set { Fields.ApprovedBy[this] = value; }
        }

        [DisplayName("Date"), NotNull, DefaultValue("now")]
        public DateTime? Date
        {
            get { return Fields.Date[this]; }
            set { Fields.Date[this] = value; }
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

        [DisplayName("Head"), Expression("jHead.[Head]")]
        public String Head
        {
            get { return Fields.Head[this]; }
            set { Fields.Head[this] = value; }
        }

        [DisplayName("Head Type"), Expression("jHead.[Type]")]
        public Int32? HeadType
        {
            get { return Fields.HeadType[this]; }
            set { Fields.HeadType[this] = value; }
        }

        [DisplayName("Approved By"), Expression("jApprovedBy.[Username]")]
        public String ApprovedByUsername
        {
            get { return Fields.ApprovedByUsername[this]; }
            set { Fields.ApprovedByUsername[this] = value; }
        }

        [DisplayName("Approved By Display Name"), Expression("jApprovedBy.[DisplayName]")]
        public String ApprovedByDisplayName
        {
            get { return Fields.ApprovedByDisplayName[this]; }
            set { Fields.ApprovedByDisplayName[this] = value; }
        }

        [DisplayName("Approved By Email"), Expression("jApprovedBy.[Email]")]
        public String ApprovedByEmail
        {
            get { return Fields.ApprovedByEmail[this]; }
            set { Fields.ApprovedByEmail[this] = value; }
        }

        [DisplayName("Approved By Source"), Expression("jApprovedBy.[Source]")]
        public String ApprovedBySource
        {
            get { return Fields.ApprovedBySource[this]; }
            set { Fields.ApprovedBySource[this] = value; }
        }

        [DisplayName("Approved By Password Hash"), Expression("jApprovedBy.[PasswordHash]")]
        public String ApprovedByPasswordHash
        {
            get { return Fields.ApprovedByPasswordHash[this]; }
            set { Fields.ApprovedByPasswordHash[this] = value; }
        }

        [DisplayName("Approved By Password Salt"), Expression("jApprovedBy.[PasswordSalt]")]
        public String ApprovedByPasswordSalt
        {
            get { return Fields.ApprovedByPasswordSalt[this]; }
            set { Fields.ApprovedByPasswordSalt[this] = value; }
        }

        [DisplayName("Approved By Last Directory Update"), Expression("jApprovedBy.[LastDirectoryUpdate]")]
        public DateTime? ApprovedByLastDirectoryUpdate
        {
            get { return Fields.ApprovedByLastDirectoryUpdate[this]; }
            set { Fields.ApprovedByLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Approved By User Image"), Expression("jApprovedBy.[UserImage]")]
        public String ApprovedByUserImage
        {
            get { return Fields.ApprovedByUserImage[this]; }
            set { Fields.ApprovedByUserImage[this] = value; }
        }

        [DisplayName("Approved By Insert Date"), Expression("jApprovedBy.[InsertDate]")]
        public DateTime? ApprovedByInsertDate
        {
            get { return Fields.ApprovedByInsertDate[this]; }
            set { Fields.ApprovedByInsertDate[this] = value; }
        }

        [DisplayName("Approved By Insert User Id"), Expression("jApprovedBy.[InsertUserId]")]
        public Int32? ApprovedByInsertUserId
        {
            get { return Fields.ApprovedByInsertUserId[this]; }
            set { Fields.ApprovedByInsertUserId[this] = value; }
        }

        [DisplayName("Approved By Update Date"), Expression("jApprovedBy.[UpdateDate]")]
        public DateTime? ApprovedByUpdateDate
        {
            get { return Fields.ApprovedByUpdateDate[this]; }
            set { Fields.ApprovedByUpdateDate[this] = value; }
        }

        [DisplayName("Approved By Update User Id"), Expression("jApprovedBy.[UpdateUserId]")]
        public Int32? ApprovedByUpdateUserId
        {
            get { return Fields.ApprovedByUpdateUserId[this]; }
            set { Fields.ApprovedByUpdateUserId[this] = value; }
        }

        [DisplayName("Approved By Is Active"), Expression("jApprovedBy.[IsActive]")]
        public Int16? ApprovedByIsActive
        {
            get { return Fields.ApprovedByIsActive[this]; }
            set { Fields.ApprovedByIsActive[this] = value; }
        }

        [DisplayName("Approved By Upper Level"), Expression("jApprovedBy.[UpperLevel]")]
        public Int32? ApprovedByUpperLevel
        {
            get { return Fields.ApprovedByUpperLevel[this]; }
            set { Fields.ApprovedByUpperLevel[this] = value; }
        }

        [DisplayName("Approved By Upper Level2"), Expression("jApprovedBy.[UpperLevel2]")]
        public Int32? ApprovedByUpperLevel2
        {
            get { return Fields.ApprovedByUpperLevel2[this]; }
            set { Fields.ApprovedByUpperLevel2[this] = value; }
        }

        [DisplayName("Approved By Upper Level3"), Expression("jApprovedBy.[UpperLevel3]")]
        public Int32? ApprovedByUpperLevel3
        {
            get { return Fields.ApprovedByUpperLevel3[this]; }
            set { Fields.ApprovedByUpperLevel3[this] = value; }
        }

        [DisplayName("Approved By Upper Level4"), Expression("jApprovedBy.[UpperLevel4]")]
        public Int32? ApprovedByUpperLevel4
        {
            get { return Fields.ApprovedByUpperLevel4[this]; }
            set { Fields.ApprovedByUpperLevel4[this] = value; }
        }

        [DisplayName("Approved By Upper Level5"), Expression("jApprovedBy.[UpperLevel5]")]
        public Int32? ApprovedByUpperLevel5
        {
            get { return Fields.ApprovedByUpperLevel5[this]; }
            set { Fields.ApprovedByUpperLevel5[this] = value; }
        }

        [DisplayName("Approved By Host"), Expression("jApprovedBy.[Host]")]
        public String ApprovedByHost
        {
            get { return Fields.ApprovedByHost[this]; }
            set { Fields.ApprovedByHost[this] = value; }
        }

        [DisplayName("Approved By Port"), Expression("jApprovedBy.[Port]")]
        public Int32? ApprovedByPort
        {
            get { return Fields.ApprovedByPort[this]; }
            set { Fields.ApprovedByPort[this] = value; }
        }

        [DisplayName("Approved By SSL"), Expression("jApprovedBy.[SSL]")]
        public Boolean? ApprovedBySsl
        {
            get { return Fields.ApprovedBySsl[this]; }
            set { Fields.ApprovedBySsl[this] = value; }
        }

        [DisplayName("Approved By Email Id"), Expression("jApprovedBy.[EmailId]")]
        public String ApprovedByEmailId
        {
            get { return Fields.ApprovedByEmailId[this]; }
            set { Fields.ApprovedByEmailId[this] = value; }
        }

        [DisplayName("Approved By Email Password"), Expression("jApprovedBy.[EmailPassword]")]
        public String ApprovedByEmailPassword
        {
            get { return Fields.ApprovedByEmailPassword[this]; }
            set { Fields.ApprovedByEmailPassword[this] = value; }
        }

        [DisplayName("Approved By Phone"), Expression("jApprovedBy.[Phone]")]
        public String ApprovedByPhone
        {
            get { return Fields.ApprovedByPhone[this]; }
            set { Fields.ApprovedByPhone[this] = value; }
        }

        [DisplayName("Approved By Mcsmtp Server"), Expression("jApprovedBy.[MCSMTPServer]")]
        public String ApprovedByMcsmtpServer
        {
            get { return Fields.ApprovedByMcsmtpServer[this]; }
            set { Fields.ApprovedByMcsmtpServer[this] = value; }
        }

        [DisplayName("Approved By Mcsmtp Port"), Expression("jApprovedBy.[MCSMTPPort]")]
        public Int32? ApprovedByMcsmtpPort
        {
            get { return Fields.ApprovedByMcsmtpPort[this]; }
            set { Fields.ApprovedByMcsmtpPort[this] = value; }
        }

        [DisplayName("Approved By Mcimap Server"), Expression("jApprovedBy.[MCIMAPServer]")]
        public String ApprovedByMcimapServer
        {
            get { return Fields.ApprovedByMcimapServer[this]; }
            set { Fields.ApprovedByMcimapServer[this] = value; }
        }

        [DisplayName("Approved By Mcimap Port"), Expression("jApprovedBy.[MCIMAPPort]")]
        public Int32? ApprovedByMcimapPort
        {
            get { return Fields.ApprovedByMcimapPort[this]; }
            set { Fields.ApprovedByMcimapPort[this] = value; }
        }

        [DisplayName("Approved By Mc Username"), Expression("jApprovedBy.[MCUsername]")]
        public String ApprovedByMcUsername
        {
            get { return Fields.ApprovedByMcUsername[this]; }
            set { Fields.ApprovedByMcUsername[this] = value; }
        }

        [DisplayName("Approved By Mc Password"), Expression("jApprovedBy.[MCPassword]")]
        public String ApprovedByMcPassword
        {
            get { return Fields.ApprovedByMcPassword[this]; }
            set { Fields.ApprovedByMcPassword[this] = value; }
        }

        [DisplayName("Approved By Start Time"), Expression("jApprovedBy.[StartTime]")]
        public String ApprovedByStartTime
        {
            get { return Fields.ApprovedByStartTime[this]; }
            set { Fields.ApprovedByStartTime[this] = value; }
        }

        [DisplayName("Approved By End Time"), Expression("jApprovedBy.[EndTime]")]
        public String ApprovedByEndTime
        {
            get { return Fields.ApprovedByEndTime[this]; }
            set { Fields.ApprovedByEndTime[this] = value; }
        }

        [DisplayName("Approved By Branch Id"), Expression("jApprovedBy.[BranchId]")]
        public Int32? ApprovedByBranchId
        {
            get { return Fields.ApprovedByBranchId[this]; }
            set { Fields.ApprovedByBranchId[this] = value; }
        }

        [DisplayName("Approved By Uid"), Expression("jApprovedBy.[UID]")]
        public String ApprovedByUid
        {
            get { return Fields.ApprovedByUid[this]; }
            set { Fields.ApprovedByUid[this] = value; }
        }

        [DisplayName("Approved By Non Operational"), Expression("jApprovedBy.[NonOperational]")]
        public Boolean? ApprovedByNonOperational
        {
            get { return Fields.ApprovedByNonOperational[this]; }
            set { Fields.ApprovedByNonOperational[this] = value; }
        }

      

       

        public ExpenseManagementRow()
            : base(Fields)
        {
        }
        public ExpenseManagementRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field RepresentativeId;
            public Int32Field HeadId;
            public DoubleField Amount;
            public StringField Attachment;
            public StringField AdditionalInfo;
            public Int32Field ApprovedBy;
            public DateTimeField Date;

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

            public StringField Head;
            public Int32Field HeadType;

            public StringField ApprovedByUsername;
            public StringField ApprovedByDisplayName;
            public StringField ApprovedByEmail;
            public StringField ApprovedBySource;
            public StringField ApprovedByPasswordHash;
            public StringField ApprovedByPasswordSalt;
            public DateTimeField ApprovedByLastDirectoryUpdate;
            public StringField ApprovedByUserImage;
            public DateTimeField ApprovedByInsertDate;
            public Int32Field ApprovedByInsertUserId;
            public DateTimeField ApprovedByUpdateDate;
            public Int32Field ApprovedByUpdateUserId;
            public Int16Field ApprovedByIsActive;
            public Int32Field ApprovedByUpperLevel;
            public Int32Field ApprovedByUpperLevel2;
            public Int32Field ApprovedByUpperLevel3;
            public Int32Field ApprovedByUpperLevel4;
            public Int32Field ApprovedByUpperLevel5;
            public StringField ApprovedByHost;
            public Int32Field ApprovedByPort;
            public BooleanField ApprovedBySsl;
            public StringField ApprovedByEmailId;
            public StringField ApprovedByEmailPassword;
            public StringField ApprovedByPhone;
            public StringField ApprovedByMcsmtpServer;
            public Int32Field ApprovedByMcsmtpPort;
            public StringField ApprovedByMcimapServer;
            public Int32Field ApprovedByMcimapPort;
            public StringField ApprovedByMcUsername;
            public StringField ApprovedByMcPassword;
            public StringField ApprovedByStartTime;
            public StringField ApprovedByEndTime;
            public Int32Field ApprovedByBranchId;
            public StringField ApprovedByUid;
            public BooleanField ApprovedByNonOperational;
        }
    }
}
