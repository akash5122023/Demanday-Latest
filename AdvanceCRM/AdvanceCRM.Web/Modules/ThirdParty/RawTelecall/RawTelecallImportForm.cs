
namespace AdvanceCRM.ThirdParty.Forms
{
    using AdvanceCRM.Administration;
    using Serenity.ComponentModel;
    using Serenity.Web;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [FormScript("ThirdParty.RawTelecallImport")]
    public class RawTelecallImportForm
    {
        [FileUploadEditor(DisplayFileName = true), Required]
        public String FileName { get; set; }
        [LookupEditor(typeof(UserRow), Multiple = true), DisplayName("Representatives")]
        public List<int> UIds { get; set; }
    }
}
