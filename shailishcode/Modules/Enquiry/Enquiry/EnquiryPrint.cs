
namespace AdvanceCRM.Enquiry
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Enquiry;
    using Serenity.Data;
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using Serenity.Services;

    [Report("Enquiry.PrintEnquiry")]
    [ReportDesign(MVC.Views.Enquiry.Enquiry_.EnquiryPrint)]
    public class EnquiryReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public EnquiryReport(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context;
        }

        public EnquiryReport()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }

        public Int32 Id { get; set; }
        public String ModType { get; set; }

        public object GetData()
        {
            var data = new EnquiryReportData();

            using (var connection = _connections.NewFor<EnquiryRow>())
            {
                data.id = this.Id;
                data.modtype = this.ModType;


                var o = EnquiryRow.Fields;

                data.Enquiry = connection.TryById<EnquiryRow>(this.Id, q => q
                     .SelectTableFields()
                     .Select(o.Id)                    
                     .Select(o.Date)
                     .Select(o.ContactsName)
                     .Select(o.ContactsPhone)
                     .Select(o.ContactsEmail)
                     .Select(o.ContactsAddress)
                     .Select(o.ContactsStateId)                   
                     .Select(o.ContactsGstin)
                     .Select(o.ContactsPanNo)
                     //.Select(o.Lines)
                     //.Select(o.MessageId)
                     //.Select(o.Message)
                     .Select(o.ContactPersonName)
                     .Select(o.ContactPersonPhone)
                     //.Select(o.Taxable)
                     //.Select(o.Conversion)
                     //.Select(o.CurrencyConversion)
                     //.Select(o.FromCurrency)
                     //.Select(o.ToCurrency)
                     //.Select(o.EnquiryNo)
                     //.Select(o.EnquiryDate)
                     .Select(o.Total)

                     );

                data.Name = data.Enquiry.ContactsName;

                var od = EnquiryProductsRow.Fields;
                data.EnquiryProducts = connection.List<EnquiryProductsRow>(q => q
                    .SelectTableFields()
                    .Select(od.ProductsCode)
                    .Select(od.ProductsHsn)
                    .Select(od.ProductsName)
                    .Select(od.ProductsDescription)
                    .Select(od.Capacity)
                    .Select(od.Price)
                    .Select(od.Quantity)
                    .Select(od.Discount)
                  
                    .Select(od.LineTotal)
                  
                    .Select(od.ProductsImage)
                    .Select(od.Description)
                    .Select(od.ProductsTechSpecs)
                    .Select(od.Unit)
                    .Where(od.EnquiryId == this.Id));


                //var qt = QuotationTermsRow.Fields;
                //data.QuotationTerms = connection.List<QuotationTermsRow>(q => q
                //    .SelectTableFields()
                //    .Select(qt.Terms)
                //    .Where(qt.QuotationId == this.Id));

                var r = Administration.UserRow.Fields;
                data.Representative = connection.TryFirst<Administration.UserRow>(r.UserId == data.Enquiry.AssignedId.Value)
                    ?? new Administration.UserRow();

                var c = CompanyDetailsRow.Fields;

                data.Company = connection.TryById<CompanyDetailsRow>(((UserDefinition)Context.User.ToUserDefinition()).CompanyId, q => q
                     .SelectTableFields()
                     .Select(c.Name)
                     .Select(c.Slogan)
                     .Select(c.Address)
                     .Select(c.Phone)
                     .Select(c.Logo)
                     .Select(c.LogoHeight)
                     .Select(c.LogoWidth)
                     .Select(c.HeaderImage)
                     .Select(c.HeaderHeight)
                     .Select(c.HeaderWidth)
                     .Select(c.FooterImage)
                     .Select(c.FooterHeight)
                     .Select(c.FooterWidth)
                     .Select(c.HeaderContent)
                     .Select(c.FooterContent)
                     .Select(c.GSTIN)
                     .Select(c.PANNo)
                     .Select(c.StateId)
                     .Select(c.CompanyDetails)
                     //.Select(c.QuotationTemplate)
                     //.Select(c.QuotationSuffix)
                     //.Select(c.QuotationPrefix)
                     //.Select(c.QuotationTaxColumnIncluded)
                     //.Select(c.QuotationDiscountedPriceIncluded)
                     );

                var QuotationSettings = connection.TryById<CompanyDetailsRow>(1, q => q
                     .SelectTableFields()
                     .Select(c.MultiCurrency)
                     .Select(c.EnableAdditionalCharges)
                     .Select(c.EnableAdditionalConcessions)
                     );

                data.Company.MultiCurrency = QuotationSettings.MultiCurrency;
                data.Company.EnableAdditionalCharges = QuotationSettings.EnableAdditionalCharges;
                data.Company.EnableAdditionalConcessions = QuotationSettings.EnableAdditionalConcessions;

                //if (data.Company.EnableAdditionalCharges == true)
                //{
                //    var qcha = QuotationChargesRow.Fields;
                //    data.QuotationCharges = connection.List<QuotationChargesRow>(q => q
                //        .SelectTableFields()
                //        .Select(qcha.ChargesName)
                //        .Select(qcha.ChargesPercentage)
                //        .Where(qcha.QuotationId == this.Id));
                //}

                //if (data.Company.EnableAdditionalConcessions == true)
                //{
                //    var qcha = QuotationConcessionRow.Fields;
                //    data.QuotationConcession = connection.List<QuotationConcessionRow>(q => q
                //        .SelectTableFields()
                //        .Select(qcha.ConcessionName)
                //        .Select(qcha.ConcessionPercentage)
                //        .Where(qcha.QuotationId == this.Id));
                //}

                //data.taxable = data.Quotation.Taxable == null ? false : data.Quotation.Taxable.Value;
                //if (data.Company.MultiCurrency == true)
                //{
                //    if (data.Quotation.CurrencyConversion != null && data.Quotation.CurrencyConversion == true)
                //    {
                //        data.taxable = false;
                //        foreach (var pro in data.QuotationProducts)
                //        {
                //            pro.Price = pro.Price * data.Quotation.Conversion;
                //            pro.Discount = pro.Discount * data.Quotation.Conversion;
                //            pro.Tax1Amount = pro.Tax1Amount * (Decimal)data.Quotation.Conversion;
                //            pro.Tax2Amount = pro.Tax2Amount * (Decimal)data.Quotation.Conversion;
                //            pro.LineTotal = pro.LineTotal * (Decimal)data.Quotation.Conversion;
                //            pro.GrandTotal = pro.GrandTotal * (Decimal)data.Quotation.Conversion;
                //        }
                //    }
                //}
            }

            return data;
        }

        public void Customize(IHtmlToPdfOptions options)
        {
            // you may customize HTML to PDF converter (WKHTML) parameters here, e.g. 
            // options.MarginsAll = "2cm";
        }
    }

    public class EnquiryReportData
    {
        public Boolean taxable { get; set; }
        public Int32 id { get; set; }
        public String modtype { get; set; }
        public String Name { get; set; }
        public EnquiryRow Enquiry { get; set; }
        public List<EnquiryProductsRow> EnquiryProducts { get; set; }
        //public List<QuotationTermsRow> QuotationTerms { get; set; }
        //public List<QuotationChargesRow> QuotationCharges { get; set; }
        //public List<QuotationConcessionRow> QuotationConcession { get; set; }
        public Administration.UserRow Representative { get; set; }
        //public List<QuotationRow> QuotationMail { get; set; }
        public Contacts.ContactsRow Contacts { get; set; }
        public CompanyDetailsRow Company { get; set; }
    }
}