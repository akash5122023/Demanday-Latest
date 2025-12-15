
namespace AdvanceCRM.Settings.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Settings.TradeIndiaConfiguration")]
    [BasedOnRow(typeof(TradeIndiaConfigurationRow), CheckNames = true)]
    public class TradeIndiaConfigurationForm
    {
        [Category("General")]
        public String Userid { get; set; }
        public String Profileid { get; set; }
        public String ApiKey { get; set; }
        [HalfWidth(UntilNext = true), BooleanSwitchEditor]
        public Boolean AutoEmail { get; set; }
        [BooleanSwitchEditor]
        public Boolean AutoSms { get; set; }
        [Category("Email Template")]
        public String Sender { get; set; }
        public String Subject { get; set; }
        [FullWidth, HtmlContentEditor]
        public String EmailTemplate { get; set; }
        [Hint("This is optional field, you can select any file to be sent as by default as attachment with email")]
        public String Attachment { get; set; }
        [Category("SMS Template"), TextAreaEditor(Rows = 5)]
        public String SMSTemplate { get; set; }
        public String SmsTemplateId { get; set; }
        [Category("WhatsApp Template"), TextAreaEditor(Rows = 5)]
        public String WaTemplate { get; set; }
        public String WaTemplateId { get; set; }
        [Category("Mail Configuration")]
        [HalfWidth, Hint("Configure this if you want to use this mail id by all users to send mail, keep blank to send mail, by users respective mail Id's")]
        public String Host { get; set; }
        [QuarterWidth]
        public Int32 Port { get; set; }
        [QuarterWidth]
        public Boolean SSL { get; set; }
        [HalfWidth]
        public String EmailId { get; set; }
        [HalfWidth, PasswordEditor]
        public String EmailPassword { get; set; }
    }
}