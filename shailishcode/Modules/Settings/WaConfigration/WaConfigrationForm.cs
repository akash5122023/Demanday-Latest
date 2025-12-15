
namespace AdvanceCRM.Settings.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Settings.WaConfigration")]
    [BasedOnRow(typeof(WaConfigrationRow), CheckNames = true)]
    public class WaConfigrationForm
    {
        public String Mobile { get; set; }
        public String ApiKey { get; set; }
        public String MessageApi { get; set; }
        public String MediaApi { get; set; }
        public String SuccessResponse { get; set; }
    }
}