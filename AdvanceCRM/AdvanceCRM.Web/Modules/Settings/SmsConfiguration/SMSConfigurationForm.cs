
namespace AdvanceCRM.Settings.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Settings.SMSConfiguration")]
    [BasedOnRow(typeof(SMSConfigurationRow), CheckNames = true)]
    public class SMSConfigurationForm
    {
        public String API { get; set; }
        public String BulkAPI { get; set; }
        public String ScheduleAPI { get; set; }
        public String SuccessResponse { get; set; }
        [HalfWidth(UntilNext = true)]
        public String Username { get; set; }
        public String Password { get; set; }
        public String SenderId { get; set; }
        public String Key { get; set; }
    }
}