using Serenity.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Common.Forms
{
    [FormScript("Common.QuickEmail")]
    public class QuickEmailForm
    {
        [DisplayName("Email")]
        [Required]
        public String Email { get; set; }
        [DisplayName("Subject")]
        [Required]
        public String Subject { get; set; }
        [DisplayName("Message")]
        [Required, HtmlNoteContentEditor]
        public String Message { get; set; }
    }
}