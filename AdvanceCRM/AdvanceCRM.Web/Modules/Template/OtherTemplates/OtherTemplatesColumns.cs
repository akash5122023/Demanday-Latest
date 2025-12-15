
namespace AdvanceCRM.Template.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Template.OtherTemplates")]
    [BasedOnRow(typeof(OtherTemplatesRow), CheckNames = true)]
    public class OtherTemplatesColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String TicketSmsTemplate { get; set; }
        public String TicketSmsTemplateId { get; set; }
        public String TaskSmsTemplate { get; set; }
        public String TaskSmsTemplateId { get; set; }
        public String FeedbackSmsTemplate { get; set; }
        public String FeedbackSmsTemplateId { get; set; }
        public String FeedbackWaTemplate { get; set; }
        public String FeedbackSwaTemplateId { get; set; }
        public String TicketWaTemplate { get; set; }
        public String TicketWaTemplateId { get; set; }
        public String TaskWaTemplate { get; set; }
        public String TaskWaTemplateId { get; set; }
        public String OtpsmsTemplate { get; set; }
        public String OtpsmsTemplateId { get; set; }
        public String OtpwaTemplate { get; set; }
        public String OtpwaTemplateId { get; set; }
    }
}