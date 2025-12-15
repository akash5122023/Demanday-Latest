
namespace AdvanceCRM.Settings.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Settings.WatiConfig")]
    [BasedOnRow(typeof(WatiConfigRow), CheckNames = true)]
    public class WatiConfigForm
    {
        public String Url { get; set; }
        public String Token { get; set; }
    }
}