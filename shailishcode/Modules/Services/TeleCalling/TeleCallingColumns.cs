
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

    [ColumnsScript("Services.TeleCalling")]
    [BasedOnRow(typeof(TeleCallingRow), CheckNames = true)]
    public class TeleCallingColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, SortOrder(1, true)]
        public Int32 Id { get; set; }
        [EditLink, QuickSearch]
        public String ContactsName { get; set; }
        [EditLink, QuickSearch]
        public String ContactsPhone { get; set; }
        public String ContactsAddress { get; set; }
        [QuickFilter]
        public DateTime Date { get; set; }
        [QuickFilter]
        public String ProductsName { get; set; }
        [QuickFilter, QuickFilterOption("multiple", true)]
        public Masters.AppointmentTypeMaster Status { get; set; }
        [QuickFilter]
        public String Source { get; set; }
        [QuickFilter]
        public String Stage { get; set; }
        public String Branch { get; set; }
        public String Details { get; set; }
        [Hidden]
        public String Feedback { get; set; }
        [QuickFilter]
        public DateTime AppointmentDate { get; set; }
        [QuickFilter]
        public String RepresentativeUsername { get; set; }
        [QuickFilter]
        public String AssignedToUsername { get; set; }
    }
}