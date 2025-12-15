
namespace AdvanceCRM.Settings.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Settings.MailInbox")]
    [BasedOnRow(typeof(MailInboxRow), CheckNames = true)]
    public class MailInboxForm
    {
        [Category("Email Configuration")]
        public String Host { get; set; }
        public Int32 Port { get; set; }
        public Boolean Ssl { get; set; }
        public String EmailId { get; set; }
        [PasswordEditor]
        public String EmailPassword { get; set; }
        [BooleanSwitchEditor]
        public Boolean AutoEmail { get; set; }

        [Category("Email Template")]
        public String Sender { get; set; }
        public String Subject { get; set; }
        [FullWidth, HtmlContentEditor]
        public String EmailTemplate { get; set; }
        [Hint("This is optional field, you can select any file to be sent as by default as attachment with email")]
        public String Attachment { get; set; }

        [Category("Mail Configuration")]
        [HalfWidth, Hint("Configure this if you want to use this mail id by all users to send mail, keep blank to send mail, by users respective mail Id's")]

         public String SHost { get; set; }
        [QuarterWidth]
        public Int32 SPort { get; set; }
        [QuarterWidth]
        public Boolean Sssl { get; set; }
        [HalfWidth]
        public String SEmailId { get; set; }
        [HalfWidth, PasswordEditor]
        public String SEmailPassword { get; set; }
    }
}