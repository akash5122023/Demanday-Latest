
namespace AdvanceCRM.Enquiry
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Enquiry"), TableName("[dbo].[EnquiryMultiInfo]")]
    [DisplayName("Enquiry Multi Info"), InstanceName("Enquiry Multi Info")]
    [ReadPermission("Enquiry:Read")]
    [InsertPermission("Enquiry:Insert")]
    [UpdatePermission("Enquiry:Update")]
    [DeletePermission("Enquiry:Delete")]
    public sealed class EnquiryMultiInfoRow : Row<EnquiryMultiInfoRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Additional Info"), NotNull, ForeignKey("[dbo].[AdditionalInfo]", "Id"), LeftJoin("jAdditionalInfo"), TextualField("AdditionalInfo")]
        public Int32? AdditionalInfoId
        {
            get { return Fields.AdditionalInfoId[this]; }
            set { Fields.AdditionalInfoId[this] = value; }
        }

        [DisplayName("Enquiry"), NotNull, ForeignKey("[dbo].[Enquiry]", "Id"), LeftJoin("jEnquiry"), TextualField("EnquiryAdditionalInfo")]
        public Int32? EnquiryId
        {
            get { return Fields.EnquiryId[this]; }
            set { Fields.EnquiryId[this] = value; }
        }

        [DisplayName("Additional Info"), Expression("jAdditionalInfo.[AdditionalInfo]")]
        public String AdditionalInfo
        {
            get { return Fields.AdditionalInfo[this]; }
            set { Fields.AdditionalInfo[this] = value; }
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

        [DisplayName("Enquiry Company Id"), Expression("jEnquiry.[CompanyId]")]
        public Int32? EnquiryCompanyId
        {
            get { return Fields.EnquiryCompanyId[this]; }
            set { Fields.EnquiryCompanyId[this] = value; }
        }

        [DisplayName("Enquiry Additional Info2"), Expression("jEnquiry.[AdditionalInfo2]")]
        public String EnquiryAdditionalInfo2
        {
            get { return Fields.EnquiryAdditionalInfo2[this]; }
            set { Fields.EnquiryAdditionalInfo2[this] = value; }
        }

      

        public EnquiryMultiInfoRow()
            : base(Fields)
        {
        }
        
        public EnquiryMultiInfoRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field AdditionalInfoId;
            public Int32Field EnquiryId;

            public StringField AdditionalInfo;

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
            public Int32Field EnquiryCompanyId;
            public StringField EnquiryAdditionalInfo2;
        }
    }
}
