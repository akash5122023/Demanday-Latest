
namespace AdvanceCRM.Enquiry
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Enquiry"), TableName("[dbo].[MultiRepEnquiry]")]
    [DisplayName("Multi Rep Enquiry"), InstanceName("Multi Rep Enquiry")]
    [ReadPermission("Enquiry:Read")]
    [InsertPermission("Enquiry:Insert")]
    [UpdatePermission("Enquiry:Update")]
    [DeletePermission("Enquiry:Delete")]
    public sealed class MultiRepEnquiryRow : Row<MultiRepEnquiryRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Assigned"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jAssigned"), TextualField("AssignedUsername")]
        public Int32? AssignedId
        {
            get { return Fields.AssignedId[this]; }
            set { Fields.AssignedId[this] = value; }
        }

        [DisplayName("Enquiry"), NotNull, ForeignKey("[dbo].[Enquiry]", "Id"), LeftJoin("jEnquiry"), TextualField("EnquiryAdditionalInfo")]
        public Int32? EnquiryId
        {
            get { return Fields.EnquiryId[this]; }
            set { Fields.EnquiryId[this] = value; }
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

        [DisplayName("Enquiry Contacts Id"), Expression("jEnquiry.[ContactsId]")]
        public Int32? EnquiryContactsId
        {
            get { return Fields.EnquiryContactsId[this]; }
            set { Fields.EnquiryContactsId[this] = value; }
        }

        [DisplayName("Enquiry Date"), Expression("jEnquiry.[Date]")]
        public DateTime? EnquiryDate
        {
            get { return Fields.EnquiryDate[this]; }
            set { Fields.EnquiryDate[this] = value; }
        }

        [DisplayName("Enquiry Status"), Expression("jEnquiry.[Status]")]
        public Int32? EnquiryStatus
        {
            get { return Fields.EnquiryStatus[this]; }
            set { Fields.EnquiryStatus[this] = value; }
        }

        [DisplayName("Enquiry Type"), Expression("jEnquiry.[Type]")]
        public Int32? EnquiryType
        {
            get { return Fields.EnquiryType[this]; }
            set { Fields.EnquiryType[this] = value; }
        }

        [DisplayName("Enquiry Additional Info"), Expression("jEnquiry.[AdditionalInfo]")]
        public String EnquiryAdditionalInfo
        {
            get { return Fields.EnquiryAdditionalInfo[this]; }
            set { Fields.EnquiryAdditionalInfo[this] = value; }
        }

        [DisplayName("Enquiry Source Id"), Expression("jEnquiry.[SourceId]")]
        public Int32? EnquirySourceId
        {
            get { return Fields.EnquirySourceId[this]; }
            set { Fields.EnquirySourceId[this] = value; }
        }

        [DisplayName("Enquiry Stage Id"), Expression("jEnquiry.[StageId]")]
        public Int32? EnquiryStageId
        {
            get { return Fields.EnquiryStageId[this]; }
            set { Fields.EnquiryStageId[this] = value; }
        }

        [DisplayName("Enquiry Branch Id"), Expression("jEnquiry.[BranchId]")]
        public Int32? EnquiryBranchId
        {
            get { return Fields.EnquiryBranchId[this]; }
            set { Fields.EnquiryBranchId[this] = value; }
        }

        [DisplayName("Enquiry Owner Id"), Expression("jEnquiry.[OwnerId]")]
        public Int32? EnquiryOwnerId
        {
            get { return Fields.EnquiryOwnerId[this]; }
            set { Fields.EnquiryOwnerId[this] = value; }
        }

        [DisplayName("Enquiry Assigned Id"), Expression("jEnquiry.[AssignedId]")]
        public Int32? EnquiryAssignedId
        {
            get { return Fields.EnquiryAssignedId[this]; }
            set { Fields.EnquiryAssignedId[this] = value; }
        }

        [DisplayName("Enquiry Reference Name"), Expression("jEnquiry.[ReferenceName]")]
        public String EnquiryReferenceName
        {
            get { return Fields.EnquiryReferenceName[this]; }
            set { Fields.EnquiryReferenceName[this] = value; }
        }

        [DisplayName("Enquiry Reference Phone"), Expression("jEnquiry.[ReferencePhone]")]
        public String EnquiryReferencePhone
        {
            get { return Fields.EnquiryReferencePhone[this]; }
            set { Fields.EnquiryReferencePhone[this] = value; }
        }

        [DisplayName("Enquiry Closing Type"), Expression("jEnquiry.[ClosingType]")]
        public Int32? EnquiryClosingType
        {
            get { return Fields.EnquiryClosingType[this]; }
            set { Fields.EnquiryClosingType[this] = value; }
        }

        [DisplayName("Enquiry Lost Reason"), Expression("jEnquiry.[LostReason]")]
        public String EnquiryLostReason
        {
            get { return Fields.EnquiryLostReason[this]; }
            set { Fields.EnquiryLostReason[this] = value; }
        }

        [DisplayName("Enquiry Closing Date"), Expression("jEnquiry.[ClosingDate]")]
        public DateTime? EnquiryClosingDate
        {
            get { return Fields.EnquiryClosingDate[this]; }
            set { Fields.EnquiryClosingDate[this] = value; }
        }

        [DisplayName("Enquiry Contact Person Id"), Expression("jEnquiry.[ContactPersonId]")]
        public Int32? EnquiryContactPersonId
        {
            get { return Fields.EnquiryContactPersonId[this]; }
            set { Fields.EnquiryContactPersonId[this] = value; }
        }

        [DisplayName("Enquiry Attachments"), Expression("jEnquiry.[Attachments]")]
        public String EnquiryAttachments
        {
            get { return Fields.EnquiryAttachments[this]; }
            set { Fields.EnquiryAttachments[this] = value; }
        }

        [DisplayName("Enquiry Enquiry N"), Expression("jEnquiry.[EnquiryN]")]
        public String EnquiryEnquiryN
        {
            get { return Fields.EnquiryEnquiryN[this]; }
            set { Fields.EnquiryEnquiryN[this] = value; }
        }

        [DisplayName("Enquiry Enquiry No"), Expression("jEnquiry.[EnquiryNo]")]
        public Int32? EnquiryEnquiryNo
        {
            get { return Fields.EnquiryEnquiryNo[this]; }
            set { Fields.EnquiryEnquiryNo[this] = value; }
        }

  

        public MultiRepEnquiryRow()
            : base(Fields)
        {
        }
        public MultiRepEnquiryRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field AssignedId;
            public Int32Field EnquiryId;

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

            public Int32Field EnquiryContactsId;
            public DateTimeField EnquiryDate;
            public Int32Field EnquiryStatus;
            public Int32Field EnquiryType;
            public StringField EnquiryAdditionalInfo;
            public Int32Field EnquirySourceId;
            public Int32Field EnquiryStageId;
            public Int32Field EnquiryBranchId;
            public Int32Field EnquiryOwnerId;
            public Int32Field EnquiryAssignedId;
            public StringField EnquiryReferenceName;
            public StringField EnquiryReferencePhone;
            public Int32Field EnquiryClosingType;
            public StringField EnquiryLostReason;
            public DateTimeField EnquiryClosingDate;
            public Int32Field EnquiryContactPersonId;
            public StringField EnquiryAttachments;
            public StringField EnquiryEnquiryN;
            public Int32Field EnquiryEnquiryNo;
        }
    }
}
