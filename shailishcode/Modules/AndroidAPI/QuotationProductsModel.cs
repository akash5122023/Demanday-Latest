using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class QuotationProductsModel
    {
        public int id { get; set; }       
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public string stage { get; set; }
        public string source { get; set; }
        public string Owner { get; set; }

        public int Assignedid { get; set; }
    }
}