
namespace AdvanceCRM.Services
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Services"), TableName("[dbo].[AMCTerms]")]
    [DisplayName("AMC Terms"), InstanceName("AMC Terms")]
    [ReadPermission("AMC:Read")]
    [InsertPermission("AMC:Insert")]
    [UpdatePermission("AMC:Update")]
    [DeletePermission("AMC:Delete")]
    public sealed class AMCTermsRow : Row<AMCTermsRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Terms"), NotNull, ForeignKey("[dbo].[QuotationTermsMaster]", "Id"), LeftJoin("jTerms"), TextualField("Terms")]
        public Int32? TermsId
        {
            get { return Fields.TermsId[this]; }
            set { Fields.TermsId[this] = value; }
        }

        [DisplayName("AMC"), Column("AMCId"), NotNull, ForeignKey("[dbo].[AMC]", "Id"), LeftJoin("jAMC"), TextualField("AMCAdditionalInfo")]
        public Int32? AMCId
        {
            get { return Fields.AMCId[this]; }
            set { Fields.AMCId[this] = value; }
        }

        [DisplayName("Terms"), Expression("jTerms.[Terms]")]
        public String Terms
        {
            get { return Fields.Terms[this]; }
            set { Fields.Terms[this] = value; }
        }

        [DisplayName("AMC Date"), Expression("jAMC.[Date]")]
        public DateTime? AMCDate
        {
            get { return Fields.AMCDate[this]; }
            set { Fields.AMCDate[this] = value; }
        }

        [DisplayName("AMC Contacts Id"), Expression("jAMC.[ContactsId]")]
        public Int32? AMCContactsId
        {
            get { return Fields.AMCContactsId[this]; }
            set { Fields.AMCContactsId[this] = value; }
        }

        [DisplayName("AMC Status"), Expression("jAMC.[Status]")]
        public Int32? AMCStatus
        {
            get { return Fields.AMCStatus[this]; }
            set { Fields.AMCStatus[this] = value; }
        }

        [DisplayName("AMC Start Date"), Expression("jAMC.[StartDate]")]
        public DateTime? AMCStartDate
        {
            get { return Fields.AMCStartDate[this]; }
            set { Fields.AMCStartDate[this] = value; }
        }

        [DisplayName("AMC End Date"), Expression("jAMC.[EndDate]")]
        public DateTime? AMCEndDate
        {
            get { return Fields.AMCEndDate[this]; }
            set { Fields.AMCEndDate[this] = value; }
        }

        [DisplayName("AMC Additional Info"), Expression("jAMC.[AdditionalInfo]")]
        public String AMCAdditionalInfo
        {
            get { return Fields.AMCAdditionalInfo[this]; }
            set { Fields.AMCAdditionalInfo[this] = value; }
        }

        [DisplayName("AMC Owner Id"), Expression("jAMC.[OwnerId]")]
        public Int32? AMCOwnerId
        {
            get { return Fields.AMCOwnerId[this]; }
            set { Fields.AMCOwnerId[this] = value; }
        }

        [DisplayName("AMC Assigned Id"), Expression("jAMC.[AssignedId]")]
        public Int32? AMCAssignedId
        {
            get { return Fields.AMCAssignedId[this]; }
            set { Fields.AMCAssignedId[this] = value; }
        }

        [DisplayName("AMC Attachment"), Expression("jAMC.[Attachment]")]
        public String AMCAttachment
        {
            get { return Fields.AMCAttachment[this]; }
            set { Fields.AMCAttachment[this] = value; }
        }

        [DisplayName("AMC Lines"), Expression("jAMC.[Lines]")]
        public Int32? AMCLines
        {
            get { return Fields.AMCLines[this]; }
            set { Fields.AMCLines[this] = value; }
        }

        [DisplayName("AMC Terms"), Expression("jAMC.[Terms]")]
        public String AMCTerms
        {
            get { return Fields.AMCTerms[this]; }
            set { Fields.AMCTerms[this] = value; }
        }

     
        public AMCTermsRow()
            : base(Fields)
        {
        }
        public AMCTermsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field TermsId;
            public Int32Field AMCId;

            public StringField Terms;

            public DateTimeField AMCDate;
            public Int32Field AMCContactsId;
            public Int32Field AMCStatus;
            public DateTimeField AMCStartDate;
            public DateTimeField AMCEndDate;
            public StringField AMCAdditionalInfo;
            public Int32Field AMCOwnerId;
            public Int32Field AMCAssignedId;
            public StringField AMCAttachment;
            
            public Int32Field AMCLines;
            public StringField AMCTerms;
        }
    }
}
