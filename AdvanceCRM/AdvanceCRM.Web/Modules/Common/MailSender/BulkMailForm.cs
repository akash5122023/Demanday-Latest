using Serenity.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Common.Forms
{
    [FormScript("Common.BulkMail")]
    public class BulkMailForm
    {
        public String Subject { get; set; }
        public String Attach { get; set; }
        [Required, Placeholder("#customername token can be used in message to auto add customers name"), TextAreaEditor(Rows = 8)]
        public String Message { get; set; }
    }
}