
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.ProductsUnit")]
    [BasedOnRow(typeof(ProductsUnitRow), CheckNames = true)]
    public class ProductsUnitForm
    {
        public String ProductsUnit { get; set; }
    }
}