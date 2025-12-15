
namespace AdvanceCRM.Template.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Template.CmsTemplate")]
    [BasedOnRow(typeof(CmsTemplateRow), CheckNames = true)]
    public class CmsTemplateForm
    {
        [Category("Email")]
        public String Sender { get; set; }
        public String Subject { get; set; }
        [HtmlContentEditor]
        public String EmailTemplate { get; set; }
        [HtmlContentEditor]
        public String EmailTemplateReceipt { get; set; }
        [HtmlContentEditor]
        public String ClosedEmailTemplate { get; set; }
        [HtmlContentEditor]
        public String EngineerEmailTemplate { get; set; }
        [Category("SMS")]
        public String SMSTemplate { get; set; }
        public String SmsTemplateId { get; set; }
        public String ClosedSMSTemplate { get; set; }
        public String ClosedTemplateId { get; set; }
        public String EngineerSMSTemplate { get; set; }
        public String EmgineerTemplateId { get; set; }
        [Category("SMS Reminder")]
        public String SmsReminder { get; set; }
        public String SmsrTemplateId { get; set; }
        [Category("WhatsApp")]
        public String WaTemplate { get; set; }
        public String WaTemplateId { get; set; }
        
        public String WaClosedTemplate { get; set; }
        public String WaClosedTemplateId { get; set; }
        public String WaengTemplate { get; set; }
        public String WaengTemplateId { get; set; }
        [Category("WhatsApp Reminder")]
        public String WaReminder { get; set; }
        public String WarTemplateId { get; set; }

        [Category("CC's")]
        public String CCEmails { get; set; }
        public String BCCEmails { get; set; }
        public String CcsmSs { get; set; }

        [Category("Mail Configuration")]
        [HalfWidth, Hint("Configure this if you want to use this mail id by all users to send mail, keep blank to send mail, by users respective mail Id's")]
        public String Host { get; set; }
        [QuarterWidth]
        public Int32 Port { get; set; }
        [QuarterWidth]
        public Boolean SSL { get; set; }
        [HalfWidth]
        public String EmailId { get; set; }
        [HalfWidth]
        public String EmailPassword { get; set; }
    }
}