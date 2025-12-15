using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class EnquiryFollowupModel
    {
        public int id { get; set; }

        public string Note { get; set; }        
        public string Phone { get; set; }
        public int StatgeID { get; set; }
        public int SourceID { get; set; }        
        public string stage { get; set; }
        public string source { get; set; }
        public string EnquiryN { get; set; }
        public int EnquiryNo { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public string Details { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public int EnquiryID { get; set; }

    }
}