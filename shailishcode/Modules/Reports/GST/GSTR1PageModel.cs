
using AdvanceCRM.Masters;
using AdvanceCRM.Sales;
using AdvanceCRM.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvanceCRM.Modules.Reports.GST
{
    public class GSTR1PageModel
    {
        public List<SalesRow> SalesList { get; set; }
        public List<CompanyDetailsRow> CompanyList { get; set; }
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

        public string ProductName { get; set; }
        public string Price { get; set; }
        public string discount { get; set; }
        public string qty { get; set; }
        public double Ptotal { get; set; }

        public string taxable0 { get; set; }
        public string taxable5 { get; set; }
        public string taxable12 { get; set; }
        public string taxable18 { get; set; }
        public string taxable28 { get; set; }

        //5
        public string taxper15 { get; set; }
        public string taxper25 { get; set; }
        public string taxamt15 { get; set; }
        public string taxamt25 { get; set; }
        public string igst5 { get; set; }
        public string igstamt5 { get; set; }

        //12
        public string taxper112 { get; set; }
        public string taxper212 { get; set; }
        public string taxamt112 { get; set; }
        public string taxamt212 { get; set; }
        public string igst12 { get; set; }
        public string igstamt12 { get; set; }

        //18
        public string taxper118 { get; set; }
        public string taxper218 { get; set; }
        public string taxamt118 { get; set; }
        public string taxamt218 { get; set; }
        public string igst18 { get; set; }
        public string igstamt18 { get; set; }

        //28
        public string taxper128 { get; set; }
        public string taxper228 { get; set; }
        public string taxamt128 { get; set; }
        public string taxamt228 { get; set; }
        public string igst28 { get; set; }
        public string igstamt28 { get; set; }

        ///

        public string taxtype1 { get; set; }
        public string taxtype2 { get; set; }
        public double taxper1 { get; set; }
        public double taxper2 { get; set; }
        public double taxamt1 { get; set; }
        public double taxamt2 { get; set; }
        public double igst { get; set; }
        public double igstamt { get; set; }
        public double totamt { get; set; }
    }


}