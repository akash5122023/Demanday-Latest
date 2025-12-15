
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.KnowlarityIvr")]
    [BasedOnRow(typeof(KnowlarityIvrRow), CheckNames = true)]
    public class KnowlarityIvrForm
    {
        public String Name { get; set; }
        public String Mobile { get; set; }
        public String EmpMobile { get; set; }
        public String IvrNo { get; set; }
        public String Recording { get; set; }
        public String Date { get; set; }
        public String Duration { get; set; }
    }
}