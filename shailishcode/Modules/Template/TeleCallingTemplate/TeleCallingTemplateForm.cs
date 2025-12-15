
namespace AdvanceCRM.Template.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Template.TeleCallingTemplate")]
    [BasedOnRow(typeof(TeleCallingTemplateRow), CheckNames = true)]
    public class TeleCallingTemplateForm
    {
        [Category("Email")]
        public String From { get; set; }
        public String Subject { get; set; }
        [HtmlContentEditor]
        public String CustomerEmail { get; set; }
        [HtmlContentEditor]
        public String ExecutiveEmail { get; set; }
        [Category("SMS")]
        public String CustomerSms { get; set; }
        public String CustTemplateId { get; set; }
        public String ExecutiveSms { get; set; }
        public String ExeTemplateId { get; set; }
        public String CustomerReminderSMS { get; set; }
        public String CustRTemplateId { get; set; }
        public String ExecutiveReminderSMS { get; set; }
        public String ExeRTemplateId { get; set; }
        [Category("SMS Reminder")]
        public String SmsReminder { get; set; }
        public String SmsrTemplateId { get; set; }
        
        [Category("WhatsApp")]
        public String WaCustomTemplate { get; set; }
        public String WaCustomTemplateId { get; set; }
        public String WaExeTemplate { get; set; }
        public String WaExeTemplateId { get; set; }
        public String RwaCustomTemplate { get; set; }
        public String RwaCustomTemplateId { get; set; }
        public String RwaExeTemplate { get; set; }
        public String RwaExeTemplateId { get; set; }
        [Category("WhatsApp Reminder")]
        public String WaReminder { get; set; }
        public String WarTemplateId { get; set; }
    }
}