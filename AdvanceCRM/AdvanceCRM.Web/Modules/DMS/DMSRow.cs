
namespace AdvanceCRM.DMS
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("DMS"), TableName("[dbo].[DMS]")]
    [DisplayName("Dms"), InstanceName("Dms")]
    [ReadPermission("DMS:Read")]
    [ModifyPermission("DMS:Modify")]
    [LookupScript("DMS.DMS", Permission = "?")]

    public sealed class DMSRow : Row<DMSRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Parent")]
        [LookupEditor(typeof(DMSRow))]
        public Int32? ParentId
        {
            get { return Fields.ParentId[this]; }
            set { Fields.ParentId[this] = value; }
        }

        [DisplayName("Folder Name"), Size(100), NotNull, QuickSearch,NameProperty]
        public String Title
        {
            get { return Fields.Title[this]; }
            set { Fields.Title[this] = value; }
        }

        [DisplayName("Files"), Size(1000)]
        [MultipleFileUploadEditor]
        public String Files
        {
            get { return Fields.Files[this]; }
            set { Fields.Files[this] = value; }
        }

        [DisplayName("Created By"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jOwner"), TextualField("OwnerUsername"), ReadOnly(true)]
        [Administration.UserEditor]
        public Int32? OwnerId
        {
            get { return Fields.OwnerId[this]; }
            set { Fields.OwnerId[this] = value; }
        }
        [DisplayName("Assigned"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jAssigned"), TextualField("AssignedUsername")]
        [Administration.UserEditor]
        public Int32? AssignedId
        {
            get { return Fields.AssignedId[this]; }
            set { Fields.AssignedId[this] = value; }
        }

        [DisplayName("Updated By"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jLastUpdated"), TextualField("LastUpdatedUsername")]
        [Administration.UserEditor]
        public Int32? LastUpdatedId
        {
            get { return Fields.LastUpdatedId[this]; }
            set { Fields.LastUpdatedId[this] = value; }
        }

        [DisplayName("Assigned Username"), Expression("jAssigned.[Username]")]
        public String AssignedUsername
        {
            get { return Fields.AssignedUsername[this]; }
            set { Fields.AssignedUsername[this] = value; }
        }

        [DisplayName("Assigned Display Name"), Expression("jAssigned.[DisplayName]")]
        public String AssignedDisplayName
        {
            get { return Fields.AssignedDisplayName[this]; }
            set { Fields.AssignedDisplayName[this] = value; }
        }

        [DisplayName("Assigned Email"), Expression("jAssigned.[Email]")]
        public String AssignedEmail
        {
            get { return Fields.AssignedEmail[this]; }
            set { Fields.AssignedEmail[this] = value; }
        }

        [DisplayName("Assigned Source"), Expression("jAssigned.[Source]")]
        public String AssignedSource
        {
            get { return Fields.AssignedSource[this]; }
            set { Fields.AssignedSource[this] = value; }
        }

        [DisplayName("Assigned Password Hash"), Expression("jAssigned.[PasswordHash]")]
        public String AssignedPasswordHash
        {
            get { return Fields.AssignedPasswordHash[this]; }
            set { Fields.AssignedPasswordHash[this] = value; }
        }

        [DisplayName("Assigned Password Salt"), Expression("jAssigned.[PasswordSalt]")]
        public String AssignedPasswordSalt
        {
            get { return Fields.AssignedPasswordSalt[this]; }
            set { Fields.AssignedPasswordSalt[this] = value; }
        }

        [DisplayName("Assigned Last Directory Update"), Expression("jAssigned.[LastDirectoryUpdate]")]
        public DateTime? AssignedLastDirectoryUpdate
        {
            get { return Fields.AssignedLastDirectoryUpdate[this]; }
            set { Fields.AssignedLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Assigned User Image"), Expression("jAssigned.[UserImage]")]
        public String AssignedUserImage
        {
            get { return Fields.AssignedUserImage[this]; }
            set { Fields.AssignedUserImage[this] = value; }
        }

        [DisplayName("Assigned Insert Date"), Expression("jAssigned.[InsertDate]")]
        public DateTime? AssignedInsertDate
        {
            get { return Fields.AssignedInsertDate[this]; }
            set { Fields.AssignedInsertDate[this] = value; }
        }

        [DisplayName("Assigned Insert User Id"), Expression("jAssigned.[InsertUserId]")]
        public Int32? AssignedInsertUserId
        {
            get { return Fields.AssignedInsertUserId[this]; }
            set { Fields.AssignedInsertUserId[this] = value; }
        }

        [DisplayName("Assigned Update Date"), Expression("jAssigned.[UpdateDate]")]
        public DateTime? AssignedUpdateDate
        {
            get { return Fields.AssignedUpdateDate[this]; }
            set { Fields.AssignedUpdateDate[this] = value; }
        }

        [DisplayName("Assigned Update User Id"), Expression("jAssigned.[UpdateUserId]")]
        public Int32? AssignedUpdateUserId
        {
            get { return Fields.AssignedUpdateUserId[this]; }
            set { Fields.AssignedUpdateUserId[this] = value; }
        }

        [DisplayName("Assigned Is Active"), Expression("jAssigned.[IsActive]")]
        public Int16? AssignedIsActive
        {
            get { return Fields.AssignedIsActive[this]; }
            set { Fields.AssignedIsActive[this] = value; }
        }

        [DisplayName("Assigned Upper Level"), Expression("jAssigned.[UpperLevel]")]
        public Int32? AssignedUpperLevel
        {
            get { return Fields.AssignedUpperLevel[this]; }
            set { Fields.AssignedUpperLevel[this] = value; }
        }

        [DisplayName("Assigned Upper Level2"), Expression("jAssigned.[UpperLevel2]")]
        public Int32? AssignedUpperLevel2
        {
            get { return Fields.AssignedUpperLevel2[this]; }
            set { Fields.AssignedUpperLevel2[this] = value; }
        }

        [DisplayName("Assigned Upper Level3"), Expression("jAssigned.[UpperLevel3]")]
        public Int32? AssignedUpperLevel3
        {
            get { return Fields.AssignedUpperLevel3[this]; }
            set { Fields.AssignedUpperLevel3[this] = value; }
        }

        [DisplayName("Assigned Upper Level4"), Expression("jAssigned.[UpperLevel4]")]
        public Int32? AssignedUpperLevel4
        {
            get { return Fields.AssignedUpperLevel4[this]; }
            set { Fields.AssignedUpperLevel4[this] = value; }
        }

        [DisplayName("Assigned Upper Level5"), Expression("jAssigned.[UpperLevel5]")]
        public Int32? AssignedUpperLevel5
        {
            get { return Fields.AssignedUpperLevel5[this]; }
            set { Fields.AssignedUpperLevel5[this] = value; }
        }

        [DisplayName("Assigned Host"), Expression("jAssigned.[Host]")]
        public String AssignedHost
        {
            get { return Fields.AssignedHost[this]; }
            set { Fields.AssignedHost[this] = value; }
        }

        [DisplayName("Assigned Port"), Expression("jAssigned.[Port]")]
        public Int32? AssignedPort
        {
            get { return Fields.AssignedPort[this]; }
            set { Fields.AssignedPort[this] = value; }
        }

        [DisplayName("Assigned Ssl"), Expression("jAssigned.[SSL]")]
        public Boolean? AssignedSsl
        {
            get { return Fields.AssignedSsl[this]; }
            set { Fields.AssignedSsl[this] = value; }
        }

        [DisplayName("Assigned Email Id"), Expression("jAssigned.[EmailId]")]
        public String AssignedEmailId
        {
            get { return Fields.AssignedEmailId[this]; }
            set { Fields.AssignedEmailId[this] = value; }
        }

        [DisplayName("Assigned Email Password"), Expression("jAssigned.[EmailPassword]")]
        public String AssignedEmailPassword
        {
            get { return Fields.AssignedEmailPassword[this]; }
            set { Fields.AssignedEmailPassword[this] = value; }
        }

        [DisplayName("Assigned Phone"), Expression("jAssigned.[Phone]")]
        public String AssignedPhone
        {
            get { return Fields.AssignedPhone[this]; }
            set { Fields.AssignedPhone[this] = value; }
        }

        [DisplayName("Assigned Mcsmtp Server"), Expression("jAssigned.[MCSMTPServer]")]
        public String AssignedMcsmtpServer
        {
            get { return Fields.AssignedMcsmtpServer[this]; }
            set { Fields.AssignedMcsmtpServer[this] = value; }
        }

        [DisplayName("Assigned Mcsmtp Port"), Expression("jAssigned.[MCSMTPPort]")]
        public Int32? AssignedMcsmtpPort
        {
            get { return Fields.AssignedMcsmtpPort[this]; }
            set { Fields.AssignedMcsmtpPort[this] = value; }
        }

        [DisplayName("Assigned Mcimap Server"), Expression("jAssigned.[MCIMAPServer]")]
        public String AssignedMcimapServer
        {
            get { return Fields.AssignedMcimapServer[this]; }
            set { Fields.AssignedMcimapServer[this] = value; }
        }

        [DisplayName("Assigned Mcimap Port"), Expression("jAssigned.[MCIMAPPort]")]
        public Int32? AssignedMcimapPort
        {
            get { return Fields.AssignedMcimapPort[this]; }
            set { Fields.AssignedMcimapPort[this] = value; }
        }

        [DisplayName("Assigned Mc Username"), Expression("jAssigned.[MCUsername]")]
        public String AssignedMcUsername
        {
            get { return Fields.AssignedMcUsername[this]; }
            set { Fields.AssignedMcUsername[this] = value; }
        }

        [DisplayName("Assigned Mc Password"), Expression("jAssigned.[MCPassword]")]
        public String AssignedMcPassword
        {
            get { return Fields.AssignedMcPassword[this]; }
            set { Fields.AssignedMcPassword[this] = value; }
        }

        [DisplayName("Assigned Start Time"), Expression("jAssigned.[StartTime]")]
        public String AssignedStartTime
        {
            get { return Fields.AssignedStartTime[this]; }
            set { Fields.AssignedStartTime[this] = value; }
        }

        [DisplayName("Assigned End Time"), Expression("jAssigned.[EndTime]")]
        public String AssignedEndTime
        {
            get { return Fields.AssignedEndTime[this]; }
            set { Fields.AssignedEndTime[this] = value; }
        }

        [DisplayName("Assigned Uid"), Expression("jAssigned.[UID]")]
        public String AssignedUid
        {
            get { return Fields.AssignedUid[this]; }
            set { Fields.AssignedUid[this] = value; }
        }

        [DisplayName("Assigned Non Operational"), Expression("jAssigned.[NonOperational]")]
        public Boolean? AssignedNonOperational
        {
            get { return Fields.AssignedNonOperational[this]; }
            set { Fields.AssignedNonOperational[this] = value; }
        }

        [DisplayName("Assigned Branch Id"), Expression("jAssigned.[BranchId]")]
        public Int32? AssignedBranchId
        {
            get { return Fields.AssignedBranchId[this]; }
            set { Fields.AssignedBranchId[this] = value; }
        }

        [DisplayName("Assigned Company Id"), Expression("jAssigned.[CompanyId]")]
        public Int32? AssignedCompanyId
        {
            get { return Fields.AssignedCompanyId[this]; }
            set { Fields.AssignedCompanyId[this] = value; }
        }

        [DisplayName("Create Date")]
        public DateTime? CreateDate
        {
            get { return Fields.CreateDate[this]; }
            set { Fields.CreateDate[this] = value; }
        }

        [DisplayName("Update Date")]
        public DateTime? UpdateDate
        {
            get { return Fields.UpdateDate[this]; }
            set { Fields.UpdateDate[this] = value; }
        }

        [DisplayName("Created By"), Expression("jOwner.[Username]")]
        public String OwnerUsername
        {
            get { return Fields.OwnerUsername[this]; }
            set { Fields.OwnerUsername[this] = value; }
        }

        [DisplayName("Owner Display Name"), Expression("jOwner.[DisplayName]")]
        public String OwnerDisplayName
        {
            get { return Fields.OwnerDisplayName[this]; }
            set { Fields.OwnerDisplayName[this] = value; }
        }

        [DisplayName("Owner Email"), Expression("jOwner.[Email]")]
        public String OwnerEmail
        {
            get { return Fields.OwnerEmail[this]; }
            set { Fields.OwnerEmail[this] = value; }
        }

        [DisplayName("Owner Source"), Expression("jOwner.[Source]")]
        public String OwnerSource
        {
            get { return Fields.OwnerSource[this]; }
            set { Fields.OwnerSource[this] = value; }
        }

        [DisplayName("Owner Password Hash"), Expression("jOwner.[PasswordHash]")]
        public String OwnerPasswordHash
        {
            get { return Fields.OwnerPasswordHash[this]; }
            set { Fields.OwnerPasswordHash[this] = value; }
        }

        [DisplayName("Owner Password Salt"), Expression("jOwner.[PasswordSalt]")]
        public String OwnerPasswordSalt
        {
            get { return Fields.OwnerPasswordSalt[this]; }
            set { Fields.OwnerPasswordSalt[this] = value; }
        }

        [DisplayName("Owner Last Directory Update"), Expression("jOwner.[LastDirectoryUpdate]")]
        public DateTime? OwnerLastDirectoryUpdate
        {
            get { return Fields.OwnerLastDirectoryUpdate[this]; }
            set { Fields.OwnerLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Owner User Image"), Expression("jOwner.[UserImage]")]
        public String OwnerUserImage
        {
            get { return Fields.OwnerUserImage[this]; }
            set { Fields.OwnerUserImage[this] = value; }
        }

        [DisplayName("Owner Insert Date"), Expression("jOwner.[InsertDate]")]
        public DateTime? OwnerInsertDate
        {
            get { return Fields.OwnerInsertDate[this]; }
            set { Fields.OwnerInsertDate[this] = value; }
        }

        [DisplayName("Owner Insert User Id"), Expression("jOwner.[InsertUserId]")]
        public Int32? OwnerInsertUserId
        {
            get { return Fields.OwnerInsertUserId[this]; }
            set { Fields.OwnerInsertUserId[this] = value; }
        }

        [DisplayName("Owner Update Date"), Expression("jOwner.[UpdateDate]")]
        public DateTime? OwnerUpdateDate
        {
            get { return Fields.OwnerUpdateDate[this]; }
            set { Fields.OwnerUpdateDate[this] = value; }
        }

        [DisplayName("Owner Update User Id"), Expression("jOwner.[UpdateUserId]")]
        public Int32? OwnerUpdateUserId
        {
            get { return Fields.OwnerUpdateUserId[this]; }
            set { Fields.OwnerUpdateUserId[this] = value; }
        }

        [DisplayName("Owner Is Active"), Expression("jOwner.[IsActive]")]
        public Int16? OwnerIsActive
        {
            get { return Fields.OwnerIsActive[this]; }
            set { Fields.OwnerIsActive[this] = value; }
        }

        [DisplayName("Owner Upper Level"), Expression("jOwner.[UpperLevel]")]
        public Int32? OwnerUpperLevel
        {
            get { return Fields.OwnerUpperLevel[this]; }
            set { Fields.OwnerUpperLevel[this] = value; }
        }

        [DisplayName("Owner Upper Level2"), Expression("jOwner.[UpperLevel2]")]
        public Int32? OwnerUpperLevel2
        {
            get { return Fields.OwnerUpperLevel2[this]; }
            set { Fields.OwnerUpperLevel2[this] = value; }
        }

        [DisplayName("Owner Upper Level3"), Expression("jOwner.[UpperLevel3]")]
        public Int32? OwnerUpperLevel3
        {
            get { return Fields.OwnerUpperLevel3[this]; }
            set { Fields.OwnerUpperLevel3[this] = value; }
        }

        [DisplayName("Owner Upper Level4"), Expression("jOwner.[UpperLevel4]")]
        public Int32? OwnerUpperLevel4
        {
            get { return Fields.OwnerUpperLevel4[this]; }
            set { Fields.OwnerUpperLevel4[this] = value; }
        }

        [DisplayName("Owner Upper Level5"), Expression("jOwner.[UpperLevel5]")]
        public Int32? OwnerUpperLevel5
        {
            get { return Fields.OwnerUpperLevel5[this]; }
            set { Fields.OwnerUpperLevel5[this] = value; }
        }

        [DisplayName("Owner Host"), Expression("jOwner.[Host]")]
        public String OwnerHost
        {
            get { return Fields.OwnerHost[this]; }
            set { Fields.OwnerHost[this] = value; }
        }

        [DisplayName("Owner Port"), Expression("jOwner.[Port]")]
        public Int32? OwnerPort
        {
            get { return Fields.OwnerPort[this]; }
            set { Fields.OwnerPort[this] = value; }
        }

        [DisplayName("Owner SSL"), Expression("jOwner.[SSL]")]
        public Boolean? OwnerSsl
        {
            get { return Fields.OwnerSsl[this]; }
            set { Fields.OwnerSsl[this] = value; }
        }

        [DisplayName("Owner Email Id"), Expression("jOwner.[EmailId]")]
        public String OwnerEmailId
        {
            get { return Fields.OwnerEmailId[this]; }
            set { Fields.OwnerEmailId[this] = value; }
        }

        [DisplayName("Owner Email Password"), Expression("jOwner.[EmailPassword]")]
        public String OwnerEmailPassword
        {
            get { return Fields.OwnerEmailPassword[this]; }
            set { Fields.OwnerEmailPassword[this] = value; }
        }

        [DisplayName("Owner Phone"), Expression("jOwner.[Phone]")]
        public String OwnerPhone
        {
            get { return Fields.OwnerPhone[this]; }
            set { Fields.OwnerPhone[this] = value; }
        }

        [DisplayName("Owner Mcsmtp Server"), Expression("jOwner.[MCSMTPServer]")]
        public String OwnerMcsmtpServer
        {
            get { return Fields.OwnerMcsmtpServer[this]; }
            set { Fields.OwnerMcsmtpServer[this] = value; }
        }

        [DisplayName("Owner Mcsmtp Port"), Expression("jOwner.[MCSMTPPort]")]
        public Int32? OwnerMcsmtpPort
        {
            get { return Fields.OwnerMcsmtpPort[this]; }
            set { Fields.OwnerMcsmtpPort[this] = value; }
        }

        [DisplayName("Owner Mcimap Server"), Expression("jOwner.[MCIMAPServer]")]
        public String OwnerMcimapServer
        {
            get { return Fields.OwnerMcimapServer[this]; }
            set { Fields.OwnerMcimapServer[this] = value; }
        }

        [DisplayName("Owner Mcimap Port"), Expression("jOwner.[MCIMAPPort]")]
        public Int32? OwnerMcimapPort
        {
            get { return Fields.OwnerMcimapPort[this]; }
            set { Fields.OwnerMcimapPort[this] = value; }
        }

        [DisplayName("Owner Mc Username"), Expression("jOwner.[MCUsername]")]
        public String OwnerMcUsername
        {
            get { return Fields.OwnerMcUsername[this]; }
            set { Fields.OwnerMcUsername[this] = value; }
        }

        [DisplayName("Owner Mc Password"), Expression("jOwner.[MCPassword]")]
        public String OwnerMcPassword
        {
            get { return Fields.OwnerMcPassword[this]; }
            set { Fields.OwnerMcPassword[this] = value; }
        }

        [DisplayName("Owner Start Time"), Expression("jOwner.[StartTime]")]
        public String OwnerStartTime
        {
            get { return Fields.OwnerStartTime[this]; }
            set { Fields.OwnerStartTime[this] = value; }
        }

        [DisplayName("Owner End Time"), Expression("jOwner.[EndTime]")]
        public String OwnerEndTime
        {
            get { return Fields.OwnerEndTime[this]; }
            set { Fields.OwnerEndTime[this] = value; }
        }

        [DisplayName("Owner Branch Id"), Expression("jOwner.[BranchId]")]
        public Int32? OwnerBranchId
        {
            get { return Fields.OwnerBranchId[this]; }
            set { Fields.OwnerBranchId[this] = value; }
        }

        [DisplayName("Owner Uid"), Expression("jOwner.[UID]")]
        public String OwnerUid
        {
            get { return Fields.OwnerUid[this]; }
            set { Fields.OwnerUid[this] = value; }
        }

        [DisplayName("Owner Non Operational"), Expression("jOwner.[NonOperational]")]
        public Boolean? OwnerNonOperational
        {
            get { return Fields.OwnerNonOperational[this]; }
            set { Fields.OwnerNonOperational[this] = value; }
        }

        [DisplayName("Last Updated By"), Expression("jLastUpdated.[Username]")]
        public String LastUpdatedUsername
        {
            get { return Fields.LastUpdatedUsername[this]; }
            set { Fields.LastUpdatedUsername[this] = value; }
        }

        [DisplayName("Last Updated Display Name"), Expression("jLastUpdated.[DisplayName]")]
        public String LastUpdatedDisplayName
        {
            get { return Fields.LastUpdatedDisplayName[this]; }
            set { Fields.LastUpdatedDisplayName[this] = value; }
        }

        [DisplayName("Last Updated Email"), Expression("jLastUpdated.[Email]")]
        public String LastUpdatedEmail
        {
            get { return Fields.LastUpdatedEmail[this]; }
            set { Fields.LastUpdatedEmail[this] = value; }
        }

        [DisplayName("Last Updated Source"), Expression("jLastUpdated.[Source]")]
        public String LastUpdatedSource
        {
            get { return Fields.LastUpdatedSource[this]; }
            set { Fields.LastUpdatedSource[this] = value; }
        }

        [DisplayName("Last Updated Password Hash"), Expression("jLastUpdated.[PasswordHash]")]
        public String LastUpdatedPasswordHash
        {
            get { return Fields.LastUpdatedPasswordHash[this]; }
            set { Fields.LastUpdatedPasswordHash[this] = value; }
        }

        [DisplayName("Last Updated Password Salt"), Expression("jLastUpdated.[PasswordSalt]")]
        public String LastUpdatedPasswordSalt
        {
            get { return Fields.LastUpdatedPasswordSalt[this]; }
            set { Fields.LastUpdatedPasswordSalt[this] = value; }
        }

        [DisplayName("Last Updated Last Directory Update"), Expression("jLastUpdated.[LastDirectoryUpdate]")]
        public DateTime? LastUpdatedLastDirectoryUpdate
        {
            get { return Fields.LastUpdatedLastDirectoryUpdate[this]; }
            set { Fields.LastUpdatedLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Last Updated User Image"), Expression("jLastUpdated.[UserImage]")]
        public String LastUpdatedUserImage
        {
            get { return Fields.LastUpdatedUserImage[this]; }
            set { Fields.LastUpdatedUserImage[this] = value; }
        }

        [DisplayName("Last Updated Insert Date"), Expression("jLastUpdated.[InsertDate]")]
        public DateTime? LastUpdatedInsertDate
        {
            get { return Fields.LastUpdatedInsertDate[this]; }
            set { Fields.LastUpdatedInsertDate[this] = value; }
        }

        [DisplayName("Last Updated Insert User Id"), Expression("jLastUpdated.[InsertUserId]")]
        public Int32? LastUpdatedInsertUserId
        {
            get { return Fields.LastUpdatedInsertUserId[this]; }
            set { Fields.LastUpdatedInsertUserId[this] = value; }
        }

        [DisplayName("Last Updated Update Date"), Expression("jLastUpdated.[UpdateDate]")]
        public DateTime? LastUpdatedUpdateDate
        {
            get { return Fields.LastUpdatedUpdateDate[this]; }
            set { Fields.LastUpdatedUpdateDate[this] = value; }
        }

        [DisplayName("Last Updated Update User Id"), Expression("jLastUpdated.[UpdateUserId]")]
        public Int32? LastUpdatedUpdateUserId
        {
            get { return Fields.LastUpdatedUpdateUserId[this]; }
            set { Fields.LastUpdatedUpdateUserId[this] = value; }
        }

        [DisplayName("Last Updated Is Active"), Expression("jLastUpdated.[IsActive]")]
        public Int16? LastUpdatedIsActive
        {
            get { return Fields.LastUpdatedIsActive[this]; }
            set { Fields.LastUpdatedIsActive[this] = value; }
        }

        [DisplayName("Last Updated Upper Level"), Expression("jLastUpdated.[UpperLevel]")]
        public Int32? LastUpdatedUpperLevel
        {
            get { return Fields.LastUpdatedUpperLevel[this]; }
            set { Fields.LastUpdatedUpperLevel[this] = value; }
        }

        [DisplayName("Last Updated Upper Level2"), Expression("jLastUpdated.[UpperLevel2]")]
        public Int32? LastUpdatedUpperLevel2
        {
            get { return Fields.LastUpdatedUpperLevel2[this]; }
            set { Fields.LastUpdatedUpperLevel2[this] = value; }
        }

        [DisplayName("Last Updated Upper Level3"), Expression("jLastUpdated.[UpperLevel3]")]
        public Int32? LastUpdatedUpperLevel3
        {
            get { return Fields.LastUpdatedUpperLevel3[this]; }
            set { Fields.LastUpdatedUpperLevel3[this] = value; }
        }

        [DisplayName("Last Updated Upper Level4"), Expression("jLastUpdated.[UpperLevel4]")]
        public Int32? LastUpdatedUpperLevel4
        {
            get { return Fields.LastUpdatedUpperLevel4[this]; }
            set { Fields.LastUpdatedUpperLevel4[this] = value; }
        }

        [DisplayName("Last Updated Upper Level5"), Expression("jLastUpdated.[UpperLevel5]")]
        public Int32? LastUpdatedUpperLevel5
        {
            get { return Fields.LastUpdatedUpperLevel5[this]; }
            set { Fields.LastUpdatedUpperLevel5[this] = value; }
        }

        [DisplayName("Last Updated Host"), Expression("jLastUpdated.[Host]")]
        public String LastUpdatedHost
        {
            get { return Fields.LastUpdatedHost[this]; }
            set { Fields.LastUpdatedHost[this] = value; }
        }

        [DisplayName("Last Updated Port"), Expression("jLastUpdated.[Port]")]
        public Int32? LastUpdatedPort
        {
            get { return Fields.LastUpdatedPort[this]; }
            set { Fields.LastUpdatedPort[this] = value; }
        }

        [DisplayName("Last Updated SSL"), Expression("jLastUpdated.[SSL]")]
        public Boolean? LastUpdatedSsl
        {
            get { return Fields.LastUpdatedSsl[this]; }
            set { Fields.LastUpdatedSsl[this] = value; }
        }

        [DisplayName("Last Updated Email Id"), Expression("jLastUpdated.[EmailId]")]
        public String LastUpdatedEmailId
        {
            get { return Fields.LastUpdatedEmailId[this]; }
            set { Fields.LastUpdatedEmailId[this] = value; }
        }

        [DisplayName("Last Updated Email Password"), Expression("jLastUpdated.[EmailPassword]")]
        public String LastUpdatedEmailPassword
        {
            get { return Fields.LastUpdatedEmailPassword[this]; }
            set { Fields.LastUpdatedEmailPassword[this] = value; }
        }

        [DisplayName("Last Updated Phone"), Expression("jLastUpdated.[Phone]")]
        public String LastUpdatedPhone
        {
            get { return Fields.LastUpdatedPhone[this]; }
            set { Fields.LastUpdatedPhone[this] = value; }
        }

        [DisplayName("Last Updated Mcsmtp Server"), Expression("jLastUpdated.[MCSMTPServer]")]
        public String LastUpdatedMcsmtpServer
        {
            get { return Fields.LastUpdatedMcsmtpServer[this]; }
            set { Fields.LastUpdatedMcsmtpServer[this] = value; }
        }

        [DisplayName("Last Updated Mcsmtp Port"), Expression("jLastUpdated.[MCSMTPPort]")]
        public Int32? LastUpdatedMcsmtpPort
        {
            get { return Fields.LastUpdatedMcsmtpPort[this]; }
            set { Fields.LastUpdatedMcsmtpPort[this] = value; }
        }

        [DisplayName("Last Updated Mcimap Server"), Expression("jLastUpdated.[MCIMAPServer]")]
        public String LastUpdatedMcimapServer
        {
            get { return Fields.LastUpdatedMcimapServer[this]; }
            set { Fields.LastUpdatedMcimapServer[this] = value; }
        }

        [DisplayName("Last Updated Mcimap Port"), Expression("jLastUpdated.[MCIMAPPort]")]
        public Int32? LastUpdatedMcimapPort
        {
            get { return Fields.LastUpdatedMcimapPort[this]; }
            set { Fields.LastUpdatedMcimapPort[this] = value; }
        }

        [DisplayName("Last Updated Mc Username"), Expression("jLastUpdated.[MCUsername]")]
        public String LastUpdatedMcUsername
        {
            get { return Fields.LastUpdatedMcUsername[this]; }
            set { Fields.LastUpdatedMcUsername[this] = value; }
        }

        [DisplayName("Last Updated Mc Password"), Expression("jLastUpdated.[MCPassword]")]
        public String LastUpdatedMcPassword
        {
            get { return Fields.LastUpdatedMcPassword[this]; }
            set { Fields.LastUpdatedMcPassword[this] = value; }
        }

        [DisplayName("Last Updated Start Time"), Expression("jLastUpdated.[StartTime]")]
        public String LastUpdatedStartTime
        {
            get { return Fields.LastUpdatedStartTime[this]; }
            set { Fields.LastUpdatedStartTime[this] = value; }
        }

        [DisplayName("Last Updated End Time"), Expression("jLastUpdated.[EndTime]")]
        public String LastUpdatedEndTime
        {
            get { return Fields.LastUpdatedEndTime[this]; }
            set { Fields.LastUpdatedEndTime[this] = value; }
        }

        [DisplayName("Last Updated Branch Id"), Expression("jLastUpdated.[BranchId]")]
        public Int32? LastUpdatedBranchId
        {
            get { return Fields.LastUpdatedBranchId[this]; }
            set { Fields.LastUpdatedBranchId[this] = value; }
        }

        [DisplayName("Last Updated Uid"), Expression("jLastUpdated.[UID]")]
        public String LastUpdatedUid
        {
            get { return Fields.LastUpdatedUid[this]; }
            set { Fields.LastUpdatedUid[this] = value; }
        }

        [DisplayName("Last Updated Non Operational"), Expression("jLastUpdated.[NonOperational]")]
        public Boolean? LastUpdatedNonOperational
        {
            get { return Fields.LastUpdatedNonOperational[this]; }
            set { Fields.LastUpdatedNonOperational[this] = value; }
        }

       

        public DMSRow()
            : base(Fields)
        {
        }
        
        public DMSRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ParentId;
            public StringField Title;
            public StringField Files;
            public Int32Field OwnerId;
            public Int32Field LastUpdatedId;
            public DateTimeField CreateDate;
            public DateTimeField UpdateDate;
            public Int32Field AssignedId;

            public StringField OwnerUsername;
            public StringField OwnerDisplayName;
            public StringField OwnerEmail;
            public StringField OwnerSource;
            public StringField OwnerPasswordHash;
            public StringField OwnerPasswordSalt;
            public DateTimeField OwnerLastDirectoryUpdate;
            public StringField OwnerUserImage;
            public DateTimeField OwnerInsertDate;
            public Int32Field OwnerInsertUserId;
            public DateTimeField OwnerUpdateDate;
            public Int32Field OwnerUpdateUserId;
            public Int16Field OwnerIsActive;
            public Int32Field OwnerUpperLevel;
            public Int32Field OwnerUpperLevel2;
            public Int32Field OwnerUpperLevel3;
            public Int32Field OwnerUpperLevel4;
            public Int32Field OwnerUpperLevel5;
            public StringField OwnerHost;
            public Int32Field OwnerPort;
            public BooleanField OwnerSsl;
            public StringField OwnerEmailId;
            public StringField OwnerEmailPassword;
            public StringField OwnerPhone;
            public StringField OwnerMcsmtpServer;
            public Int32Field OwnerMcsmtpPort;
            public StringField OwnerMcimapServer;
            public Int32Field OwnerMcimapPort;
            public StringField OwnerMcUsername;
            public StringField OwnerMcPassword;
            public StringField OwnerStartTime;
            public StringField OwnerEndTime;
            public Int32Field OwnerBranchId;
            public StringField OwnerUid;
            public BooleanField OwnerNonOperational;

            public StringField LastUpdatedUsername;
            public StringField LastUpdatedDisplayName;
            public StringField LastUpdatedEmail;
            public StringField LastUpdatedSource;
            public StringField LastUpdatedPasswordHash;
            public StringField LastUpdatedPasswordSalt;
            public DateTimeField LastUpdatedLastDirectoryUpdate;
            public StringField LastUpdatedUserImage;
            public DateTimeField LastUpdatedInsertDate;
            public Int32Field LastUpdatedInsertUserId;
            public DateTimeField LastUpdatedUpdateDate;
            public Int32Field LastUpdatedUpdateUserId;
            public Int16Field LastUpdatedIsActive;
            public Int32Field LastUpdatedUpperLevel;
            public Int32Field LastUpdatedUpperLevel2;
            public Int32Field LastUpdatedUpperLevel3;
            public Int32Field LastUpdatedUpperLevel4;
            public Int32Field LastUpdatedUpperLevel5;
            public StringField LastUpdatedHost;
            public Int32Field LastUpdatedPort;
            public BooleanField LastUpdatedSsl;
            public StringField LastUpdatedEmailId;
            public StringField LastUpdatedEmailPassword;
            public StringField LastUpdatedPhone;
            public StringField LastUpdatedMcsmtpServer;
            public Int32Field LastUpdatedMcsmtpPort;
            public StringField LastUpdatedMcimapServer;
            public Int32Field LastUpdatedMcimapPort;
            public StringField LastUpdatedMcUsername;
            public StringField LastUpdatedMcPassword;
            public StringField LastUpdatedStartTime;
            public StringField LastUpdatedEndTime;
            public Int32Field LastUpdatedBranchId;
            public StringField LastUpdatedUid;
            public BooleanField LastUpdatedNonOperational;

            public StringField AssignedUsername;
            public StringField AssignedDisplayName;
            public StringField AssignedEmail;
            public StringField AssignedSource;
            public StringField AssignedPasswordHash;
            public StringField AssignedPasswordSalt;
            public DateTimeField AssignedLastDirectoryUpdate;
            public StringField AssignedUserImage;
            public DateTimeField AssignedInsertDate;
            public Int32Field AssignedInsertUserId;
            public DateTimeField AssignedUpdateDate;
            public Int32Field AssignedUpdateUserId;
            public Int16Field AssignedIsActive;
            public Int32Field AssignedUpperLevel;
            public Int32Field AssignedUpperLevel2;
            public Int32Field AssignedUpperLevel3;
            public Int32Field AssignedUpperLevel4;
            public Int32Field AssignedUpperLevel5;
            public StringField AssignedHost;
            public Int32Field AssignedPort;
            public BooleanField AssignedSsl;
            public StringField AssignedEmailId;
            public StringField AssignedEmailPassword;
            public StringField AssignedPhone;
            public StringField AssignedMcsmtpServer;
            public Int32Field AssignedMcsmtpPort;
            public StringField AssignedMcimapServer;
            public Int32Field AssignedMcimapPort;
            public StringField AssignedMcUsername;
            public StringField AssignedMcPassword;
            public StringField AssignedStartTime;
            public StringField AssignedEndTime;
            public StringField AssignedUid;
            public BooleanField AssignedNonOperational;
            public Int32Field AssignedBranchId;
            public Int32Field AssignedCompanyId;
        }
    }
}
