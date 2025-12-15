
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Department")]
    [BasedOnRow(typeof(DepartmentRow), CheckNames = true)]
    public class DepartmentForm
    {
        public String Department { get; set; }
    }
}