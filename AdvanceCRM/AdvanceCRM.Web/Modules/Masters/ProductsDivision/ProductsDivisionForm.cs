
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.ProductsDivision")]
    [BasedOnRow(typeof(ProductsDivisionRow), CheckNames = true)]
    public class ProductsDivisionForm
    {
        public String ProductsDivision { get; set; }
    }
}