using AdvanceCRM.Administration;

using AdvanceCRM.Masters;
using AdvanceCRM.Purchase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvanceCRM.Modules.Reports.GST
{
    public class GSTR2B2BURPageModel
    {
        public List<PurchaseRow> PurchaseList { get; set; }
        public List<PurchaseProductsRow> PurchaseProductList { get; set; }
        public List<StateRow> StateList { get; set; }
        //public string GSTIN { get; set; }
        public string Supplier_Name { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string Total { get; set; }
        public string State { get; set; }
        public string ReverseCharge { get; set; }
        public string InvoiceType { get; set; }
        public string Rate { get; set; }
        public string TaxableValue { get; set; }
        public string Integrated_Tax_Paid { get; set; }
        public string Central_Tax_Paid { get; set; }
        public string State_UT_Tax_Paid { get; set; }
        public string Cess_Paid { get; set; }
        public string Eligibility_For_ITC { get; set; }
        public string Availed_ITC_Integrated_Tax { get; set; }
        public string Availed_ITC_Central_Tax { get; set; }
        public string Availed_ITC_State_UT_Tax { get; set; }
        public string Availed_ITC_Cess { get; set; }
        public string SupplyType { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public List<CompanyDetailsRow> CompanyList { get; set; }
    }
}