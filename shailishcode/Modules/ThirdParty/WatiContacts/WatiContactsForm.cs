
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.WatiContacts")]
    [BasedOnRow(typeof(WatiContactsRow), CheckNames = true)]
    public class WatiContactsForm
    {
        public String Waid { get; set; }
        public String FirtName { get; set; }
        public String FullName { get; set; }
        public String Phone { get; set; }
        public String Source { get; set; }
        public String Status { get; set; }
        public DateTime Created { get; set; }
        public Boolean IsMoved { get; set; }
    }
}