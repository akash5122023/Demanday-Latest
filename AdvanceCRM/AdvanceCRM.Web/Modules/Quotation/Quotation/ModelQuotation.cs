using AdvanceCRM.Services;
using AdvanceCRM.Products;
using System;
using System.Collections.Generic;

namespace AdvanceCRM.Services
{
    public class ModelLodgeComplaint
    {
        public string Phone { get; set; }
        public string Name { get; set; }


        public List<CMSRow> CMSList { get; set; }
        public Int32 contactdetails { get; set; }
        public string OTPNumber { get; set; }
        public string ComplaintDetails { get; set; }
        public string ProductId { get; set; }
        public List<ProductsRow> ProductList { get; set; }
        public string PriorityId { get; set; }
        public Int32 Id { get; set; }

        public DateTime Date { get; set; }


    }
}