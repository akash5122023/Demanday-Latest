
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("ThirdParty.InteraktUser")]
    [BasedOnRow(typeof(InteraktUserRow), CheckNames = true)]
    public class InteraktUserColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String InteraktId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public String Phone { get; set; }
        public String CountryCode { get; set; }
        public String UserId { get; set; }
        public String FullName { get; set; }
        public String Email { get; set; }
        public Boolean WpOptedIn { get; set; }
        public Boolean IsMoved { get; set; }
    }
}