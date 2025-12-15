
namespace AdvanceCRM.Tasks.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [ColumnsScript("Tasks.Tasks")]
    [BasedOnRow(typeof(TasksRow), CheckNames = true)]
    public class TasksColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
       
        public Int32 Id { get; set; }

        [EditLink, QuickFilter]
        public String Project { get; set; }
        [EditLink, QuickFilter, Width(120), QuickSearch]
        public String TaskTitle { get; set; }
        [EditLink, QuickFilterOption("multiple", true), Width(120), QuickSearch]
        public String TaskId { get; set; }

        [QuickFilter, QuickFilterOption("multiple", true)]
        public String Status { get; set; }
        [QuickFilter]
        public String Type { get; set; }
        [QuickFilter]
        public Int32 Priority { get; set; }
        public Boolean Recurring { get; set; }
        [QuickSearch]
        public Int32 Period { get; set; }
        [QuickSearch, Hidden]
        public String ContactsName { get; set; }
        [Hidden,DisplayName("Details")]
        public String Details { get; set; }
        [QuickSearch, Hidden]
        public String ProductName { get; set; }
        [QuickFilter]
        public DateTime CreationDate { get; set; }
        [QuickFilter]
        public DateTime CompletionDate { get; set; }
        public DateTime ExpectedCompletion { get; set; }
        [Hidden]
        public String Resolution { get; set; }
        [QuickFilter]
        public String AssignedByUsername { get; set; }
        [QuickFilter]
        public String AssignedToUsername { get; set; }
    }
}