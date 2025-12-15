
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.KnowlarityDetails")]
    [BasedOnRow(typeof(KnowlarityDetailsRow), CheckNames = true)]
    public class KnowlarityDetailsForm
    {
        public String Name { get; set; }
        public String CustomerNumber { get; set; }
        public String EmployeeName { get; set; }
        public String Cmiuid { get; set; }
        public String BilledSec { get; set; }
        public String Rate { get; set; }
        public String Record { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public String Type { get; set; }
        public Int32 CompanyType { get; set; }
        public String Email { get; set; }
        public String EmployeeNumber { get; set; }
        public String Duration { get; set; }        
        public String Recording { get; set; }
        [DateTimeEditor]
        public DateTime DateTime { get; set; }
        public Boolean IsMoved { get; set; }

    }
}