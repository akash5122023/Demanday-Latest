
namespace AdvanceCRM.Tasks
{
    using AdvanceCRM.Administration;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Tasks"), TableName("[dbo].[TaskWatcher]")]
    [DisplayName("Task Watcher"), InstanceName("Task Watcher")]
    [ReadPermission("Tasks:Read")]
    [ModifyPermission("Tasks:Modify")]

    public sealed class TaskWatcherRow : Row<TaskWatcherRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Assigned"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jAssigned"), TextualField("AssignedUsername")]
        [LookupEditor(typeof(UserRow))]
        public Int32? AssignedId
        {
            get { return Fields.AssignedId[this]; }
            set { Fields.AssignedId[this] = value; }
        }

        [DisplayName("Tasks"), NotNull, ForeignKey("[dbo].[Tasks]", "Id"), LeftJoin("jTasks"), TextualField("TasksName")]
        [LookupEditor(typeof(TasksRow), InplaceAdd = true)]
        public Int32? TasksId
        {
            get { return Fields.TasksId[this]; }
            set { Fields.TasksId[this] = value; }
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

        [DisplayName("Assigned SSL"), Expression("jAssigned.[SSL]")]
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

        [DisplayName("Assigned Branch Id"), Expression("jAssigned.[BranchId]")]
        public Int32? AssignedBranchId
        {
            get { return Fields.AssignedBranchId[this]; }
            set { Fields.AssignedBranchId[this] = value; }
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

        [DisplayName("Tasks Task"), Expression("jTasks.[Task]")]
        public String TasksTask
        {
            get { return Fields.TasksTask[this]; }
            set { Fields.TasksTask[this] = value; }
        }

        [DisplayName("Tasks Details"), Expression("jTasks.[Details]")]
        public String TasksDetails
        {
            get { return Fields.TasksDetails[this]; }
            set { Fields.TasksDetails[this] = value; }
        }

        [DisplayName("Tasks Creation Date"), Expression("jTasks.[CreationDate]")]
        public DateTime? TasksCreationDate
        {
            get { return Fields.TasksCreationDate[this]; }
            set { Fields.TasksCreationDate[this] = value; }
        }

        [DisplayName("Tasks Expected Completion"), Expression("jTasks.[ExpectedCompletion]")]
        public DateTime? TasksExpectedCompletion
        {
            get { return Fields.TasksExpectedCompletion[this]; }
            set { Fields.TasksExpectedCompletion[this] = value; }
        }

        [DisplayName("Tasks Created By"), Expression("jTasks.[AssignedBy]")]
        public Int32? TasksAssignedBy
        {
            get { return Fields.TasksAssignedBy[this]; }
            set { Fields.TasksAssignedBy[this] = value; }
        }

        [DisplayName("Tasks Assigned To"), Expression("jTasks.[AssignedTo]")]
        public Int32? TasksAssignedTo
        {
            get { return Fields.TasksAssignedTo[this]; }
            set { Fields.TasksAssignedTo[this] = value; }
        }

        [DisplayName("Tasks Status Id"), Expression("jTasks.[StatusID]")]
        public Int32? TasksStatusId
        {
            get { return Fields.TasksStatusId[this]; }
            set { Fields.TasksStatusId[this] = value; }
        }

        [DisplayName("Tasks Completion Date"), Expression("jTasks.[CompletionDate]")]
        public DateTime? TasksCompletionDate
        {
            get { return Fields.TasksCompletionDate[this]; }
            set { Fields.TasksCompletionDate[this] = value; }
        }

        [DisplayName("Tasks Type Id"), Expression("jTasks.[TypeId]")]
        public Int32? TasksTypeId
        {
            get { return Fields.TasksTypeId[this]; }
            set { Fields.TasksTypeId[this] = value; }
        }

        [DisplayName("Tasks Phone"), Expression("jTasks.[Phone]")]
        public String TasksPhone
        {
            get { return Fields.TasksPhone[this]; }
            set { Fields.TasksPhone[this] = value; }
        }

        [DisplayName("Tasks Attachments"), Expression("jTasks.[Attachments]")]
        public String TasksAttachments
        {
            get { return Fields.TasksAttachments[this]; }
            set { Fields.TasksAttachments[this] = value; }
        }

        [DisplayName("Tasks Priority"), Expression("jTasks.[Priority]")]
        public Int32? TasksPriority
        {
            get { return Fields.TasksPriority[this]; }
            set { Fields.TasksPriority[this] = value; }
        }

        [DisplayName("Tasks Resolution"), Expression("jTasks.[Resolution]")]
        public String TasksResolution
        {
            get { return Fields.TasksResolution[this]; }
            set { Fields.TasksResolution[this] = value; }
        }

        [DisplayName("Tasks Contacts Id"), Expression("jTasks.[ContactsId]")]
        public Int32? TasksContactsId
        {
            get { return Fields.TasksContactsId[this]; }
            set { Fields.TasksContactsId[this] = value; }
        }

        [DisplayName("Tasks Product Id"), Expression("jTasks.[ProductId]")]
        public Int32? TasksProductId
        {
            get { return Fields.TasksProductId[this]; }
            set { Fields.TasksProductId[this] = value; }
        }

       

        public TaskWatcherRow()
            : base(Fields)
        {
        }
        
        public TaskWatcherRow(RowFields fields )
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field AssignedId;
            public Int32Field TasksId;

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
            public Int32Field AssignedBranchId;
            public StringField AssignedUid;
            public BooleanField AssignedNonOperational;

            public StringField TasksTask;
            public StringField TasksDetails;
            public DateTimeField TasksCreationDate;
            public DateTimeField TasksExpectedCompletion;
            public Int32Field TasksAssignedBy;
            public Int32Field TasksAssignedTo;
            public Int32Field TasksStatusId;
            public DateTimeField TasksCompletionDate;
            public Int32Field TasksTypeId;
            public StringField TasksPhone;
            public StringField TasksAttachments;
            public Int32Field TasksPriority;
            public StringField TasksResolution;
            public Int32Field TasksContactsId;
            public Int32Field TasksProductId;
        }
    }
}
