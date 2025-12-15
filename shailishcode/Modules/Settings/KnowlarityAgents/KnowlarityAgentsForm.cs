
namespace AdvanceCRM.Settings.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Settings.KnowlarityAgents")]
    [BasedOnRow(typeof(KnowlarityAgentsRow), CheckNames = true)]
    public class KnowlarityAgentsForm
    {
        //public Int32 KnowlarityId { get; set; }
        public String Name { get; set; }
        public String Number { get; set; }
    }
}