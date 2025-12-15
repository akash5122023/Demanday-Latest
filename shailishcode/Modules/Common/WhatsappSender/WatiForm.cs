using Serenity.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Common.Forms
{
    [FormScript("Common.Wati")]
    public class WatiForm
    {
        [DisplayName("Number")]
        [Required,MaskedEditor(Mask = "+919999999999")]
        public String Number { get; set; }
        [DisplayName("Message")]
        [Required, TextAreaEditor(Rows = 8)]
        public String Message { get; set; }
    }
}