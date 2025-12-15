
namespace AdvanceCRM.Quotation
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Quotation"), TableName("[dbo].[MultiRepQuotation]")]
    [DisplayName("Multi Rep Quotation"), InstanceName("Multi Rep Quotation")]
    [ReadPermission("Quotation:Read")]
    [InsertPermission("Quotation:Insert")]
    [UpdatePermission("Quotation:Update")]
    [DeletePermission("Quotation:Delete")]
    public sealed class MultiRepQuotationRow : Row<MultiRepQuotationRow.RowFields>, IIdRow
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

        [DisplayName("Quotation"), NotNull, ForeignKey("[dbo].[Quotation]", "Id"), LeftJoin("jQuotation"), TextualField("QuotationAdditionalInfo")]
        public Int32? QuotationId
        {
            get { return Fields.QuotationId[this]; }
            set { Fields.QuotationId[this] = value; }
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

        [DisplayName("Quotation Contacts Id"), Expression("jQuotation.[ContactsId]")]
        public Int32? QuotationContactsId
        {
            get { return Fields.QuotationContactsId[this]; }
            set { Fields.QuotationContactsId[this] = value; }
        }

        [DisplayName("Quotation Date"), Expression("jQuotation.[Date]")]
        public DateTime? QuotationDate
        {
            get { return Fields.QuotationDate[this]; }
            set { Fields.QuotationDate[this] = value; }
        }

        [DisplayName("Quotation Status"), Expression("jQuotation.[Status]")]
        public Int32? QuotationStatus
        {
            get { return Fields.QuotationStatus[this]; }
            set { Fields.QuotationStatus[this] = value; }
        }

        [DisplayName("Quotation Type"), Expression("jQuotation.[Type]")]
        public Int32? QuotationType
        {
            get { return Fields.QuotationType[this]; }
            set { Fields.QuotationType[this] = value; }
        }

        [DisplayName("Quotation Additional Info"), Expression("jQuotation.[AdditionalInfo]")]
        public String QuotationAdditionalInfo
        {
            get { return Fields.QuotationAdditionalInfo[this]; }
            set { Fields.QuotationAdditionalInfo[this] = value; }
        }

        [DisplayName("Quotation Source Id"), Expression("jQuotation.[SourceId]")]
        public Int32? QuotationSourceId
        {
            get { return Fields.QuotationSourceId[this]; }
            set { Fields.QuotationSourceId[this] = value; }
        }

        [DisplayName("Quotation Stage Id"), Expression("jQuotation.[StageId]")]
        public Int32? QuotationStageId
        {
            get { return Fields.QuotationStageId[this]; }
            set { Fields.QuotationStageId[this] = value; }
        }

        [DisplayName("Quotation Branch Id"), Expression("jQuotation.[BranchId]")]
        public Int32? QuotationBranchId
        {
            get { return Fields.QuotationBranchId[this]; }
            set { Fields.QuotationBranchId[this] = value; }
        }

        [DisplayName("Quotation Owner Id"), Expression("jQuotation.[OwnerId]")]
        public Int32? QuotationOwnerId
        {
            get { return Fields.QuotationOwnerId[this]; }
            set { Fields.QuotationOwnerId[this] = value; }
        }

        [DisplayName("Quotation Assigned Id"), Expression("jQuotation.[AssignedId]")]
        public Int32? QuotationAssignedId
        {
            get { return Fields.QuotationAssignedId[this]; }
            set { Fields.QuotationAssignedId[this] = value; }
        }

        [DisplayName("Quotation Reference Name"), Expression("jQuotation.[ReferenceName]")]
        public String QuotationReferenceName
        {
            get { return Fields.QuotationReferenceName[this]; }
            set { Fields.QuotationReferenceName[this] = value; }
        }

        [DisplayName("Quotation Reference Phone"), Expression("jQuotation.[ReferencePhone]")]
        public String QuotationReferencePhone
        {
            get { return Fields.QuotationReferencePhone[this]; }
            set { Fields.QuotationReferencePhone[this] = value; }
        }

        [DisplayName("Quotation Closing Type"), Expression("jQuotation.[ClosingType]")]
        public Int32? QuotationClosingType
        {
            get { return Fields.QuotationClosingType[this]; }
            set { Fields.QuotationClosingType[this] = value; }
        }

        [DisplayName("Quotation Lost Reason"), Expression("jQuotation.[LostReason]")]
        public String QuotationLostReason
        {
            get { return Fields.QuotationLostReason[this]; }
            set { Fields.QuotationLostReason[this] = value; }
        }

        [DisplayName("Quotation Subject"), Expression("jQuotation.[Subject]")]
        public String QuotationSubject
        {
            get { return Fields.QuotationSubject[this]; }
            set { Fields.QuotationSubject[this] = value; }
        }

        [DisplayName("Quotation Reference"), Expression("jQuotation.[Reference]")]
        public String QuotationReference
        {
            get { return Fields.QuotationReference[this]; }
            set { Fields.QuotationReference[this] = value; }
        }

        [DisplayName("Quotation Attachment"), Expression("jQuotation.[Attachment]")]
        public String QuotationAttachment
        {
            get { return Fields.QuotationAttachment[this]; }
            set { Fields.QuotationAttachment[this] = value; }
        }

        [DisplayName("Quotation Lines"), Expression("jQuotation.[Lines]")]
        public Int32? QuotationLines
        {
            get { return Fields.QuotationLines[this]; }
            set { Fields.QuotationLines[this] = value; }
        }

        [DisplayName("Quotation Contact Person Id"), Expression("jQuotation.[ContactPersonId]")]
        public Int32? QuotationContactPersonId
        {
            get { return Fields.QuotationContactPersonId[this]; }
            set { Fields.QuotationContactPersonId[this] = value; }
        }

        [DisplayName("Quotation Closing Date"), Expression("jQuotation.[ClosingDate]")]
        public DateTime? QuotationClosingDate
        {
            get { return Fields.QuotationClosingDate[this]; }
            set { Fields.QuotationClosingDate[this] = value; }
        }

        [DisplayName("Quotation Enquiry No"), Expression("jQuotation.[EnquiryNo]")]
        public Int32? QuotationEnquiryNo
        {
            get { return Fields.QuotationEnquiryNo[this]; }
            set { Fields.QuotationEnquiryNo[this] = value; }
        }

        [DisplayName("Quotation Enquiry Date"), Expression("jQuotation.[EnquiryDate]")]
        public DateTime? QuotationEnquiryDate
        {
            get { return Fields.QuotationEnquiryDate[this]; }
            set { Fields.QuotationEnquiryDate[this] = value; }
        }

        [DisplayName("Quotation Conversion"), Expression("jQuotation.[Conversion]")]
        public Double? QuotationConversion
        {
            get { return Fields.QuotationConversion[this]; }
            set { Fields.QuotationConversion[this] = value; }
        }

        [DisplayName("Quotation Currency Conversion"), Expression("jQuotation.[CurrencyConversion]")]
        public Boolean? QuotationCurrencyConversion
        {
            get { return Fields.QuotationCurrencyConversion[this]; }
            set { Fields.QuotationCurrencyConversion[this] = value; }
        }

        [DisplayName("Quotation From Currency"), Expression("jQuotation.[FromCurrency]")]
        public Int32? QuotationFromCurrency
        {
            get { return Fields.QuotationFromCurrency[this]; }
            set { Fields.QuotationFromCurrency[this] = value; }
        }

        [DisplayName("Quotation To Currency"), Expression("jQuotation.[ToCurrency]")]
        public Int32? QuotationToCurrency
        {
            get { return Fields.QuotationToCurrency[this]; }
            set { Fields.QuotationToCurrency[this] = value; }
        }

        [DisplayName("Quotation Taxable"), Expression("jQuotation.[Taxable]")]
        public Boolean? QuotationTaxable
        {
            get { return Fields.QuotationTaxable[this]; }
            set { Fields.QuotationTaxable[this] = value; }
        }

        [DisplayName("Quotation Quotation No"), Expression("jQuotation.[QuotationNo]")]
        public Int32? QuotationQuotationNo
        {
            get { return Fields.QuotationQuotationNo[this]; }
            set { Fields.QuotationQuotationNo[this] = value; }
        }

        [DisplayName("Quotation Roundup"), Expression("jQuotation.[Roundup]")]
        public Double? QuotationRoundup
        {
            get { return Fields.QuotationRoundup[this]; }
            set { Fields.QuotationRoundup[this] = value; }
        }

        [DisplayName("Quotation Message Id"), Expression("jQuotation.[MessageId]")]
        public Int32? QuotationMessageId
        {
            get { return Fields.QuotationMessageId[this]; }
            set { Fields.QuotationMessageId[this] = value; }
        }

        [DisplayName("Quotation Quotation N"), Expression("jQuotation.[QuotationN]")]
        public String QuotationQuotationN
        {
            get { return Fields.QuotationQuotationN[this]; }
            set { Fields.QuotationQuotationN[this] = value; }
        }

      

        public MultiRepQuotationRow()
            : base(Fields)
        {
        }
         public MultiRepQuotationRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field AssignedId;
            public Int32Field QuotationId;

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

            public Int32Field QuotationContactsId;
            public DateTimeField QuotationDate;
            public Int32Field QuotationStatus;
            public Int32Field QuotationType;
            public StringField QuotationAdditionalInfo;
            public Int32Field QuotationSourceId;
            public Int32Field QuotationStageId;
            public Int32Field QuotationBranchId;
            public Int32Field QuotationOwnerId;
            public Int32Field QuotationAssignedId;
            public StringField QuotationReferenceName;
            public StringField QuotationReferencePhone;
            public Int32Field QuotationClosingType;
            public StringField QuotationLostReason;
            public StringField QuotationSubject;
            public StringField QuotationReference;
            public StringField QuotationAttachment;
            public Int32Field QuotationLines;
            public Int32Field QuotationContactPersonId;
            public DateTimeField QuotationClosingDate;
            public Int32Field QuotationEnquiryNo;
            public DateTimeField QuotationEnquiryDate;
            public DoubleField QuotationConversion;
            public BooleanField QuotationCurrencyConversion;
            public Int32Field QuotationFromCurrency;
            public Int32Field QuotationToCurrency;
            public BooleanField QuotationTaxable;
            public Int32Field QuotationQuotationNo;
            public DoubleField QuotationRoundup;
            public Int32Field QuotationMessageId;
            public StringField QuotationQuotationN;
        }
    }
}
