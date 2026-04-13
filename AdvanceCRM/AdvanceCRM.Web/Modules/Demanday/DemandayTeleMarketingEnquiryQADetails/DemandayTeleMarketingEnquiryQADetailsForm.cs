using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;

namespace AdvanceCRM.Demanday.Forms
{
    [FormScript("Demanday.DemandayTeleMarketingEnquiryQADetails")]
    [BasedOnRow(typeof(DemandayTeleMarketingEnquiryQADetailsRow), CheckNames = true)]
    public class DemandayTeleMarketingEnquiryQADetailsForm
    {
        [HideOnInsert, HideOnUpdate, Hidden]
        public String CampaignId { get; set; }

        [LookupEditor(typeof(Masters.DemandayTeleMarketingEnquiryCampaignQuestionsRow), CascadeFrom = "CampaignId", CascadeField = "CampaignCampaignId")]
        public Int32 QuestionId { get; set; }

        [LookupEditor(typeof(Masters.DemandayTeleMarketingEnquiryQuestionAnswersRow), CascadeFrom = "QuestionId", CascadeField = "QuestionId")]
        public Int32 AnswerId { get; set; }
    }
}
