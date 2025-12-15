
namespace AdvanceCRM.Settings.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Settings.BizWaConfig")]
    [BasedOnRow(typeof(BizWaConfigRow), CheckNames = true)]
    public class BizWaConfigForm
    {
        public String WhatsAppNo { get; set; }
        public String PhoneNoId { get; set; }
        public String Wbaid { get; set; }
        public String Accesstoken { get; set; }
    }
}