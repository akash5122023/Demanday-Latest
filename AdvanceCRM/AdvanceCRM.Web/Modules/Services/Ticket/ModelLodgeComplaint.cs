
using AdvanceCRM.Quotation;
using AdvanceCRM.Masters;
using AdvanceCRM.Administration;
using AdvanceCRM.Services;
using AdvanceCRM.Products;
using System;
using System.Collections.Generic;

namespace AdvanceCRM.Quotation
{
    public class ModelQuotation
    {
        public Boolean taxable { get; set; }
        public Int32 id { get; set; }
        public String modtype { get; set; }
        public String Name { get; set; }
        public ProductsRow products { get; set; }

        public List<CMSRow> CMSList { get; set; }
        public Int32 contactdetails { get; set; }
        public List<ProductsRow> ProductsList { get; set; }
        public QuotationRow Quotation { get; set; }
        public List<ProductsDivisionRow> Division { get; set; }
        public List<QuotationProductsRow> QuotationProducts { get; set; }
        public List<QuotationTermsRow> QuotationTerms { get; set; }
        public List<QuotationChargesRow> QuotationCharges { get; set; }
        public List<QuotationConcessionRow> QuotationConcession { get; set; }
        public Administration.UserRow Representative { get; set; }
        public List<QuotationRow> QuotationMail { get; set; }
        public Contacts.ContactsRow Contacts { get; set; }
        public CompanyDetailsRow Company { get; set; }
        public ProjectRow Project { get; set; }
    }
}