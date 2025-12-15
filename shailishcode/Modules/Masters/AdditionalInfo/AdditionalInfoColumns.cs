
namespace AdvanceCRM.Masters.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Masters.AdditionalInfo")]
    [BasedOnRow(typeof(AdditionalInfoRow), CheckNames = true)]
    public class AdditionalInfoColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String AdditionalInfo { get; set; }
        public AddInfoTypeMaster Type { get; set; }
    }
}