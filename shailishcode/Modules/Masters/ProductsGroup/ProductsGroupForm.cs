
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.ProductsGroup")]
    [BasedOnRow(typeof(ProductsGroupRow), CheckNames = true)]
    public class ProductsGroupForm
    {
        public String ProductsGroup { get; set; }
    }
}