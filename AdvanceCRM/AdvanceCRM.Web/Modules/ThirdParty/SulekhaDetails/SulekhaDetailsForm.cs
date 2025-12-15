
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.SulekhaDetails")]
    [BasedOnRow(typeof(SulekhaDetailsRow), CheckNames = true)]
    public class SulekhaDetailsForm
    {
        [DateTimeEditor]
        public DateTime DateTime { get; set; }
        public String UserName { get; set; }
        public String Mobile { get; set; }
        public String Email { get; set; }
        public String City { get; set; }
        public String Localities { get; set; }
        public String Comments { get; set; }
        public String Keywords { get; set; }
        public String Feedback { get; set; }
        public Boolean IsMoved { get; set; }
      
    }
}