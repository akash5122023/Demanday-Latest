
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

    [ColumnsScript("Services.AMCVisitPlanner")]
    [BasedOnRow(typeof(AMCVisitPlannerRow), CheckNames = true)]
    public class AMCVisitPlannerColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink, QuickFilter]
        public DateTime VisitDate { get; set; }
        [Hidden, QuickSearch]
        public String ContactName { get; set; }
        [Hidden, QuickSearch]
        public String ContactPhone { get; set; }
        [Hidden]
        public String ContactAddress { get; set; }
        [QuickFilter]
        public String AssignedToUsername { get; set; }
        [EditLink, QuickSearch]
        public String Serial { get; set; }
        [QuickFilter]
        public Masters.StatusMaster Status { get; set; }
        [QuickFilter]
        public DateTime CompletionDate { get; set; }
        public String VisitDetails { get; set; }
        [QuickFilter]
        public String RepresentativeDisplayName { get; set; }

    }
}