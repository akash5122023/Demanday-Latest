using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Forms
{
    [FormScript("Masters.DemandayTeleMarketingEnquiryQuestionAnswers")]
    [BasedOnRow(typeof(DemandayTeleMarketingEnquiryQuestionAnswersRow), CheckNames = true)]
    public class DemandayTeleMarketingEnquiryQuestionAnswersForm
    {
        [LookupEditor(typeof(DemandayTeleMarketingEnquiryCampaignQuestionsRow))]
        public Int32 QuestionId { get; set; }
        public String AnswerText { get; set; }
        public Int32 OwnerId { get; set; }
    }
}