using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Settings.Forms
{
    [FormScript("Settings.AiConfiguration")]
    [BasedOnRow(typeof(AiConfigurationRow), CheckNames = true)]
    public class AiConfigurationForm
    {
        public String AiKey { get; set; }
    }
}