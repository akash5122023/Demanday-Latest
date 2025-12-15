
namespace AdvanceCRM.Settings.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Settings.KnowlarityAgents")]
    [BasedOnRow(typeof(KnowlarityAgentsRow), CheckNames = true)]
    public class KnowlarityAgentsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        //public String KnowlarityIvrNumber { get; set; }
        [EditLink]
        public String Name { get; set; }
        public String Number { get; set; }
    }
}