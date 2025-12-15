
namespace AdvanceCRM.Template.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Template.ChallanTemplate")]
    [BasedOnRow(typeof(ChallanTemplateRow), CheckNames = true)]
    public class ChallanTemplateForm
    {
        [Category("Email")]
        public String Sender { get; set; }
        public String Subject { get; set; }
        [HtmlContentEditor]
        public String EmailTemplate { get; set; }
        public String TermsConditions { get; set; }
        [Category("SMS")]
        public String SMSTemplate { get; set; }
        public String TemplateId { get; set; }
        [Category("WhatsApp")]
        public String WaTemplate { get; set; }
        public String WaTemplateId { get; set; }
        [Category("CC's")]
        public String CCEmails { get; set; }
        public String Bcc { get; set; }
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