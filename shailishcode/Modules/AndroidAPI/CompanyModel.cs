using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class CompanyModel
    {
        public int id { get; set; }

        public int Yearinprefix { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string EnquirySuffix { get; set; }
        public string EnquiryPrefix { get; set; }
        public string QuotationSuffix { get; set; }
        public string QuotationPrefix { get; set; }
        public string CMSSuffix { get; set; }
        public string CMSPrefix { get; set; }

    }
}