
namespace AdvanceCRM.Premium.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Premium.TargetSetting")]
    [BasedOnRow(typeof(TargetSettingRow), CheckNames = true)]
    public class TargetSettingForm
    {
        public Int32 Type { get; set; }
        public Int32 MonthlyTarget { get; set; }
        public Double MonthlyTargetAmount { get; set; }
        public Int32 Representative { get; set; }
    }
}