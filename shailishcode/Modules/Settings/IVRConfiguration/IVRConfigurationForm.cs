
namespace AdvanceCRM.Settings.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using AdvanceCRM.Settings;

    [FormScript("Settings.IVRConfiguration")]
    [BasedOnRow(typeof(IVRConfigurationRow), CheckNames = true)]
    public class IVRConfigurationForm
    {
        public Int32 IVRType { get; set; }
        [ReadOnly(true)]
        public String IVRNumber { get; set; }
        public String ApiKey { get; set; }
        public String Plan { get; set; }
        public String AppId { get; set; }
        public String AppSecret { get; set; }
        public String Token_Id { get; set; }
        public String userType { get; set; }
        public String Number { get; set; }
        public String CliNumber { get; set; }
        public String Username { get; set; }
        [PasswordEditor]
        public String Password { get; set; }
        [Hint("API With JSON Format")]
        [ReadOnly(true), TextAreaEditor(Rows = 6)]
        public String PostUrl { get; set; }
        [Category("Agents")]
        public List<KnowlarityAgentsRow> Agents { get; set; }

        [Category("Settings")]
        [HalfWidth(UntilNext = true), BooleanSwitchEditor]
        public Boolean AutoEmail { get; set; }
        [BooleanSwitchEditor]
        public Boolean RoundRobin { get; set; }
        [BooleanSwitchEditor]
        public Boolean AutoRefresh { get; set; }
        public Int32 AutoRefreshTime { get; set; }

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
        public String SmsTemplate { get; set; }
        public String TemplateId { get; set; }
        [Category("WhatsApp Template"), TextAreaEditor(Rows = 5)]
        public String WaTemplate { get; set; }
        public String WaTemplateId { get; set; }
        [Category("Mail Configuration")]
        [HalfWidth, Hint("Configure this if you want to use this mail id by all users to send mail, keep blank to send mail, by users respective mail Id's")]
        public String Host { get; set; }
        [QuarterWidth]
        public Int32 Port { get; set; }
        [QuarterWidth]
        public Boolean Ssl { get; set; }
        [HalfWidth]
        public String EmailId { get; set; }
        [HalfWidth]
        public String EmailPassword { get; set; }
    }
}