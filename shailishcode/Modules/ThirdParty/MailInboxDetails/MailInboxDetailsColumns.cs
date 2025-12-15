
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("ThirdParty.MailInboxDetails")]
    [BasedOnRow(typeof(MailInboxDetailsRow), CheckNames = true)]
    public class MailInboxDetailsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
       
       
        [EditLink]

        public String Subject { get; set; }
        public String Phone { get; set; }
        [DisplayName("MessageID")]
        public String ToName { get; set; }
        public String To { get; set; }
        public String ToAddress { get; set; }
        public String FromName { get; set; }
        public String From { get; set; }
        public String FromAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public String Content { get; set; }
      
        public Boolean IsMoved { get; set; }
    }
}