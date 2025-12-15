
namespace AdvanceCRM.BizMail.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("BizMail.BmSubscribers")]
    [BasedOnRow(typeof(BmSubscribersRow), CheckNames = true)]
    public class BmSubscribersColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String SubscriberId { get; set; }
        public String Email { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Status { get; set; }
        public String Source { get; set; }
        public String IpAddress { get; set; }
        public String DateAdded { get; set; }
        public String ListName { get; set; }
        public String ListId { get; set; }
        public String Phone { get; set; }
        public Boolean IsMoved { get; set; }
    }
}