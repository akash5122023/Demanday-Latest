
using AdvanceCRM.Enquiry;
using AdvanceCRM.Quotation;
using AdvanceCRM.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvanceCRM.Modules.Premium
{
    public class RepresentativePerformancePageModel
    {
        public List<EnquiryRow> EnquiryList { get; set; }
        public List<SalesRow> SalesList { get; set; }
        public List<QuotationRow> QuotationList { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
}