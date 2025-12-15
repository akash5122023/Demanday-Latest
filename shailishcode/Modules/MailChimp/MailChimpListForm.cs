
namespace AdvanceCRM.MailChimpList.Forms
{
    using Serenity.ComponentModel;
    using Serenity.Web;
    using System;
    using System.ComponentModel;

    [FormScript("MailChimp.MailChimpList")]
    public class MailChimpListForm
    {
        [Required, DisplayName("List Name")]
        public String ListName { get; set; }
    }
}
