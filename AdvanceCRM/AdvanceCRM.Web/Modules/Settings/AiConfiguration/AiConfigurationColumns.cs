using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Settings.Columns
{
    [ColumnsScript("Settings.AiConfiguration")]
    [BasedOnRow(typeof(AiConfigurationRow), CheckNames = true)]
    public class AiConfigurationColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String AiKey { get; set; }
    }
}