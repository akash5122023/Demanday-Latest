
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Grade")]
    [BasedOnRow(typeof(GradeRow), CheckNames = true)]
    public class GradeForm
    {
        public String Grade { get; set; }
    }
}