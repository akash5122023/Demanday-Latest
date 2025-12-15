using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class QuotationModel
    {
        public int id { get; set; }
       // public int ContactsId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }

        public int Status { get; set; }
        public string stage { get; set; }
        public string source { get; set; }
        public string Owner { get; set; }

        public string Assign { get; set; }
        public int QuotationNo { get; set; }
        public string QuotationN { get; set; }

        public int Assignedid { get; set; }
        //add Email
        public string Email { get; set; }

    }
}