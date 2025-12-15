
namespace AdvanceCRM.Template.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Template.InvoiceTemplate")]
    [BasedOnRow(typeof(InvoiceTemplateRow), CheckNames = true)]
    public class InvoiceTemplateForm
    {
        [Category("Email")]
        public String Sender { get; set; }
        public String Subject { get; set; }
        [HtmlContentEditor]
        public String EmailTemplate { get; set; }
        [Hint("This is optional field, you can select any file to be sent as by default as attachment while sending Quotation over email")]
        public String Attachment { get; set; }
        public String TermsConditions { get; set; }
        [Category("SMS")]
        public String SMSTemplate { get; set; }
        public String TemplateId { get; set; }
        [Category("SMS Reminder")]
        public String SmsReminder { get; set; }
        public String SmsrTemplateId { get; set; }
        [Category("WhatsApp")]
        public String WaTemplate { get; set; }
        public String WaTemplateId { get; set; }
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