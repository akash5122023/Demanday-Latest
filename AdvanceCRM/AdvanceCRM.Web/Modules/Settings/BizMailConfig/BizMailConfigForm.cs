
namespace AdvanceCRM.Settings.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Settings.BizMailConfig")]
    [BasedOnRow(typeof(BizMailConfigRow), CheckNames = true)]
    public class BizMailConfigForm
    {
        [Category("Configuration")]
        public String Apiurl { get; set; }
        public String Apikey { get; set; }
        [Category("Trasactional-Mails")]
        public String FromName { get; set; }
        public String FromMail { get; set; }
        public String ReplyToName { get; set; }
        public String ReplyToMail { get; set; }
    }
}