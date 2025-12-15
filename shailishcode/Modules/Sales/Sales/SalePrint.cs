namespace AdvanceCRM.Sales
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;

    using Serenity.Data;
    using Serenity.Reporting;
  
    using Serenity.Web;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Serenity.Services;
    using Serenity.Extensions.DependencyInjection;

    [Report("Sales.PrintSale")]
    [ReportDesign(MVC.Views.Sales.Sales_.SalePrint)]
    public class SalesReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _connections;
        private IRequestContext Context { get; }

        public List<int> Ids { get; set; }
        public Int32 Id { get; set; }
        public String ModType { get; set; }

        public SalesReport(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context;
        }

        public SalesReport()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }

        public object GetData()
        {
            using var connection = _connections.NewFor<SalesRow>();

            if (Ids != null && Ids.Any())
            {
                var multipleData = new List<SalesReportData>();
                foreach (var singleId in Ids)
                    multipleData.Add(FetchSalesReportData(connection, singleId));

                return multipleData;
            }

            return FetchSalesReportData(connection, Id);
        }

        private SalesReportData FetchSalesReportData(IDbConnection connection, int salesId)
        {
            var data = new SalesReportData();

            data.id = salesId;
            data.modtype = this.ModType;

            var o = SalesRow.Fields;
            data.Invoice = connection.TryById<SalesRow>(salesId, q => q
                .SelectTableFields()
                .Select(o.Id)
                .Select(o.Date)
                .Select(o.ContactsName)
                .Select(o.ContactsPhone)
                .Select(o.ContactsEmail)
                .Select(o.ContactsAddress)
                .Select(o.ContactsStateId)
                .Select(o.ShippingAddress)
                .Select(o.BillingAddress)
                .Select(o.ContactsGstin)
                .Select(o.ContactsPanNo)
                .Select(o.PackagingCharges)
                .Select(o.FreightCharges)
                .Select(o.Advacne)
                .Select(o.DueDate)
                .Select(o.DispatchDetails)
                .Select(o.Roundup)
                .Select(o.Lines)
                .Select(o.QuotationN)
                .Select(o.ContactPersonName)
                .Select(o.ContactPersonPhone)
                .Select(o.InvoiceNo)
                .Select(o.MessageId)
                .Select(o.Message)
                .Select(o.Conversion)
                .Select(o.PurchaseOrderNo)
                .Select(o.PurchaseOrderDate)
                .Select(o.CurrencyConversion)
                .Select(o.ToCurrency)
                .Select(o.FromCurrency)
                .Select(o.Taxable)
                .Select(o.Total)
                .Select(o.Tax1)
                .Select(o.Tax2)
                .Select(o.GrandTotal)
            );

            if (data.Invoice == null)
                return data; // no invoice found, return empty

            data.Name = data.Invoice.ContactsName;

            var od = SalesProductsRow.Fields;
            data.InvoiceProducts = connection.List<SalesProductsRow>(q => q
                .SelectTableFields()
                .Select(od.ProductsCode)
                .Select(od.ProductsHsn)
                .Select(od.ProductsName)
                .Select(od.ProductsDescription)
                .Select(od.Serial)
                .Select(od.Price)
                .Select(od.Quantity)
                .Select(od.Discount)
                .Select(od.TaxType1)
                .Select(od.Percentage1)
                .Select(od.Tax1Amount)
                .Select(od.TaxType2)
                .Select(od.Percentage2)
                .Select(od.Tax2Amount)
                .Select(od.WarrantyStart)
                .Select(od.WarrantyEnd)
                .Select(od.DiscPrice)
                .Select(od.LineTotal)
                .Select(od.GrandTotal)
                .Select(od.Description)
                .Select(od.ProductsTechSpecs)
                .Select(od.ProductsImage)
                .Select(od.Unit)
                .OrderBy(od.Id, false)
                .Where(od.SalesId == salesId));

            var it = SalesTermsRow.Fields;
            data.SalesTerms = connection.List<SalesTermsRow>(q => q
                .SelectTableFields()
                .Select(it.Terms)
                .Where(it.SalesId == salesId));

            var r = Administration.UserRow.Fields;
            data.Representative = connection.TryFirst<Administration.UserRow>(r.UserId == data.Invoice.AssignedId.Value)
                ?? new Administration.UserRow();

            //var c = CompanyDetailsRow.Fields;

            //var userDef = Context?.User?.ToUserDefinition() as UserDefinition ??
            //               Serenity.Authorization.UserDefinition as UserDefinition;

            //var companyId = userDef?.CompanyId;

            //if (companyId == null || companyId == 0)
            //    companyId = 1;

            //if (userDef != null)
            //    data.Company = connection.TryById<CompanyDetailsRow>(userDef.CompanyId, q => q
            //        .SelectTableFields()
            //        .Select(c.Name)
            //        .Select(c.Slogan)
            //        .Select(c.Address)
            //        .Select(c.Phone)
            //        .Select(c.StateId)
            //        .Select(c.Logo)
            //        .Select(c.LogoHeight)
            //        .Select(c.LogoWidth)
            //        .Select(c.InvoiceHeaderImage)
            //        .Select(c.InvoiceHeaderHeight)
            //        .Select(c.InvoiceHeaderWidth)
            //        .Select(c.InvoiceFooterImage)
            //        .Select(c.InvoiceFooterHeight)
            //        .Select(c.InvoiceFooterWidth)
            //        .Select(c.InvoiceHeaderContent)
            //        .Select(c.InvoiceFooterContent)
            //        .Select(c.GSTIN)
            //        .Select(c.PANNo)
            //        .Select(c.MultiCurrency)
            //        .Select(c.CompanyDetails)
            //        .Select(c.InvoiceTemplate)
            //        .Select(c.EnableAdditionalCharges)
            //        .Select(c.EnableAdditionalConcessions)
            //        .Select(c.InvoiceSuffix)
            //        .Select(c.InvoicePrefix)
            //        .Select(c.InvoiceTaxColumnIncluded)
            //    );
            //else
            //    data.Company = new CompanyDetailsRow();

            //var InvoiceSettings = connection.TryById<CompanyDetailsRow>(1, q => q
            //    .SelectTableFields()
            //    .Select(c.MultiCurrency)
            //    .Select(c.EnableAdditionalCharges)
            //    .Select(c.EnableAdditionalConcessions)
            //);

            //data.Company.MultiCurrency = InvoiceSettings.MultiCurrency;
            //data.Company.EnableAdditionalCharges = InvoiceSettings.EnableAdditionalCharges;
            //data.Company.EnableAdditionalConcessions = InvoiceSettings.EnableAdditionalConcessions;

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
                     .Select(c.StateId)
                     .Select(c.Logo)
                     .Select(c.LogoHeight)
                     .Select(c.LogoWidth)
                     .Select(c.InvoiceHeaderImage)
                     .Select(c.InvoiceHeaderHeight)
                     .Select(c.InvoiceHeaderWidth)
                     .Select(c.InvoiceFooterImage)
                     .Select(c.InvoiceFooterHeight)
                     .Select(c.InvoiceFooterWidth)
                     .Select(c.InvoiceHeaderContent)
                     .Select(c.InvoiceFooterContent)
                     .Select(c.GSTIN)
                     .Select(c.PANNo)
                     .Select(c.CompanyDetails)
                     .Select(c.InvoiceTemplate)
                     .Select(c.InvoiceSuffix)
                     .Select(c.InvoicePrefix)
                     .Select(c.InvoiceTaxColumnIncluded)
                     );
            new CompanyDetailsRow();

            var InvoiceSettings = connection.TryById<CompanyDetailsRow>(1, q => q
                 .SelectTableFields()
                 .Select(c.MultiCurrency)
                 .Select(c.EnableAdditionalCharges)
                 .Select(c.EnableAdditionalConcessions)
                 );

            data.Company.MultiCurrency = InvoiceSettings.MultiCurrency;
            data.Company.EnableAdditionalCharges = InvoiceSettings.EnableAdditionalCharges;
            data.Company.EnableAdditionalConcessions = InvoiceSettings.EnableAdditionalConcessions;

            if (data.Company.EnableAdditionalCharges == true)
            {
                var qcha = SalesChargesRow.Fields;
                data.SalesCharges = connection.List<SalesChargesRow>(q => q
                    .SelectTableFields()
                    .Select(qcha.ChargesName)
                    .Select(qcha.ChargesPercentage)
                    .Where(qcha.SalesId == salesId));
            }

            if (data.Company.EnableAdditionalConcessions == true)
            {
                var qcha = SalesConcessionRow.Fields;
                data.SalesConcession = connection.List<SalesConcessionRow>(q => q
                    .SelectTableFields()
                    .Select(qcha.ConcessionName)
                    .Select(qcha.ConcessionAmount)
                    .Select(qcha.ConcessionPercentage)
                    .Where(qcha.SalesId == salesId));
            }

            data.taxable = data.Invoice.Taxable == null ? false : data.Invoice.Taxable.Value;

            if (data.Company.MultiCurrency == true)
            {
                if (data.Invoice.CurrencyConversion != null && data.Invoice.CurrencyConversion == true)
                {
                    data.taxable = false;

                    foreach (var pro in data.InvoiceProducts)
                    {
                        pro.Price = pro.Price * data.Invoice.Conversion;
                        pro.Discount = pro.Discount * data.Invoice.Conversion;
                        pro.Tax1Amount = pro.Tax1Amount * (Decimal)data.Invoice.Conversion;
                        pro.Tax2Amount = pro.Tax2Amount * (Decimal)data.Invoice.Conversion;
                        pro.LineTotal = pro.LineTotal * (Decimal)data.Invoice.Conversion;
                        pro.GrandTotal = pro.GrandTotal * (Decimal)data.Invoice.Conversion;
                    }
                    data.Invoice.PackagingCharges *= data.Invoice.Conversion;
                    data.Invoice.FreightCharges *= data.Invoice.Conversion;
                    data.Invoice.Advacne *= data.Invoice.Conversion;
                    data.Invoice.Roundup *= data.Invoice.Conversion;
                }
            }

            return data;
        }

        public void Customize(IHtmlToPdfOptions options)
        {
            // Customize HTML to PDF converter (WKHTML) parameters here if needed
            // options.MarginsAll = "2cm";
        }
    }

    public class SalesReportData
    {
        public Boolean taxable { get; set; }
        public Int32 id { get; set; }
        public String modtype { get; set; }
        public String Name { get; set; }
        public SalesRow Invoice { get; set; }
        public List<SalesProductsRow> InvoiceProducts { get; set; }
        //public List<QuotationTermsRow> QuotationTerms { get; set; }
        public Administration.UserRow Representative { get; set; }
        public ContactsRow Contacts { get; set; }
        public CompanyDetailsRow Company { get; set; }
        public Template.InvoiceTemplateRow Template { get; set; }
        public List<SalesTermsRow> SalesTerms { get; set; }
        public List<SalesChargesRow> SalesCharges { get; set; }
        public List<SalesConcessionRow> SalesConcession { get; set; }
    }
}
