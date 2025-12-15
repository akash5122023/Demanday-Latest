
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.RawTelecall")]
    [BasedOnRow(typeof(RawTelecallRow), CheckNames = true)]
    public class RawTelecallForm
    {
        public String CompanyName { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Details { get; set; }
        public String Feedback { get; set; }
        [ReadOnly(true)]
        public Int32 CreatedBy { get; set; }
        
        public Int32 AssignedTo { get; set; }
       
        public Boolean IsMoved { get; set; }
    }
}