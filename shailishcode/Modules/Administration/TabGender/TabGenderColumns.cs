using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Administration.Columns
{
    [ColumnsScript("Administration.TabGender")]
    [BasedOnRow(typeof(TabGenderRow), CheckNames = true)]
    public class TabGenderColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        //public String Name { get; set; }
        public DateTime Creation { get; set; }
        public DateTime Modified { get; set; }
        public String ModifiedBy { get; set; }
        public String Owner { get; set; }
        //public Int32 Docstatus { get; set; }
        //public String Parent { get; set; }
        //public String Parentfield { get; set; }
        //public String Parenttype { get; set; }
        //public Int32 Idx { get; set; }
        public String Gender { get; set; }
        //public String UserTags { get; set; }
        //public String Comments { get; set; }
        //public String Assign { get; set; }
        //public String LikedBy { get; set; }
    }
}