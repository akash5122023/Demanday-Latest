using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Purchase.Forms
{
    [FormScript("Purchase.GrnProductsTwo")]
    [BasedOnRow(typeof(GrnProductsTwoRow), CheckNames = true)]
    public class GrnProductsTwoForm
    {
        [Category("General")]
        [HalfWidth]
        public Int32 ProductsId { get; set; }

        [HalfWidth]
        public String Code { get; set; }

        [HalfWidth]
        public Int32 BranchId { get; set; }

        [HalfWidth]
        public Int32 Price { get; set; }

        [HalfWidth]
        public Double OrderQuantity { get; set; }

        [HalfWidth]
        public Double ReceivedQuantity { get; set; }

        [HalfWidth]
        public Double ExtraQuantity { get; set; }

        [HalfWidth]
        public Double RejectedQuantity { get; set; }

        [Category("Description")]
        public String Description { get; set; }

        //public Int32 GrnId { get; set; }
    }
}