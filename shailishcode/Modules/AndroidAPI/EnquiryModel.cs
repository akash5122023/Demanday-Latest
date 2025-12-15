using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{

    public class pagination 
    {
        public string totalCount { get; set; }
        public string pageSize { get; set; }
        public string currentPage { get; set; }
        public string totalPages { get; set; }
        public string previousPage { get; set; }
        public string nextPage { get; set; }
        public List<EnquiryModel> Enquiry { get; set; }
    }
    public class EnquiryModel
    {
        public int id { get; set; }
       public int ContactsId { get; set; }
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int Status { get; set; }

       public int Stageid { get; set; }

        public string stage { get; set; }
        public string phone { get; set; }
        public string Address { get; set; }
        public string source { get; set; }
        public string Owner { get; set; }
        public string Assign { get; set; }
        public string AdditionalInfo { get; set; }
        public int EnquiryNo { get; set; }
        public string EnquiryN { get; set; }
        public int sourceid { get; set; }
        public int Ownerid { get; set; }
        public int Assignedid { get; set; }

    }
}