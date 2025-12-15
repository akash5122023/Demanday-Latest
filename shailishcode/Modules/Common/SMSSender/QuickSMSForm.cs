using Serenity.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Common.Forms
{
    [FormScript("Common.QuickSMS")]
    public class QuickSMSForm
    {
        [DisplayName("Number")]
        [Required, MaskedEditor(Mask = "9999999999")]
        public String Number { get; set; }
        [DisplayName("Message")]
        [Required, TextAreaEditor(Rows = 8)]
        public String Message { get; set; }
        [DisplayName("TemplateID")]
        [Required]
        public String TemplateID { get; set; }
    }
}