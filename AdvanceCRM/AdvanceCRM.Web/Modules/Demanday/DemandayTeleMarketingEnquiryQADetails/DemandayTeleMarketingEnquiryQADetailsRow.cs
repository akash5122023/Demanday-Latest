using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace AdvanceCRM.Demanday
{
    [ConnectionKey("Default"), Module("Demanday"), TableName("[dbo].[DemandayTeleMarketingEnquiryQADetails]")]
    [DisplayName("Demanday TeleMarketing Enquiry QA Details"), InstanceName("Demanday TeleMarketing Enquiry QA Detail")]
    [ReadPermission("DemandayTeleMarketingEnquiry:Read")]
    [InsertPermission("DemandayTeleMarketingEnquiry:Insert")]
    [UpdatePermission("DemandayTeleMarketingEnquiry:Update")]
    [DeletePermission("DemandayTeleMarketingEnquiry:Delete")]
    public sealed class DemandayTeleMarketingEnquiryQADetailsRow : Row<DemandayTeleMarketingEnquiryQADetailsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Enquiry"), NotNull]
        public Int32? EnquiryId
        {
            get => fields.EnquiryId[this];
            set => fields.EnquiryId[this] = value;
        }

        [DisplayName("Question"), ForeignKey("[dbo].[DemandayTeleMarketingEnquiryCampaignQuestions]", "Id"), LeftJoin("jQuestion"),
            TextualField("QuestionText"), LookupInclude]
        [LookupEditor(typeof(Masters.DemandayTeleMarketingEnquiryCampaignQuestionsRow))]
        public Int32? QuestionId
        {
            get => fields.QuestionId[this];
            set => fields.QuestionId[this] = value;
        }

        [DisplayName("Answer"), ForeignKey("[dbo].[DemandayTeleMarketingEnquiryQuestionAnswers]", "Id"), LeftJoin("jAnswer"),
            TextualField("AnswerText"), LookupInclude]
        [LookupEditor(typeof(Masters.DemandayTeleMarketingEnquiryQuestionAnswersRow), CascadeFrom = "QuestionId", CascadeField = "QuestionId")]
        public Int32? AnswerId
        {
            get => fields.AnswerId[this];
            set => fields.AnswerId[this] = value;
        }

        [DisplayName("Question Text"), Expression("jQuestion.[QuestionText]"), NameProperty]
        public String QuestionText
        {
            get => fields.QuestionText[this];
            set => fields.QuestionText[this] = value;
        }

        [DisplayName("Answer Text"), Expression("jAnswer.[AnswerText]")]
        public String AnswerText
        {
            get => fields.AnswerText[this];
            set => fields.AnswerText[this] = value;
        }

        [DisplayName("Campaign"), NotMapped]
        public String CampaignId
        {
            get => fields.CampaignId[this];
            set => fields.CampaignId[this] = value;
        }

        public DemandayTeleMarketingEnquiryQADetailsRow()
            : base()
        {
        }

        public DemandayTeleMarketingEnquiryQADetailsRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field EnquiryId;
            public Int32Field QuestionId;
            public Int32Field AnswerId;
            public StringField QuestionText;
            public StringField AnswerText;
            public StringField CampaignId;
        }
    }
}
