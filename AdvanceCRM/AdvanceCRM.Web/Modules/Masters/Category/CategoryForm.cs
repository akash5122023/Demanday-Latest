
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Category")]
    [BasedOnRow(typeof(CategoryRow), CheckNames = true)]
    public class CategoryForm
    {
        public String Category { get; set; }
        public Int32 Type { get; set; }
    }
}