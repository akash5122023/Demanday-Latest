
namespace AdvanceCRM.Tasks
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Products;
    using AdvanceCRM.Tasks;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Tasks"), TableName("[dbo].[Tasks]")]
    [DisplayName("Tasks"), InstanceName("Tasks")]
    [ReadPermission("Tasks:Read")]
    [InsertPermission("Tasks:Insert")]
    [UpdatePermission("Tasks:Update")]
    [DeletePermission("Tasks:Delete")]
    [LookupScript("Tasks.Tasks", Permission = "?")]
    public sealed class TasksRow : Row<TasksRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, SortOrder(1, true),IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Project"), ForeignKey("[dbo].[Project]", "Id"), LeftJoin("jProject"), TextualField("Project"),LookupInclude]
        [LookupEditor(typeof(ProjectRow), InplaceAdd = true)]
        public Int32? ProjectId
        {
            get { return Fields.ProjectId[this]; }
            set { Fields.ProjectId[this] = value; }
        }

        [DisplayName("Task Title"), Size(150), QuickSearch]
        public String TaskTitle
        {
            get { return Fields.TaskTitle[this]; }
            set { Fields.TaskTitle[this] = value; }
        }

        [DisplayName("Task"), Column("TaskID"), ForeignKey("[dbo].[Task]", "Id"), LeftJoin("jTask"), TextualField("Task"), QuickSearch]
        [LookupEditor(typeof(TaskRow), InplaceAdd = true)]
        public Int32? TaskId
        {
            get { return Fields.TaskId[this]; }
            set { Fields.TaskId[this] = value; }
        }

        [DisplayName("Task"), Expression("jTask.[Task]"),NameProperty]
        public String Task
        {
            get { return Fields.Task[this]; }
            set { Fields.Task[this] = value; }
        }


        [DisplayName("Description"), Size(1024), TextAreaEditor(Rows = 6)]
        public String Details
        {
            get { return Fields.Details[this]; }
            set { Fields.Details[this] = value; }
        }
        [DisplayName("Project"), Expression("jProject.[Project]")]
        public String Project
        {
            get { return Fields.Project[this]; }
            set { Fields.Project[this] = value; }
        }

        [DisplayName("Recurring"), NotNull]
        [BooleanSwitchEditor]
        public Boolean? Recurring
        {
            get { return Fields.Recurring[this]; }
            set { Fields.Recurring[this] = value; }
        }

        [DisplayName("Period")]
        public Masters.TaskPeriodMaster? Period
        {
            get { return (Masters.TaskPeriodMaster?)Fields.Period[this]; }
            set { Fields.Period[this] = (Int32?)value; }
        }

        [DisplayName("Creation Date"), NotNull]
        public DateTime? CreationDate
        {
            get { return Fields.CreationDate[this]; }
            set { Fields.CreationDate[this] = value; }
        }

        [DisplayName("Expected Completion")]
        public DateTime? ExpectedCompletion
        {
            get { return Fields.ExpectedCompletion[this]; }
            set { Fields.ExpectedCompletion[this] = value; }
        }

        [DisplayName("Created By"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jAssignedBy"), TextualField("AssignedByUsername"), QuickSearch]
        [Administration.UserEditor]
        public Int32? AssignedBy
        {
            get { return Fields.AssignedBy[this]; }
            set { Fields.AssignedBy[this] = value; }
        }

        [DisplayName("Assigned To"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jAssignedTo"), TextualField("AssignedToUsername"), QuickFilter]
        [Administration.UserEditor]
        public Int32? AssignedTo
        {
            get { return Fields.AssignedTo[this]; }
            set { Fields.AssignedTo[this] = value; }
        }

        [DisplayName("Status"), Column("StatusID"), NotNull, ForeignKey("[dbo].[TaskStatus]", "Id"), LeftJoin("jStatus"), TextualField("Status")]
        [LookupEditor(typeof(TaskStatusRow), InplaceAdd = true)]
        public Int32? StatusId
        {
            get { return Fields.StatusId[this]; }
            set { Fields.StatusId[this] = value; }
        }

        [DisplayName("Completion Date")]
        public DateTime? CompletionDate
        {
            get { return Fields.CompletionDate[this]; }
            set { Fields.CompletionDate[this] = value; }
        }

        [DisplayName("Type"), NotNull, ForeignKey("[dbo].[TaskType]", "Id"), LeftJoin("jType"), TextualField("Type")]
        [LookupEditor(typeof(TaskTypeRow), InplaceAdd = true)]
        public Int32? TypeId
        {
            get { return Fields.TypeId[this]; }
            set { Fields.TypeId[this] = value; }
        }

        [DisplayName("Attachments"), Size(1024)]
        [MultipleImageUploadEditor(FilenameFormat = "Tasks/~", CopyToHistory = true, AllowNonImage = true)]
        public String Attachments
        {
            get { return Fields.Attachments[this]; }
            set { Fields.Attachments[this] = value; }
        }

        [DisplayName("Priority"), NotNull, DefaultValue("1"), QuickSearch]
        public Masters.PriorityMaster? Priority
        {
            get { return (Masters.PriorityMaster?)Fields.Priority[this]; }
            set { Fields.Priority[this] = (Int32)value; }
        }

        [DisplayName("Resolution"), Size(2048), TextAreaEditor(Rows = 6)]
        public String Resolution
        {
            get { return Fields.Resolution[this]; }
            set { Fields.Resolution[this] = value; }
        }

        [DisplayName("Contacts"), ForeignKey("[dbo].[Contacts]", "Id"), LeftJoin("jContacts"), TextualField("ContactsName"), QuickSearch]
        [LookupEditor(typeof(ContactsRow), InplaceAdd = true)]
        public Int32? ContactsId
        {
            get { return Fields.ContactsId[this]; }
            set { Fields.ContactsId[this] = value; }
        }

        [DisplayName("Product"), ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProduct"), TextualField("ProductName"), QuickSearch]
        [LookupEditor(typeof(ProductsRow), InplaceAdd = true)]
        public Int32? ProductId
        {
            get { return Fields.ProductId[this]; }
            set { Fields.ProductId[this] = value; }
        }

        [DisplayName("Created By"), Expression("jAssignedBy.[Username]")]
        public String AssignedByUsername
        {
            get { return Fields.AssignedByUsername[this]; }
            set { Fields.AssignedByUsername[this] = value; }
        }

        [DisplayName("Created By Display Name"), Expression("jAssignedBy.[DisplayName]")]
        public String AssignedByDisplayName
        {
            get { return Fields.AssignedByDisplayName[this]; }
            set { Fields.AssignedByDisplayName[this] = value; }
        }

        [DisplayName("Created By Email"), Expression("jAssignedBy.[Email]")]
        public String AssignedByEmail
        {
            get { return Fields.AssignedByEmail[this]; }
            set { Fields.AssignedByEmail[this] = value; }
        }

        [DisplayName("Created By Source"), Expression("jAssignedBy.[Source]")]
        public String AssignedBySource
        {
            get { return Fields.AssignedBySource[this]; }
            set { Fields.AssignedBySource[this] = value; }
        }

        [DisplayName("Created By Password Hash"), Expression("jAssignedBy.[PasswordHash]")]
        public String AssignedByPasswordHash
        {
            get { return Fields.AssignedByPasswordHash[this]; }
            set { Fields.AssignedByPasswordHash[this] = value; }
        }

        [DisplayName("Created By Password Salt"), Expression("jAssignedBy.[PasswordSalt]")]
        public String AssignedByPasswordSalt
        {
            get { return Fields.AssignedByPasswordSalt[this]; }
            set { Fields.AssignedByPasswordSalt[this] = value; }
        }

        [DisplayName("Created By Last Directory Update"), Expression("jAssignedBy.[LastDirectoryUpdate]")]
        public DateTime? AssignedByLastDirectoryUpdate
        {
            get { return Fields.AssignedByLastDirectoryUpdate[this]; }
            set { Fields.AssignedByLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Created By User Image"), Expression("jAssignedBy.[UserImage]")]
        public String AssignedByUserImage
        {
            get { return Fields.AssignedByUserImage[this]; }
            set { Fields.AssignedByUserImage[this] = value; }
        }

        [DisplayName("Created By Insert Date"), Expression("jAssignedBy.[InsertDate]")]
        public DateTime? AssignedByInsertDate
        {
            get { return Fields.AssignedByInsertDate[this]; }
            set { Fields.AssignedByInsertDate[this] = value; }
        }

        [DisplayName("Created By Insert User Id"), Expression("jAssignedBy.[InsertUserId]")]
        public Int32? AssignedByInsertUserId
        {
            get { return Fields.AssignedByInsertUserId[this]; }
            set { Fields.AssignedByInsertUserId[this] = value; }
        }

        [DisplayName("Created By Update Date"), Expression("jAssignedBy.[UpdateDate]")]
        public DateTime? AssignedByUpdateDate
        {
            get { return Fields.AssignedByUpdateDate[this]; }
            set { Fields.AssignedByUpdateDate[this] = value; }
        }

        [DisplayName("Created By Update User Id"), Expression("jAssignedBy.[UpdateUserId]")]
        public Int32? AssignedByUpdateUserId
        {
            get { return Fields.AssignedByUpdateUserId[this]; }
            set { Fields.AssignedByUpdateUserId[this] = value; }
        }

        [DisplayName("Created By Is Active"), Expression("jAssignedBy.[IsActive]")]
        public Int16? AssignedByIsActive
        {
            get { return Fields.AssignedByIsActive[this]; }
            set { Fields.AssignedByIsActive[this] = value; }
        }

        [DisplayName("Created By Upper Level"), Expression("jAssignedBy.[UpperLevel]")]
        public Int32? AssignedByUpperLevel
        {
            get { return Fields.AssignedByUpperLevel[this]; }
            set { Fields.AssignedByUpperLevel[this] = value; }
        }

        [DisplayName("Created By Upper Level2"), Expression("jAssignedBy.[UpperLevel2]")]
        public Int32? AssignedByUpperLevel2
        {
            get { return Fields.AssignedByUpperLevel2[this]; }
            set { Fields.AssignedByUpperLevel2[this] = value; }
        }

        [DisplayName("Created By Upper Level3"), Expression("jAssignedBy.[UpperLevel3]")]
        public Int32? AssignedByUpperLevel3
        {
            get { return Fields.AssignedByUpperLevel3[this]; }
            set { Fields.AssignedByUpperLevel3[this] = value; }
        }

        [DisplayName("Created By Upper Level4"), Expression("jAssignedBy.[UpperLevel4]")]
        public Int32? AssignedByUpperLevel4
        {
            get { return Fields.AssignedByUpperLevel4[this]; }
            set { Fields.AssignedByUpperLevel4[this] = value; }
        }

        [DisplayName("Created By Upper Level5"), Expression("jAssignedBy.[UpperLevel5]")]
        public Int32? AssignedByUpperLevel5
        {
            get { return Fields.AssignedByUpperLevel5[this]; }
            set { Fields.AssignedByUpperLevel5[this] = value; }
        }

        [DisplayName("Created By Host"), Expression("jAssignedBy.[Host]")]
        public String AssignedByHost
        {
            get { return Fields.AssignedByHost[this]; }
            set { Fields.AssignedByHost[this] = value; }
        }

        [DisplayName("Created By Port"), Expression("jAssignedBy.[Port]")]
        public Int32? AssignedByPort
        {
            get { return Fields.AssignedByPort[this]; }
            set { Fields.AssignedByPort[this] = value; }
        }

        [DisplayName("Created By SSL"), Expression("jAssignedBy.[SSL]")]
        public Boolean? AssignedBySsl
        {
            get { return Fields.AssignedBySsl[this]; }
            set { Fields.AssignedBySsl[this] = value; }
        }

        [DisplayName("Created By Email Id"), Expression("jAssignedBy.[EmailId]")]
        public String AssignedByEmailId
        {
            get { return Fields.AssignedByEmailId[this]; }
            set { Fields.AssignedByEmailId[this] = value; }
        }

        [DisplayName("Created By Email Password"), Expression("jAssignedBy.[EmailPassword]")]
        public String AssignedByEmailPassword
        {
            get { return Fields.AssignedByEmailPassword[this]; }
            set { Fields.AssignedByEmailPassword[this] = value; }
        }

        [DisplayName("Created By Phone"), Expression("jAssignedBy.[Phone]")]
        public String AssignedByPhone
        {
            get { return Fields.AssignedByPhone[this]; }
            set { Fields.AssignedByPhone[this] = value; }
        }

        [DisplayName("Created By Mcsmtp Server"), Expression("jAssignedBy.[MCSMTPServer]")]
        public String AssignedByMcsmtpServer
        {
            get { return Fields.AssignedByMcsmtpServer[this]; }
            set { Fields.AssignedByMcsmtpServer[this] = value; }
        }

        [DisplayName("Created By Mcsmtp Port"), Expression("jAssignedBy.[MCSMTPPort]")]
        public Int32? AssignedByMcsmtpPort
        {
            get { return Fields.AssignedByMcsmtpPort[this]; }
            set { Fields.AssignedByMcsmtpPort[this] = value; }
        }

        [DisplayName("Created By Mcimap Server"), Expression("jAssignedBy.[MCIMAPServer]")]
        public String AssignedByMcimapServer
        {
            get { return Fields.AssignedByMcimapServer[this]; }
            set { Fields.AssignedByMcimapServer[this] = value; }
        }

        [DisplayName("Created By Mcimap Port"), Expression("jAssignedBy.[MCIMAPPort]")]
        public Int32? AssignedByMcimapPort
        {
            get { return Fields.AssignedByMcimapPort[this]; }
            set { Fields.AssignedByMcimapPort[this] = value; }
        }

        [DisplayName("Created By Mc Username"), Expression("jAssignedBy.[MCUsername]")]
        public String AssignedByMcUsername
        {
            get { return Fields.AssignedByMcUsername[this]; }
            set { Fields.AssignedByMcUsername[this] = value; }
        }

        [DisplayName("Created By Mc Password"), Expression("jAssignedBy.[MCPassword]")]
        public String AssignedByMcPassword
        {
            get { return Fields.AssignedByMcPassword[this]; }
            set { Fields.AssignedByMcPassword[this] = value; }
        }

        [DisplayName("Created By Start Time"), Expression("jAssignedBy.[StartTime]")]
        public String AssignedByStartTime
        {
            get { return Fields.AssignedByStartTime[this]; }
            set { Fields.AssignedByStartTime[this] = value; }
        }

        [DisplayName("Created By End Time"), Expression("jAssignedBy.[EndTime]")]
        public String AssignedByEndTime
        {
            get { return Fields.AssignedByEndTime[this]; }
            set { Fields.AssignedByEndTime[this] = value; }
        }

        [DisplayName("Created By Branch Id"), Expression("jAssignedBy.[BranchId]")]
        public Int32? AssignedByBranchId
        {
            get { return Fields.AssignedByBranchId[this]; }
            set { Fields.AssignedByBranchId[this] = value; }
        }

        [DisplayName("Created By Uid"), Expression("jAssignedBy.[UID]")]
        public String AssignedByUid
        {
            get { return Fields.AssignedByUid[this]; }
            set { Fields.AssignedByUid[this] = value; }
        }

        [DisplayName("Created By Non Operational"), Expression("jAssignedBy.[NonOperational]")]
        public Boolean? AssignedByNonOperational
        {
            get { return Fields.AssignedByNonOperational[this]; }
            set { Fields.AssignedByNonOperational[this] = value; }
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

        [DisplayName("Status"), Expression("jStatus.[Status]")]
        public String Status
        {
            get { return Fields.Status[this]; }
            set { Fields.Status[this] = value; }
        }

        [DisplayName("Type"), Expression("jType.[Type]")]
        public String Type
        {
            get { return Fields.Type[this]; }
            set { Fields.Type[this] = value; }
        }

        [DisplayName("Contacts Contact Type"), Expression("jContacts.[ContactType]")]
        public Int32? ContactsContactType
        {
            get { return Fields.ContactsContactType[this]; }
            set { Fields.ContactsContactType[this] = value; }
        }

        [DisplayName("Contact"), Expression("jContacts.[Name]")]
        public String ContactsName
        {
            get { return Fields.ContactsName[this]; }
            set { Fields.ContactsName[this] = value; }
        }

        [DisplayName("Phone"), Expression("jContacts.[Phone]")]
        public String ContactsPhone
        {
            get { return Fields.ContactsPhone[this]; }
            set { Fields.ContactsPhone[this] = value; }
        }

        [DisplayName("Contacts Email"), Expression("jContacts.[Email]")]
        public String ContactsEmail
        {
            get { return Fields.ContactsEmail[this]; }
            set { Fields.ContactsEmail[this] = value; }
        }

        [DisplayName("Address"), Expression("jContacts.[Address]")]
        public String ContactsAddress
        {
            get { return Fields.ContactsAddress[this]; }
            set { Fields.ContactsAddress[this] = value; }
        }

        [DisplayName("Contacts City Id"), Expression("jContacts.[CityId]")]
        public Int32? ContactsCityId
        {
            get { return Fields.ContactsCityId[this]; }
            set { Fields.ContactsCityId[this] = value; }
        }

        [DisplayName("Contacts State Id"), Expression("jContacts.[StateId]")]
        public Int32? ContactsStateId
        {
            get { return Fields.ContactsStateId[this]; }
            set { Fields.ContactsStateId[this] = value; }
        }

        [DisplayName("Contacts Pin"), Expression("jContacts.[Pin]")]
        public String ContactsPin
        {
            get { return Fields.ContactsPin[this]; }
            set { Fields.ContactsPin[this] = value; }
        }

        [DisplayName("Contacts Country"), Expression("jContacts.[Country]")]
        public Int32? ContactsCountry
        {
            get { return Fields.ContactsCountry[this]; }
            set { Fields.ContactsCountry[this] = value; }
        }

        [DisplayName("Contacts Website"), Expression("jContacts.[Website]")]
        public String ContactsWebsite
        {
            get { return Fields.ContactsWebsite[this]; }
            set { Fields.ContactsWebsite[this] = value; }
        }

        [DisplayName("Contacts Additional Info"), Expression("jContacts.[AdditionalInfo]")]
        public String ContactsAdditionalInfo
        {
            get { return Fields.ContactsAdditionalInfo[this]; }
            set { Fields.ContactsAdditionalInfo[this] = value; }
        }

        [DisplayName("Contacts Residential Phone"), Expression("jContacts.[ResidentialPhone]")]
        public String ContactsResidentialPhone
        {
            get { return Fields.ContactsResidentialPhone[this]; }
            set { Fields.ContactsResidentialPhone[this] = value; }
        }

        [DisplayName("Contacts Office Phone"), Expression("jContacts.[OfficePhone]")]
        public String ContactsOfficePhone
        {
            get { return Fields.ContactsOfficePhone[this]; }
            set { Fields.ContactsOfficePhone[this] = value; }
        }

        [DisplayName("Contacts Gender"), Expression("jContacts.[Gender]")]
        public Int32? ContactsGender
        {
            get { return Fields.ContactsGender[this]; }
            set { Fields.ContactsGender[this] = value; }
        }

        [DisplayName("Contacts Religion"), Expression("jContacts.[Religion]")]
        public Int32? ContactsReligion
        {
            get { return Fields.ContactsReligion[this]; }
            set { Fields.ContactsReligion[this] = value; }
        }

        [DisplayName("Contacts Area Id"), Expression("jContacts.[AreaId]")]
        public Int32? ContactsAreaId
        {
            get { return Fields.ContactsAreaId[this]; }
            set { Fields.ContactsAreaId[this] = value; }
        }

        [DisplayName("Contacts Marital Status"), Expression("jContacts.[MaritalStatus]")]
        public Int32? ContactsMaritalStatus
        {
            get { return Fields.ContactsMaritalStatus[this]; }
            set { Fields.ContactsMaritalStatus[this] = value; }
        }

        [DisplayName("Contacts Marriage Anniversary"), Expression("jContacts.[MarriageAnniversary]")]
        public DateTime? ContactsMarriageAnniversary
        {
            get { return Fields.ContactsMarriageAnniversary[this]; }
            set { Fields.ContactsMarriageAnniversary[this] = value; }
        }

        [DisplayName("Contacts Birthdate"), Expression("jContacts.[Birthdate]")]
        public DateTime? ContactsBirthdate
        {
            get { return Fields.ContactsBirthdate[this]; }
            set { Fields.ContactsBirthdate[this] = value; }
        }

        [DisplayName("Contacts Date Of Incorporation"), Expression("jContacts.[DateOfIncorporation]")]
        public DateTime? ContactsDateOfIncorporation
        {
            get { return Fields.ContactsDateOfIncorporation[this]; }
            set { Fields.ContactsDateOfIncorporation[this] = value; }
        }

        [DisplayName("Contacts Category Id"), Expression("jContacts.[CategoryId]")]
        public Int32? ContactsCategoryId
        {
            get { return Fields.ContactsCategoryId[this]; }
            set { Fields.ContactsCategoryId[this] = value; }
        }

        [DisplayName("Contacts Grade Id"), Expression("jContacts.[GradeId]")]
        public Int32? ContactsGradeId
        {
            get { return Fields.ContactsGradeId[this]; }
            set { Fields.ContactsGradeId[this] = value; }
        }

        [DisplayName("Contacts Type"), Expression("jContacts.[Type]")]
        public Int32? ContactsType
        {
            get { return Fields.ContactsType[this]; }
            set { Fields.ContactsType[this] = value; }
        }

        [DisplayName("Contacts Owner Id"), Expression("jContacts.[OwnerId]")]
        public Int32? ContactsOwnerId
        {
            get { return Fields.ContactsOwnerId[this]; }
            set { Fields.ContactsOwnerId[this] = value; }
        }

        [DisplayName("Contacts Assigned Id"), Expression("jContacts.[AssignedId]")]
        public Int32? ContactsAssignedId
        {
            get { return Fields.ContactsAssignedId[this]; }
            set { Fields.ContactsAssignedId[this] = value; }
        }

        [DisplayName("Contacts Channel Category"), Expression("jContacts.[ChannelCategory]")]
        public Int32? ContactsChannelCategory
        {
            get { return Fields.ContactsChannelCategory[this]; }
            set { Fields.ContactsChannelCategory[this] = value; }
        }

        [DisplayName("Contacts National Distributor"), Expression("jContacts.[NationalDistributor]")]
        public Int32? ContactsNationalDistributor
        {
            get { return Fields.ContactsNationalDistributor[this]; }
            set { Fields.ContactsNationalDistributor[this] = value; }
        }

        [DisplayName("Contacts Stockist"), Expression("jContacts.[Stockist]")]
        public Int32? ContactsStockist
        {
            get { return Fields.ContactsStockist[this]; }
            set { Fields.ContactsStockist[this] = value; }
        }

        [DisplayName("Contacts Distributor"), Expression("jContacts.[Distributor]")]
        public Int32? ContactsDistributor
        {
            get { return Fields.ContactsDistributor[this]; }
            set { Fields.ContactsDistributor[this] = value; }
        }

        [DisplayName("Contacts Dealer"), Expression("jContacts.[Dealer]")]
        public Int32? ContactsDealer
        {
            get { return Fields.ContactsDealer[this]; }
            set { Fields.ContactsDealer[this] = value; }
        }

        [DisplayName("Contacts Wholesaler"), Expression("jContacts.[Wholesaler]")]
        public Int32? ContactsWholesaler
        {
            get { return Fields.ContactsWholesaler[this]; }
            set { Fields.ContactsWholesaler[this] = value; }
        }

        [DisplayName("Contacts Reseller"), Expression("jContacts.[Reseller]")]
        public Int32? ContactsReseller
        {
            get { return Fields.ContactsReseller[this]; }
            set { Fields.ContactsReseller[this] = value; }
        }

        [DisplayName("Contacts GSTIN"), Expression("jContacts.[GSTIN]")]
        public String ContactsGstin
        {
            get { return Fields.ContactsGstin[this]; }
            set { Fields.ContactsGstin[this] = value; }
        }

        [DisplayName("Contacts Pan No"), Expression("jContacts.[PANNo]")]
        public String ContactsPanNo
        {
            get { return Fields.ContactsPanNo[this]; }
            set { Fields.ContactsPanNo[this] = value; }
        }

        [DisplayName("Contacts Cc Emails"), Expression("jContacts.[CCEmails]")]
        public String ContactsCcEmails
        {
            get { return Fields.ContactsCcEmails[this]; }
            set { Fields.ContactsCcEmails[this] = value; }
        }

        [DisplayName("Contacts Bcc Emails"), Expression("jContacts.[BCCEmails]")]
        public String ContactsBccEmails
        {
            get { return Fields.ContactsBccEmails[this]; }
            set { Fields.ContactsBccEmails[this] = value; }
        }

        [DisplayName("Contacts Attachment"), Expression("jContacts.[Attachment]")]
        public String ContactsAttachment
        {
            get { return Fields.ContactsAttachment[this]; }
            set { Fields.ContactsAttachment[this] = value; }
        }

        [DisplayName("Contacts E Com GSTIN"), Expression("jContacts.[EComGSTIN]")]
        public String ContactsEComGstin
        {
            get { return Fields.ContactsEComGstin[this]; }
            set { Fields.ContactsEComGstin[this] = value; }
        }

        [DisplayName("Contacts Creditors Opening"), Expression("jContacts.[CreditorsOpening]")]
        public Double? ContactsCreditorsOpening
        {
            get { return Fields.ContactsCreditorsOpening[this]; }
            set { Fields.ContactsCreditorsOpening[this] = value; }
        }

        [DisplayName("Contacts Debtors Opening"), Expression("jContacts.[DebtorsOpening]")]
        public Double? ContactsDebtorsOpening
        {
            get { return Fields.ContactsDebtorsOpening[this]; }
            set { Fields.ContactsDebtorsOpening[this] = value; }
        }

        [DisplayName("Contacts Bank Name"), Expression("jContacts.[BankName]")]
        public String ContactsBankName
        {
            get { return Fields.ContactsBankName[this]; }
            set { Fields.ContactsBankName[this] = value; }
        }

        [DisplayName("Contacts Account Number"), Expression("jContacts.[AccountNumber]")]
        public String ContactsAccountNumber
        {
            get { return Fields.ContactsAccountNumber[this]; }
            set { Fields.ContactsAccountNumber[this] = value; }
        }

        [DisplayName("Contacts Ifsc"), Expression("jContacts.[IFSC]")]
        public String ContactsIfsc
        {
            get { return Fields.ContactsIfsc[this]; }
            set { Fields.ContactsIfsc[this] = value; }
        }

        [DisplayName("Contacts Bank Type"), Expression("jContacts.[BankType]")]
        public String ContactsBankType
        {
            get { return Fields.ContactsBankType[this]; }
            set { Fields.ContactsBankType[this] = value; }
        }

        [DisplayName("Contacts Branch"), Expression("jContacts.[Branch]")]
        public String ContactsBranch
        {
            get { return Fields.ContactsBranch[this]; }
            set { Fields.ContactsBranch[this] = value; }
        }

        [DisplayName("Contacts Accounts Email"), Expression("jContacts.[AccountsEmail]")]
        public String ContactsAccountsEmail
        {
            get { return Fields.ContactsAccountsEmail[this]; }
            set { Fields.ContactsAccountsEmail[this] = value; }
        }

        [DisplayName("Contacts Purchase Email"), Expression("jContacts.[PurchaseEmail]")]
        public String ContactsPurchaseEmail
        {
            get { return Fields.ContactsPurchaseEmail[this]; }
            set { Fields.ContactsPurchaseEmail[this] = value; }
        }

        [DisplayName("Contacts Service Email"), Expression("jContacts.[ServiceEmail]")]
        public String ContactsServiceEmail
        {
            get { return Fields.ContactsServiceEmail[this]; }
            set { Fields.ContactsServiceEmail[this] = value; }
        }

        [DisplayName("Contacts Sales Email"), Expression("jContacts.[SalesEmail]")]
        public String ContactsSalesEmail
        {
            get { return Fields.ContactsSalesEmail[this]; }
            set { Fields.ContactsSalesEmail[this] = value; }
        }

        [DisplayName("Contacts Credit Days"), Expression("jContacts.[CreditDays]")]
        public Int32? ContactsCreditDays
        {
            get { return Fields.ContactsCreditDays[this]; }
            set { Fields.ContactsCreditDays[this] = value; }
        }

        [DisplayName("Contacts Customer Type"), Expression("jContacts.[CustomerType]")]
        public Int32? ContactsCustomerType
        {
            get { return Fields.ContactsCustomerType[this]; }
            set { Fields.ContactsCustomerType[this] = value; }
        }

        [DisplayName("Contacts Trasportation Id"), Expression("jContacts.[TrasportationId]")]
        public Int32? ContactsTrasportationId
        {
            get { return Fields.ContactsTrasportationId[this]; }
            set { Fields.ContactsTrasportationId[this] = value; }
        }

        [DisplayName("Contacts Tehsil Id"), Expression("jContacts.[TehsilId]")]
        public Int32? ContactsTehsilId
        {
            get { return Fields.ContactsTehsilId[this]; }
            set { Fields.ContactsTehsilId[this] = value; }
        }

        [DisplayName("Contacts Village Id"), Expression("jContacts.[VillageId]")]
        public Int32? ContactsVillageId
        {
            get { return Fields.ContactsVillageId[this]; }
            set { Fields.ContactsVillageId[this] = value; }
        }

        [DisplayName("Contacts Whatsapp"), Expression("jContacts.[Whatsapp]")]
        public String ContactsWhatsapp
        {
            get { return Fields.ContactsWhatsapp[this]; }
            set { Fields.ContactsWhatsapp[this] = value; }
        }

        [DisplayName("Product"), Expression("jProduct.[Name]")]
        public String ProductName
        {
            get { return Fields.ProductName[this]; }
            set { Fields.ProductName[this] = value; }
        }

        [DisplayName("Product Code"), Expression("jProduct.[Code]")]
        public String ProductCode
        {
            get { return Fields.ProductCode[this]; }
            set { Fields.ProductCode[this] = value; }
        }

        [DisplayName("Product Division Id"), Expression("jProduct.[DivisionId]")]
        public Int32? ProductDivisionId
        {
            get { return Fields.ProductDivisionId[this]; }
            set { Fields.ProductDivisionId[this] = value; }
        }

        [DisplayName("Product Group Id"), Expression("jProduct.[GroupId]")]
        public Int32? ProductGroupId
        {
            get { return Fields.ProductGroupId[this]; }
            set { Fields.ProductGroupId[this] = value; }
        }

        [DisplayName("Product Selling Price"), Expression("jProduct.[SellingPrice]")]
        public Double? ProductSellingPrice
        {
            get { return Fields.ProductSellingPrice[this]; }
            set { Fields.ProductSellingPrice[this] = value; }
        }

        [DisplayName("Product MRP"), Expression("jProduct.[MRP]")]
        public Double? ProductMrp
        {
            get { return Fields.ProductMrp[this]; }
            set { Fields.ProductMrp[this] = value; }
        }

        [DisplayName("Product Description"), Expression("jProduct.[Description]")]
        public String ProductDescription
        {
            get { return Fields.ProductDescription[this]; }
            set { Fields.ProductDescription[this] = value; }
        }

        [DisplayName("Product Tax Id1"), Expression("jProduct.[TaxId1]")]
        public Int32? ProductTaxId1
        {
            get { return Fields.ProductTaxId1[this]; }
            set { Fields.ProductTaxId1[this] = value; }
        }

        [DisplayName("Product Tax Id2"), Expression("jProduct.[TaxId2]")]
        public Int32? ProductTaxId2
        {
            get { return Fields.ProductTaxId2[this]; }
            set { Fields.ProductTaxId2[this] = value; }
        }

        [DisplayName("Product Image"), Expression("jProduct.[Image]")]
        public String ProductImage
        {
            get { return Fields.ProductImage[this]; }
            set { Fields.ProductImage[this] = value; }
        }

        [DisplayName("Product Tech Specs"), Expression("jProduct.[TechSpecs]")]
        public String ProductTechSpecs
        {
            get { return Fields.ProductTechSpecs[this]; }
            set { Fields.ProductTechSpecs[this] = value; }
        }

        [DisplayName("Product Hsn"), Expression("jProduct.[HSN]")]
        public String ProductHsn
        {
            get { return Fields.ProductHsn[this]; }
            set { Fields.ProductHsn[this] = value; }
        }

        [DisplayName("Product Channel Customer Price"), Expression("jProduct.[ChannelCustomerPrice]")]
        public Double? ProductChannelCustomerPrice
        {
            get { return Fields.ProductChannelCustomerPrice[this]; }
            set { Fields.ProductChannelCustomerPrice[this] = value; }
        }

        [DisplayName("Product Reseller Price"), Expression("jProduct.[ResellerPrice]")]
        public Double? ProductResellerPrice
        {
            get { return Fields.ProductResellerPrice[this]; }
            set { Fields.ProductResellerPrice[this] = value; }
        }

        [DisplayName("Product Wholesaler Price"), Expression("jProduct.[WholesalerPrice]")]
        public Double? ProductWholesalerPrice
        {
            get { return Fields.ProductWholesalerPrice[this]; }
            set { Fields.ProductWholesalerPrice[this] = value; }
        }

        [DisplayName("Product Dealer Price"), Expression("jProduct.[DealerPrice]")]
        public Double? ProductDealerPrice
        {
            get { return Fields.ProductDealerPrice[this]; }
            set { Fields.ProductDealerPrice[this] = value; }
        }

        [DisplayName("Product Distributor Price"), Expression("jProduct.[DistributorPrice]")]
        public Double? ProductDistributorPrice
        {
            get { return Fields.ProductDistributorPrice[this]; }
            set { Fields.ProductDistributorPrice[this] = value; }
        }

        [DisplayName("Product Stockiest Price"), Expression("jProduct.[StockiestPrice]")]
        public Double? ProductStockiestPrice
        {
            get { return Fields.ProductStockiestPrice[this]; }
            set { Fields.ProductStockiestPrice[this] = value; }
        }

        [DisplayName("Product National Distributor Price"), Expression("jProduct.[NationalDistributorPrice]")]
        public Double? ProductNationalDistributorPrice
        {
            get { return Fields.ProductNationalDistributorPrice[this]; }
            set { Fields.ProductNationalDistributorPrice[this] = value; }
        }

        [DisplayName("Product Minimum Stock"), Expression("jProduct.[MinimumStock]")]
        public Double? ProductMinimumStock
        {
            get { return Fields.ProductMinimumStock[this]; }
            set { Fields.ProductMinimumStock[this] = value; }
        }

        [DisplayName("Product Maximum Stock"), Expression("jProduct.[MaximumStock]")]
        public Double? ProductMaximumStock
        {
            get { return Fields.ProductMaximumStock[this]; }
            set { Fields.ProductMaximumStock[this] = value; }
        }

        [DisplayName("Product Raw Material"), Expression("jProduct.[RawMaterial]")]
        public Boolean? ProductRawMaterial
        {
            get { return Fields.ProductRawMaterial[this]; }
            set { Fields.ProductRawMaterial[this] = value; }
        }

        [DisplayName("Product Purchase Price"), Expression("jProduct.[PurchasePrice]")]
        public Double? ProductPurchasePrice
        {
            get { return Fields.ProductPurchasePrice[this]; }
            set { Fields.ProductPurchasePrice[this] = value; }
        }

        [DisplayName("Product Opening Stock"), Expression("jProduct.[OpeningStock]")]
        public Double? ProductOpeningStock
        {
            get { return Fields.ProductOpeningStock[this]; }
            set { Fields.ProductOpeningStock[this] = value; }
        }

        [DisplayName("Product Unit Id"), Expression("jProduct.[UnitId]")]
        public Int32? ProductUnitId
        {
            get { return Fields.ProductUnitId[this]; }
            set { Fields.ProductUnitId[this] = value; }
        }

        [NotesEditor, NotMapped]
        [DisplayName("")]
        public List<NoteRow> NoteList
        {
            get { return Fields.NoteList[this]; }
            set { Fields.NoteList[this] = value; }
        }

        [TimelineEditor, NotMapped]
        [DisplayName("")]
        public List<TimelineRow> Timeline
        {
            get { return Fields.Timeline[this]; }
            set { Fields.Timeline[this] = value; }
        }

        [DisplayName("Multi Assign")]
        [LookupEditor(typeof(UserRow), Multiple = true), NotMapped]
        [LinkingSetRelation(typeof(TaskWatcherRow), "TasksId", "AssignedId")]
        public List<Int32> WatcherList
        {
            get { return Fields.WatcherList[this]; }
            set { Fields.WatcherList[this] = value; }
        }
     

        public TasksRow()
            : base(Fields)
        {
        }

        public TasksRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField TaskTitle;

            public Int32Field TaskId;
      
            public StringField Details;
            public DateTimeField CreationDate;
            public DateTimeField ExpectedCompletion;
            public Int32Field AssignedBy;
            public Int32Field AssignedTo;
            public Int32Field StatusId;
            public DateTimeField CompletionDate;
            public Int32Field TypeId;
            public StringField Attachments;
            public Int32Field Priority;
            public StringField Resolution;
            public Int32Field ContactsId;
            public Int32Field ProductId;
            public Int32Field ProjectId;
            public BooleanField Recurring;
            public Int32Field Period;

            public StringField AssignedByUsername;
            public StringField AssignedByDisplayName;
            public StringField AssignedByEmail;
            public StringField AssignedBySource;
            public StringField AssignedByPasswordHash;
            public StringField AssignedByPasswordSalt;
            public DateTimeField AssignedByLastDirectoryUpdate;
            public StringField AssignedByUserImage;
            public DateTimeField AssignedByInsertDate;
            public Int32Field AssignedByInsertUserId;
            public DateTimeField AssignedByUpdateDate;
            public Int32Field AssignedByUpdateUserId;
            public Int16Field AssignedByIsActive;
            public Int32Field AssignedByUpperLevel;
            public Int32Field AssignedByUpperLevel2;
            public Int32Field AssignedByUpperLevel3;
            public Int32Field AssignedByUpperLevel4;
            public Int32Field AssignedByUpperLevel5;
            public StringField AssignedByHost;
            public Int32Field AssignedByPort;
            public BooleanField AssignedBySsl;
            public StringField AssignedByEmailId;
            public StringField AssignedByEmailPassword;
            public StringField AssignedByPhone;
            public StringField AssignedByMcsmtpServer;
            public Int32Field AssignedByMcsmtpPort;
            public StringField AssignedByMcimapServer;
            public Int32Field AssignedByMcimapPort;
            public StringField AssignedByMcUsername;
            public StringField AssignedByMcPassword;
            public StringField AssignedByStartTime;
            public StringField AssignedByEndTime;
            public Int32Field AssignedByBranchId;
            public StringField AssignedByUid;
            public BooleanField AssignedByNonOperational;

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

            public StringField Status;

            public StringField Type;

            public StringField Task;


            public Int32Field ContactsContactType;
            public StringField ContactsName;
            public StringField ContactsPhone;
            public StringField ContactsEmail;
            public StringField ContactsAddress;
            public Int32Field ContactsCityId;
            public Int32Field ContactsStateId;
            public StringField ContactsPin;
            public Int32Field ContactsCountry;
            public StringField ContactsWebsite;
            public StringField ContactsAdditionalInfo;
            public StringField ContactsResidentialPhone;
            public StringField ContactsOfficePhone;
            public Int32Field ContactsGender;
            public Int32Field ContactsReligion;
            public Int32Field ContactsAreaId;
            public Int32Field ContactsMaritalStatus;
            public DateTimeField ContactsMarriageAnniversary;
            public DateTimeField ContactsBirthdate;
            public DateTimeField ContactsDateOfIncorporation;
            public Int32Field ContactsCategoryId;
            public Int32Field ContactsGradeId;
            public Int32Field ContactsType;
            public Int32Field ContactsOwnerId;
            public Int32Field ContactsAssignedId;
            public Int32Field ContactsChannelCategory;
            public Int32Field ContactsNationalDistributor;
            public Int32Field ContactsStockist;
            public Int32Field ContactsDistributor;
            public Int32Field ContactsDealer;
            public Int32Field ContactsWholesaler;
            public Int32Field ContactsReseller;
            public StringField ContactsGstin;
            public StringField ContactsPanNo;
            public StringField ContactsCcEmails;
            public StringField ContactsBccEmails;
            
            
            public StringField ContactsAttachment;
            public StringField ContactsEComGstin;
            public DoubleField ContactsCreditorsOpening;
            public DoubleField ContactsDebtorsOpening;
            public StringField ContactsBankName;
            public StringField ContactsAccountNumber;
            public StringField ContactsIfsc;
            public StringField ContactsBankType;
            public StringField ContactsBranch;
            public StringField ContactsAccountsEmail;
            public StringField ContactsPurchaseEmail;
            public StringField ContactsServiceEmail;
            public StringField ContactsSalesEmail;
            public Int32Field ContactsCreditDays;
            public Int32Field ContactsCustomerType;
            public Int32Field ContactsTrasportationId;
            public Int32Field ContactsTehsilId;
            public Int32Field ContactsVillageId;
            public StringField ContactsWhatsapp;

            public StringField ProductName;
            public StringField ProductCode;
            public Int32Field ProductDivisionId;
            public Int32Field ProductGroupId;
            public DoubleField ProductSellingPrice;
            public DoubleField ProductMrp;
            public StringField ProductDescription;
            public Int32Field ProductTaxId1;
            public Int32Field ProductTaxId2;
            public StringField ProductImage;
            public StringField ProductTechSpecs;
            public StringField ProductHsn;
            public DoubleField ProductChannelCustomerPrice;
            public DoubleField ProductResellerPrice;
            public DoubleField ProductWholesalerPrice;
            public DoubleField ProductDealerPrice;
            public DoubleField ProductDistributorPrice;
            public DoubleField ProductStockiestPrice;
            public DoubleField ProductNationalDistributorPrice;
            public DoubleField ProductMinimumStock;
            public DoubleField ProductMaximumStock;
            public BooleanField ProductRawMaterial;
            public DoubleField ProductPurchasePrice;
            public DoubleField ProductOpeningStock;
            public Int32Field ProductUnitId;

            public StringField Project;

            public readonly ListField<Int32> WatcherList;

            public RowListField<NoteRow> NoteList;
            public RowListField<TimelineRow> Timeline;
        }
    }
}
