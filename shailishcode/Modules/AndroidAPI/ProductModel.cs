using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class ProductModel
    {

        public int id { get; set; }
        public string Name { get; set; }       
     
        public string Description { get; set; }
       
        public double SellingPrice { get; set; }
       
        public double Mrp { get; set; }

        public int TaxId1 { get; set; }
        public string TaxType1 { get; set; }
        public double TaxPer1 { get; set; }

        public int TaxId2 { get; set; }
        public string TaxType2 { get; set; }
        public double TaxPer2 { get; set; }





    }
}