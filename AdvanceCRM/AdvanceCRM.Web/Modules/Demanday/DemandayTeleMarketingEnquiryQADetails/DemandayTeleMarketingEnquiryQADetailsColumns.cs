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
        public String CampaignId { get; set; }

        [EditLink, Width(300)]
        public String QuestionText { get; set; }

        [EditLink, Width(300)]
        public String AnswerText { get; set; }
    }
}
