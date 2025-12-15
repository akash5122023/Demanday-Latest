
namespace AdvanceCRM.Masters.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Masters.Transportation")]
    [BasedOnRow(typeof(TransportationRow), CheckNames = true)]
    public class TransportationColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink]
        public String Name { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
    }
}