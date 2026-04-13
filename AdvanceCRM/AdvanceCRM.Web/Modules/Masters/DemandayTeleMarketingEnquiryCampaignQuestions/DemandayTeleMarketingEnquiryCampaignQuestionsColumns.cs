using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Columns
{
    [ColumnsScript("Masters.DemandayTeleMarketingEnquiryCampaignQuestions")]
    [BasedOnRow(typeof(DemandayTeleMarketingEnquiryCampaignQuestionsRow), CheckNames = true)]
    public class DemandayTeleMarketingEnquiryCampaignQuestionsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String QuestionText { get; set; }
        public String CampaignCampaignId { get; set; }
        //public String OwnerUsername { get; set; }
    }
}