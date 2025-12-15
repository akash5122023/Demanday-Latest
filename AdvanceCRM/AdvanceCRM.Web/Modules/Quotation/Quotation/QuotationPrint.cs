namespace AdvanceCRM.Quotation
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Products;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Masters;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdvanceCRM.Itinerary;
    using Serenity.Services;

    [Report("Quotation.PrintQuotation")]
    [ReportDesign(MVC.Views.Quotation.Quotation_.QuotationPrint)]
    public class QuotationReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public QuotationReport(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public QuotationReport()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }

        public Int32 Id { get; set; }
        public String ModType { get; set; }

        public object GetData()
        {
            var data = new QuotationReportData();

            using (var connection = _connections.NewFor<QuotationRow>())
            {
                data.id = this.Id;
                data.modtype = this.ModType;


                var o = QuotationRow.Fields;

                data.Quotation = connection.TryById<QuotationRow>(this.Id, q => q
                     .SelectTableFields()
                     .Select(o.Id)
                     .Select(o.QuotationNo)
                     .Select(o.QuotationN)
                     .Select(o.ProjectId)
                     .Select(o.Date)
                     .Select(o.ContactsName)
                     .Select(o.ContactsPhone)
                     .Select(o.ContactsEmail)
                     .Select(o.ContactsAddress)
                     .Select(o.ContactsStateId)
                     .Select(o.Subject)
                     .Select(o.Reference)
                     .Select(o.ContactsGstin)
                     .Select(o.ContactsPanNo)
                     .Select(o.Lines)
                     .Select(o.EnquiryN)
                     .Select(o.MessageId)
                     .Select(o.Message)
                     .Select(o.ContactPersonName)
                     .Select(o.ContactPersonPhone)
                     .Select(o.Taxable)
                     .Select(o.Conversion)
                     .Select(o.CurrencyConversion)
                     .Select(o.FromCurrency)
                     .Select(o.ToCurrency)
                     .Select(o.EnquiryNo)
                     .Select(o.EnquiryDate)
                     .Select(o.Total)
                     .Select(o.Tax1)
                     .Select(o.Tax2)
                     .Select(o.GrandTotal)
                     .Select(o.PerDiscount)
                     .Select(o.DiscountAmt)
                     .Select(o.DisGrandTotal)
                     .Select(o.Roundup)
                     );

                data.Name = data.Quotation.ContactsName;

                data.Quotation.Roundup *= data.Quotation.Conversion;


                var od = QuotationProductsRow.Fields;
                //string temp_str = od.QuotationId == this.Id + " ORDER BY "+od.Id+" ASC ";// + "CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + Context.User.GetIdentifier().ToString() + fstr + ")";

                data.QuotationProducts = connection.List<QuotationProductsRow>(q => q
                    .SelectTableFields()
                    .Select(od.ProductsDivisionId)
                    .Select(od.ProductsDivision)
                    .Select(od.ProductsId)
                    .Select(od.ProductsCode)
                    .Select(od.ProductsHsn)
                    .Select(od.ProductsName)
                    .Select(od.ProductsDescription)
                    .Select(od.Capacity)
                    .Select(od.Price)
                    .Select(od.Quantity)
                    .Select(od.Discount)
                    .Select(od.DiscountAmount)
                    .Select(od.TaxType1)
                    .Select(od.Percentage1)
                    .Select(od.Tax1Amount)
                    .Select(od.TaxType2)
                    .Select(od.Percentage2)
                    .Select(od.Tax2Amount)
                    .Select(od.DiscPrice)
                    .Select(od.LineTotal)
                    .Select(od.GrandTotal)
                    .Select(od.ProductsImage)
                    .Select(od.Description)
                    .Select(od.ProductsTechSpecs)
                    .Select(od.Unit)
                    .Select(od.FileAttachment)
                    .OrderBy(od.Id, false)
                    .Where(od.QuotationId == this.Id));


                //model.SourceWise = connection.List<EnquiryRow>(q => q
                //                 .SelectTableFields()
                //                 .Select(e.Source)
                //                 .Select(e.SourceId));
                //var div = ProductsRow.Fields;
                //data.ProductsList = connection.List<ProductsRow>(q => q
                //  .SelectTableFields()
                //  .Select(div.DivisionId)
                // .Select(div.DivisionProductsDivision));

                //// .Where(div.Id == )
                // );


                var qt = QuotationTermsRow.Fields;
                data.QuotationTerms = connection.List<QuotationTermsRow>(q => q
                    .SelectTableFields()
                    .Select(qt.Terms)
                    .Where(qt.QuotationId == this.Id));

                var r = Administration.UserRow.Fields;
                data.Representative = connection.TryFirst<Administration.UserRow>(r.UserId == data.Quotation.AssignedId.Value)
                    ?? new Administration.UserRow();

                var c = CompanyDetailsRow.Fields;

                var userDef = Context?.User?.ToUserDefinition();
                var companyId = userDef?.CompanyId;

                if (companyId == null || companyId == 0)
                    companyId = 1;

                data.Company = connection.TryById<CompanyDetailsRow>(companyId.Value, q => q
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
                         .Select(c.QuotationTemplate)
                         .Select(c.QuotationSuffix)
                         .Select(c.QuotationPrefix)
                         .Select(c.QuotationTaxColumnIncluded)
                         .Select(c.QuotationDiscountedPriceIncluded)
                         .Select(c.QuotationTotal)
                         .Select(c.RemoveGtColumn)
                         );
                     new CompanyDetailsRow();

                var QuotationSettings = connection.TryById<CompanyDetailsRow>(1, q => q
                     .SelectTableFields()
                     .Select(c.MultiCurrency)
                     .Select(c.EnableAdditionalCharges)
                     .Select(c.EnableAdditionalConcessions)
                     );

                data.Company.MultiCurrency = QuotationSettings.MultiCurrency;
                data.Company.EnableAdditionalCharges = QuotationSettings.EnableAdditionalCharges;
                data.Company.EnableAdditionalConcessions = QuotationSettings.EnableAdditionalConcessions;

                if (data.Company.EnableAdditionalCharges == true)
                {
                    var qcha = QuotationChargesRow.Fields;
                    data.QuotationCharges = connection.List<QuotationChargesRow>(q => q
                        .SelectTableFields()
                        .Select(qcha.ChargesName)
                        .Select(qcha.ChargesPercentage)
                        .Where(qcha.QuotationId == this.Id));
                }

                if (data.Company.EnableAdditionalConcessions == true)
                {
                    var qcha = QuotationConcessionRow.Fields;
                    data.QuotationConcession = connection.List<QuotationConcessionRow>(q => q
                        .SelectTableFields()
                        .Select(qcha.ConcessionName)
                        .Select(qcha.ConcessionPercentage)
                        .Select(qcha.ConcessionAmount)
                        .Where(qcha.QuotationId == this.Id));
                }

                if (data.Quotation.ProjectId != null)
                {
                    var proj = ProjectRow.Fields;
                    data.Project = connection.TryById<ProjectRow>(Convert.ToInt32(data.Quotation.ProjectId), q => q
                        .SelectTableFields()
                        .Select(proj.Project)
                        );
                }
                data.taxable = data.Quotation.Taxable == null ? false : data.Quotation.Taxable.Value;
                if (data.Company.MultiCurrency == true)
                {
                    if (data.Quotation.CurrencyConversion != null && data.Quotation.CurrencyConversion == true)
                    {
                        data.taxable = false;
                        foreach (var pro in data.QuotationProducts)
                        {
                            pro.Price = pro.Price * data.Quotation.Conversion;
                            pro.Discount = pro.Discount * data.Quotation.Conversion;
                            pro.Tax1Amount = pro.Tax1Amount * (Decimal)data.Quotation.Conversion;
                            pro.Tax2Amount = pro.Tax2Amount * (Decimal)data.Quotation.Conversion;
                            pro.LineTotal = pro.LineTotal * (Decimal)data.Quotation.Conversion;
                            pro.GrandTotal = pro.GrandTotal * (Decimal)data.Quotation.Conversion;

                        }
                    }
                }
                //data.Quotation.GrandTotal = (double?)data.QuotationProducts.Sum(p => p.GrandTotal);

            }

            return data;
        }

        public void Customize(IHtmlToPdfOptions options)
        {
            // you may customize HTML to PDF converter (WKHTML) parameters here, e.g. 
            // options.MarginsAll = "2cm";
            // options.FooterHeaderReplace();
        }
    }

    public class QuotationReportData
    {
        public Boolean taxable { get; set; }
        public Int32 id { get; set; }
        public String modtype { get; set; }
        public String Name { get; set; }

        public ProductsRow products { get; set; }

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
        public List<ItineraryTermRow> ItineraryTerm { get; internal set; }
        public ItineraryRow Itinerary { get; internal set; }
    }
}