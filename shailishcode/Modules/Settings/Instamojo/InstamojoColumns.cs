
namespace AdvanceCRM.Settings.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Settings.Instamojo")]
    [BasedOnRow(typeof(InstamojoRow), CheckNames = true)]
    public class InstamojoColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String AppId { get; set; }
        public String AccessTokenKey { get; set; }
        public Boolean AutoEmail { get; set; }
        public Boolean AutoSms { get; set; }
        public String Sender { get; set; }
        public String Subject { get; set; }
        public String EmailTemplate { get; set; }
        public String Attachment { get; set; }
        public String SmsTemplate { get; set; }
        public String Host { get; set; }
        public Int32 Port { get; set; }
        public Boolean Ssl { get; set; }
        public String EmailId { get; set; }
        public String EmailPassword { get; set; }
    }
}