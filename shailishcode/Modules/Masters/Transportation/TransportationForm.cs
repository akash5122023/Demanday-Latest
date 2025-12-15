
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Transportation")]
    [BasedOnRow(typeof(TransportationRow), CheckNames = true)]
    public class TransportationForm
    {
        public String Name { get; set; }
        public String Address { get; set; }
        [MaskedEditor(Mask = "9999999999")]
        public String Phone { get; set; }
        public String Email { get; set; }
        public String ContactPerson { get; set; }
        [MaskedEditor(Mask = "9999999999")]
        public String ContactPersonPhone { get; set; }
        public String GSTIN { get; set; }
    }
}