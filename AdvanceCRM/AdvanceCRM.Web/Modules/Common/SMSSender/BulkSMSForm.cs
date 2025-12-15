using Serenity.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Common.Forms
{
    [FormScript("Common.BulkSMS")]
    public class BulkSMSForm
    {
        [Required, Placeholder("#customername token can be used in message to auto add customers name"), TextAreaEditor(Rows = 8)]
        public String Message { get; set; }

        [DisplayName("TemplateID")]
        [Required]
        public String TemplateID { get; set; }
    }
}