
namespace AdvanceCRM.Employee.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Employee.EmployeeAssests")]
    [BasedOnRow(typeof(EmployeeAssestsRow), CheckNames = true)]
    public class EmployeeAssestsForm
    {
        public String Items { get; set; }
        public Int32 Quantity { get; set; }
        public String Description { get; set; }
        [Hidden]
        public Int32 EmployeeId { get; set; }

        //public Int32 OwnerId { get; set; }
    }
}