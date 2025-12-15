
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[RawTelecall]")]
    [DisplayName("TelecallDetails"), InstanceName("TelecallDetails")]
    [ReadPermission("RawTelecall:Inbox")]
    [ModifyPermission("RawTelecall:Inbox")]
    public sealed class RawTelecallRow : Row<RawTelecallRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Company Name"), Size(150), QuickSearch,NameProperty]
        public String CompanyName
        {
            get { return Fields.CompanyName[this]; }
            set { Fields.CompanyName[this] = value; }
        }

        [DisplayName("Name"), Size(150)]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Phone"), Size(20)]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Email"), Size(50)]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("Details"), Size(1000),TextAreaEditor(Rows =4)]
        public String Details
        {
            get { return Fields.Details[this]; }
            set { Fields.Details[this] = value; }
        }

        [DisplayName("Created By"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jCreatedBy"), TextualField("CreatedByUsername")]
        [Administration.UserEditor]
        public Int32? CreatedBy
        {
            get { return Fields.CreatedBy[this]; }
            set { Fields.CreatedBy[this] = value; }
        }

        [DisplayName("Assigned To"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jAssignedTo"), TextualField("AssignedToUsername")]
        [Administration.UserEditor]
        public Int32? AssignedTo
        {
            get { return Fields.AssignedTo[this]; }
            set { Fields.AssignedTo[this] = value; }
        }

        [DisplayName("Is Moved"), NotNull]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }

        [DisplayName("Created By Username"), Expression("jCreatedBy.[Username]")]
        public String CreatedByUsername
        {
            get { return Fields.CreatedByUsername[this]; }
            set { Fields.CreatedByUsername[this] = value; }
        }
        [DisplayName("Feedback"),TextAreaEditor(Rows =3)]
        public String Feedback
        {
            get { return Fields.Feedback[this]; }
            set { Fields.Feedback[this] = value; }
        }
        [DisplayName("Created By Display Name"), Expression("jCreatedBy.[DisplayName]")]
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

        [DisplayName("Created By Enquiry"), Expression("jCreatedBy.[Enquiry]")]
        public Boolean? CreatedByEnquiry
        {
            get { return Fields.CreatedByEnquiry[this]; }
            set { Fields.CreatedByEnquiry[this] = value; }
        }

        [DisplayName("Created By Quotation"), Expression("jCreatedBy.[Quotation]")]
        public Boolean? CreatedByQuotation
        {
            get { return Fields.CreatedByQuotation[this]; }
            set { Fields.CreatedByQuotation[this] = value; }
        }

        [DisplayName("Created By Tasks"), Expression("jCreatedBy.[Tasks]")]
        public Boolean? CreatedByTasks
        {
            get { return Fields.CreatedByTasks[this]; }
            set { Fields.CreatedByTasks[this] = value; }
        }

        [DisplayName("Created By Contacts"), Expression("jCreatedBy.[Contacts]")]
        public Boolean? CreatedByContacts
        {
            get { return Fields.CreatedByContacts[this]; }
            set { Fields.CreatedByContacts[this] = value; }
        }

        [DisplayName("Created By Purchase"), Expression("jCreatedBy.[Purchase]")]
        public Boolean? CreatedByPurchase
        {
            get { return Fields.CreatedByPurchase[this]; }
            set { Fields.CreatedByPurchase[this] = value; }
        }

        [DisplayName("Created By Sales"), Expression("jCreatedBy.[Sales]")]
        public Boolean? CreatedBySales
        {
            get { return Fields.CreatedBySales[this]; }
            set { Fields.CreatedBySales[this] = value; }
        }

        [DisplayName("Created By Cms"), Expression("jCreatedBy.[CMS]")]
        public Boolean? CreatedByCms
        {
            get { return Fields.CreatedByCms[this]; }
            set { Fields.CreatedByCms[this] = value; }
        }

        [DisplayName("Assigned To Username"), Expression("jAssignedTo.[Username]")]
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

        [DisplayName("Assigned To Ssl"), Expression("jAssignedTo.[SSL]")]
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

        [DisplayName("Assigned To Branch Id"), Expression("jAssignedTo.[BranchId]")]
        public Int32? AssignedToBranchId
        {
            get { return Fields.AssignedToBranchId[this]; }
            set { Fields.AssignedToBranchId[this] = value; }
        }

        [DisplayName("Assigned To Company Id"), Expression("jAssignedTo.[CompanyId]")]
        public Int32? AssignedToCompanyId
        {
            get { return Fields.AssignedToCompanyId[this]; }
            set { Fields.AssignedToCompanyId[this] = value; }
        }

        [DisplayName("Assigned To Enquiry"), Expression("jAssignedTo.[Enquiry]")]
        public Boolean? AssignedToEnquiry
        {
            get { return Fields.AssignedToEnquiry[this]; }
            set { Fields.AssignedToEnquiry[this] = value; }
        }

        [DisplayName("Assigned To Quotation"), Expression("jAssignedTo.[Quotation]")]
        public Boolean? AssignedToQuotation
        {
            get { return Fields.AssignedToQuotation[this]; }
            set { Fields.AssignedToQuotation[this] = value; }
        }

        [DisplayName("Assigned To Tasks"), Expression("jAssignedTo.[Tasks]")]
        public Boolean? AssignedToTasks
        {
            get { return Fields.AssignedToTasks[this]; }
            set { Fields.AssignedToTasks[this] = value; }
        }

        [DisplayName("Assigned To Contacts"), Expression("jAssignedTo.[Contacts]")]
        public Boolean? AssignedToContacts
        {
            get { return Fields.AssignedToContacts[this]; }
            set { Fields.AssignedToContacts[this] = value; }
        }

        [DisplayName("Assigned To Purchase"), Expression("jAssignedTo.[Purchase]")]
        public Boolean? AssignedToPurchase
        {
            get { return Fields.AssignedToPurchase[this]; }
            set { Fields.AssignedToPurchase[this] = value; }
        }

        [DisplayName("Assigned To Sales"), Expression("jAssignedTo.[Sales]")]
        public Boolean? AssignedToSales
        {
            get { return Fields.AssignedToSales[this]; }
            set { Fields.AssignedToSales[this] = value; }
        }

        [DisplayName("Assigned To Cms"), Expression("jAssignedTo.[CMS]")]
        public Boolean? AssignedToCms
        {
            get { return Fields.AssignedToCms[this]; }
            set { Fields.AssignedToCms[this] = value; }
        }

    

        public RawTelecallRow()
            : base(Fields)
        {
        }
         public RawTelecallRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField CompanyName;
            public StringField Name;
            public StringField Phone;
            public StringField Email;
            public StringField Details;
            public Int32Field CreatedBy;
            public Int32Field AssignedTo;
            public BooleanField IsMoved;
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
            public BooleanField CreatedByEnquiry;
            public BooleanField CreatedByQuotation;
            public BooleanField CreatedByTasks;
            public BooleanField CreatedByContacts;
            public BooleanField CreatedByPurchase;
            public BooleanField CreatedBySales;
            public BooleanField CreatedByCms;

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
            public StringField AssignedToUid;
            public BooleanField AssignedToNonOperational;
            public Int32Field AssignedToBranchId;
            public Int32Field AssignedToCompanyId;
            public BooleanField AssignedToEnquiry;
            public BooleanField AssignedToQuotation;
            public BooleanField AssignedToTasks;
            public BooleanField AssignedToContacts;
            public BooleanField AssignedToPurchase;
            public BooleanField AssignedToSales;
            public BooleanField AssignedToCms;
        }
    }
}
