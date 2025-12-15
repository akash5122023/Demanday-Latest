
namespace AdvanceCRM.Services.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [ColumnsScript("Services.AMC")]
    [BasedOnRow(typeof(AMCRow), CheckNames = true)]
    public class AMCColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Hidden]
        public Int32 Id { get; set; }
        [EditLink, AlignRight, SortOrder(1, true)]
        public Int32 AMCNo { get; set; }
        [EditLink, Width(180), QuickFilter, QuickSearch]
        public String ContactsName { get; set; }
        [QuickFilter]
        public DateTime Date { get; set; }
        [QuickFilter, QuickFilterOption("multiple", true)]

        public Masters.StatusMaster Status { get; set; }

        [QuickFilter]
        public DateTime StartDate { get; set; }
        [QuickFilter]
        public DateTime EndDate { get; set; }
        [QuickFilter]
        public DateTime DueDate { get; set; }
        public String AdditionalInfo { get; set; }
        [QuickFilter]
        public String OwnerUsername { get; set; }
        [QuickFilter]
        public String AssignedUsername { get; set; }
        
    }
}