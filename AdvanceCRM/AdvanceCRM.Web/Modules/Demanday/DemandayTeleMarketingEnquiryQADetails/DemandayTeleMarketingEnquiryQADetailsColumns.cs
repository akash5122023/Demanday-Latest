using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;

namespace AdvanceCRM.Demanday.Columns
{
    [ColumnsScript("Demanday.DemandayTeleMarketingEnquiryQADetails")]
    [BasedOnRow(typeof(DemandayTeleMarketingEnquiryQADetailsRow), CheckNames = true)]
    public class DemandayTeleMarketingEnquiryQADetailsColumns
    {
        [Width(300)]
        public String QuestionText { get; set; }

        [Width(300)]
        public String AnswerText { get; set; }
    }
}
