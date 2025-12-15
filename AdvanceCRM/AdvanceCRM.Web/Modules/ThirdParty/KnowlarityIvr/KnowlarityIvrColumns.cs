
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("ThirdParty.KnowlarityIvr")]
    [BasedOnRow(typeof(KnowlarityIvrRow), CheckNames = true)]
    public class KnowlarityIvrColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Name { get; set; }
        public String Mobile { get; set; }
        public String EmpMobile { get; set; }
        public String IvrNo { get; set; }
        public String Recording { get; set; }
        public String Date { get; set; }
        public String Duration { get; set; }
    }
}