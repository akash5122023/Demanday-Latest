
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.MailInboxDetails")]
    [BasedOnRow(typeof(MailInboxDetailsRow), CheckNames = true)]
    public class MailInboxDetailsForm
    {
        public String Subject { get; set; }
        public String Phone { get; set; }
        public String ToName { get; set; }
        public String To { get; set; }
        public String ToAddress { get; set; }
        public String FromName { get; set; }
        public String From { get; set; }
        public String FromAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public String Content { get; set; }
        public String Attachment { get; set; }
       
        public Boolean IsMoved { get; set; }
    }
}