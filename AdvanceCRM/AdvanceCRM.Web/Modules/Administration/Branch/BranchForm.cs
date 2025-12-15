
namespace AdvanceCRM.Administration.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [FormScript("Administration.Branch")]
    [BasedOnRow(typeof(BranchRow), CheckNames = true)]
    public class BranchForm
    {
        public String Branch { get; set; }
        [HalfWidth, MaskedEditor(Mask = "9999999999")]
        public String Phone { get; set; }
        [HalfWidth]
        [EmailEditor]
        public String Email { get; set; }
        [TextAreaEditor(Rows = 4)]
        public String Address { get; set; }
        [HalfWidth]
        public Int32 Country { get; set; }
        [HalfWidth]
        public Int32 StateId { get; set; }
        [HalfWidth]
        public Int32 CityId { get; set; }
        [HalfWidth]
        public String Pin { get; set; }
        [Hidden]
        public Int32 CompanyId { get; set; }
    }
}