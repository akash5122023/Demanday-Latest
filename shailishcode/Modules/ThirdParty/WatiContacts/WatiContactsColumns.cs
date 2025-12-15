
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("ThirdParty.WatiContacts")]
    [BasedOnRow(typeof(WatiContactsRow), CheckNames = true)]
    public class WatiContactsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Waid { get; set; }
        public String FirtName { get; set; }
        public String FullName { get; set; }
        public String Phone { get; set; }
        public String Source { get; set; }
        public String Status { get; set; }
        public DateTime Created { get; set; }
        public Boolean IsMoved { get; set; }
    }
}