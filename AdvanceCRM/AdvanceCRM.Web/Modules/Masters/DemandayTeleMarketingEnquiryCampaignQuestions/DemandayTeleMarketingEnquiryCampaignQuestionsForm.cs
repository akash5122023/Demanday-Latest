using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Forms
{
    [FormScript("Masters.DemandayTeleMarketingEnquiryCampaignQuestions")]
    [BasedOnRow(typeof(DemandayTeleMarketingEnquiryCampaignQuestionsRow), CheckNames = true)]
    public class DemandayTeleMarketingEnquiryCampaignQuestionsForm
    {
        public String QuestionText { get; set; }

        [LookupEditor(typeof(DemandayCampaignIdRow))]
        public Int32 CampaignId { get; set; }
        public Int32 OwnerId { get; set; }
    }
}