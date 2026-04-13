using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Columns
{
    [ColumnsScript("Masters.DemandayTeleMarketingEnquiryQuestionAnswers")]
    [BasedOnRow(typeof(DemandayTeleMarketingEnquiryQuestionAnswersRow), CheckNames = true)]
    public class DemandayTeleMarketingEnquiryQuestionAnswersColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public String QuestionQuestionText { get; set; }
        [EditLink]
        public String AnswerText { get; set; }
        //public String OwnerUsername { get; set; }
    }
}