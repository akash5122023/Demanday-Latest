
namespace AdvanceCRM.Quotation
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Quotation"), TableName("[dbo].[QuotationMultiInfo]")]
    [DisplayName("Quotation Multi Info"), InstanceName("Quotation Multi Info")]
    [ReadPermission("Quotation:Read")]
    [InsertPermission("Quotation:Insert")]
    [UpdatePermission("Quotation:Update")]
    [DeletePermission("Quotation:Delete")]
    public sealed class QuotationMultiInfoRow : Row<QuotationMultiInfoRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity]
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

        [DisplayName("Quotation"), NotNull, ForeignKey("[dbo].[Quotation]", "Id"), LeftJoin("jQuotation"), TextualField("QuotationAdditionalInfo")]
        public Int32? QuotationId
        {
            get { return Fields.QuotationId[this]; }
            set { Fields.QuotationId[this] = value; }
        }

        [DisplayName("Additional Info"), Expression("jAdditionalInfo.[AdditionalInfo]")]
        public String AdditionalInfo
        {
            get { return Fields.AdditionalInfo[this]; }
            set { Fields.AdditionalInfo[this] = value; }
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

        [DisplayName("Quotation Company Id"), Expression("jQuotation.[CompanyId]")]
        public Int32? QuotationCompanyId
        {
            get { return Fields.QuotationCompanyId[this]; }
            set { Fields.QuotationCompanyId[this] = value; }
        }

        [DisplayName("Quotation Enquiry N"), Expression("jQuotation.[EnquiryN]")]
        public String QuotationEnquiryN
        {
            get { return Fields.QuotationEnquiryN[this]; }
            set { Fields.QuotationEnquiryN[this] = value; }
        }

        [DisplayName("Quotation Additional Info2"), Expression("jQuotation.[AdditionalInfo2]")]
        public String QuotationAdditionalInfo2
        {
            get { return Fields.QuotationAdditionalInfo2[this]; }
            set { Fields.QuotationAdditionalInfo2[this] = value; }
        }

        public QuotationMultiInfoRow()
            : base(Fields)
        {
        }
        public QuotationMultiInfoRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field AdditionalInfoId;
            public Int32Field QuotationId;

            public StringField AdditionalInfo;

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
            public Int32Field QuotationCompanyId;
            public StringField QuotationEnquiryN;
            public StringField QuotationAdditionalInfo2;
        }
    }
}
