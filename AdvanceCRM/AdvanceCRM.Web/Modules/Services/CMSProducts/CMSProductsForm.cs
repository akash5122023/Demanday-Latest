
namespace AdvanceCRM.Services.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Services.CMSProducts")]
    [BasedOnRow(typeof(CMSProductsRow), CheckNames = true)]
    public class CMSProductsForm
    {
        public Int32 ProductsId { get; set; }
        public Double Price { get; set; }
        [DefaultValue("1")]
        public Double Quantity { get; set; }
        //public Int32 CMSId { get; set; }
    }
}