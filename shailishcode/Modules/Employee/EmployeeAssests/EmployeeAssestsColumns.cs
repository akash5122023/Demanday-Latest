
namespace AdvanceCRM.Employee.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using Serenity.Data.Mapping;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Employee.EmployeeAssests")]
    [BasedOnRow(typeof(EmployeeAssestsRow), CheckNames = true)]
    public class EmployeeAssestsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }

        [EditLink]
        public String Items { get; set; }
        public Int32 Quantity { get; set; }
        public String Description { get; set; }
        [Hidden]
        public String EmployeeName { get; set; }
        //[Hidden]
       // public String OwnerUsername { get; set; }
    }
}