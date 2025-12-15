using Serenity.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Common.Forms
{
    [FormScript("Common.QuickBizEmail")]
    public class QuickBizEmailForm
    {
        
        [DisplayName("Subject")]
        [Required]
        public String Subject { get; set; }
        [DisplayName("Message")]
        [Required, HtmlNoteContentEditor]
        public String Message { get; set; }

        [DisplayName("Send_at")]
        [Required,DateTimeEditor]
        public DateTime Date { get; set; }
    }
}