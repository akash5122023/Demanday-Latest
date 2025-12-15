
using AdvanceCRM.Masters;
using AdvanceCRM.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvanceCRM.Modules.Reports.GST
{
    public class GSTR1B2CLPageModel
    {
        public List<SalesRow> SalesList { get; set; }
        public List<SalesProductsRow> SalesProductList { get; set; }
        public List<StateRow> StateList { get; set; }
        public string GSTIN { get; set; }
        public string NAME { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string Total { get; set; }
        public string State { get; set; }
        public string ReverseCharge { get; set; }
        public string InvoiceType { get; set; }
        public string ECommGSTIN { get; set; }
        public string Rate { get; set; }
        public double ApplicablePer { get; set; }
        public string TaxableValue { get; set; }
        public double CessAmount { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

    }
}