
namespace AdvanceCRM.Sales
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Purchase;
    
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;

    [Report("Invoice.FileInvoice")]
    [ReportDesign(MVC.Views.Sales.Invoice.InvoicePrint)]
    public class FileInvoiceReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public FileInvoiceReport(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public FileInvoiceReport()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }

        public Int32 Id { get; set; }
        public String ModType { get; set; }

        public object GetData()
        {
            var data = new InvoiceReportData();

            using (var connection = _connections.NewFor<InvoiceRow>())
            {
                data.id = this.Id;
                data.modtype = this.ModType;


                var o = InvoiceRow.Fields;

                data.Invoice = connection.TryById<InvoiceRow>(this.Id, q => q
                     .SelectTableFields()
                     .Select(o.Id)
                     .Select(o.InvoiceNo)
                     .Select(o.InvoiceN)
                     .Select(o.Date)
                     .Select(o.ContactsName)
                     .Select(o.ContactsEmail)
                     .Select(o.ContactsPhone)
                     .Select(o.ContactsAddress)
                     .Select(o.ContactsStateId)
                     .Select(o.ContactsGstin)
                     .Select(o.ContactsPanNo)
                     .Select(o.PackagingCharges)
                     .Select(o.FreightCharges)
                     .Select(o.BillingAddress)
                     .Select(o.Advacne)
                     .Select(o.DueDate)
                     .Select(o.DispatchDetails)
                     .Select(o.Roundup)
                     .Select(o.QuotationN)
                     .Select(o.Lines)
                     .Select(o.ContactPersonName)
                     .Select(o.ContactPersonPhone)
                     .Select(o.CurrencyConversion)
                     .Select(o.Conversion)
                     .Select(o.ToCurrency)
                     .Select(o.Taxable)
                     .Select(o.Subject)
                     .Select(o.MessageId)
                     .Select(o.Message)
                     .Select(o.PurchaseOrderNo)
                     .Select(o.PurchaseOrderDate)
                     .Select(o.Total)
                     .Select(o.Tax1)
                     .Select(o.Tax2)
                     .Select(o.GrandTotal)
                     );

                //data.PONO = Convert.ToInt32(o.PurchaseOrderNo);

                //var po = PurchaseOrderRow.Fields;
                //data.PurchaseOrder = connection.List<PurchaseOrderRow>(q => q
                //  .SelectTableFields()
                //  .Select(po.Date)
                //  .Select(po.Id ==data.PONO));

                data.Name = data.Invoice.ContactsName;

                var od = InvoiceProductsRow.Fields;
                data.InvoiceProducts = connection.List<InvoiceProductsRow>(q => q
                    .SelectTableFields()
                    .Select(od.ProductsCode)
                    .Select(od.ProductsHsn)
                    .Select(od.ProductsName)
                    .Select(od.ProductsDescription)
                    .Select(od.Price)
                    .Select(od.Quantity)
                    .Select(od.Discount)
                    .Select(od.TaxType1)
                    .Select(od.Percentage1)
                    .Select(od.Tax1Amount)
                    .Select(od.TaxType2)
                    .Select(od.Percentage2)
                    .Select(od.Tax2Amount)
                    .Select(od.DiscPrice)
                    .Select(od.LineTotal)
                    .Select(od.GrandTotal)
                    .Select(od.Description)
                    .Select(od.ProductsTechSpecs)
                    .Select(od.ProductsImage)
                    .Select(od.Unit)
                    .Where(od.InvoiceId == this.Id));

              

                var it = InvoiceTermsRow.Fields;
                data.InvoiceTerms = connection.List<InvoiceTermsRow>(q => q
                    .SelectTableFields()
                    .Select(it.Terms)
                    .Where(it.InvoiceId == this.Id));

                var r = Administration.UserRow.Fields;
                data.Representative = connection.TryFirst<Administration.UserRow>(r.UserId == data.Invoice.AssignedId.Value)
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
                    var qcha = InvoiceChargesRow.Fields;
                    data.InvoiceCharges = connection.List<InvoiceChargesRow>(q => q
                        .SelectTableFields()
                        .Select(qcha.ChargesName)
                        .Select(qcha.ChargesPercentage)
                        .Where(qcha.InvoiceId == this.Id));
                }

                if (data.Company.EnableAdditionalConcessions == true)
                {
                    var qcha = InvoiceConcessionRow.Fields;
                    data.InvoiceConcession = connection.List<InvoiceConcessionRow>(q => q
                        .SelectTableFields()
                        .Select(qcha.ConcessionName)
                        .Select(qcha.ConcessionAmount)
                        .Select(qcha.ConcessionPercentage)
                        .Where(qcha.InvoiceId == this.Id));
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
            }

            return data;
        }

        public void Customize(IHtmlToPdfOptions options)
        {
            // you may customize HTML to PDF converter (WKHTML) parameters here, e.g. 
            // options.MarginsAll = "2cm";
        }
    }

[Report("Invoice.PrintInvoice")]
[ReportDesign(MVC.Views.Sales.Invoice.InvoicePrint)]
    public class InvoiceReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public InvoiceReport(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public InvoiceReport()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }

        public Int32 Id { get; set; }
        public String ModType { get; set; }

        public object GetData()
        {
            var data = new InvoiceReportData();

            using (var connection = _connections.NewFor<InvoiceRow>())
            {
                data.id = this.Id;
                data.modtype = this.ModType;


                var o = InvoiceRow.Fields;

                data.Invoice = connection.TryById<InvoiceRow>(this.Id, q => q
                     .SelectTableFields()
                     .Select(o.Id)
                     .Select(o.InvoiceNo)
                     .Select(o.Date)
                     .Select(o.ContactsName)
                     .Select(o.ContactsEmail)
                     .Select(o.ContactsPhone)
                     .Select(o.ContactsAddress)
                     .Select(o.ContactsStateId)
                     .Select(o.ContactsGstin)
                     .Select(o.ContactsPanNo)
                     .Select(o.PackagingCharges)
                     .Select(o.FreightCharges)
                     .Select(o.BillingAddress)
                     .Select(o.Advacne)
                     .Select(o.DueDate)
                     .Select(o.DispatchDetails)
                     .Select(o.Roundup)
                     .Select(o.QuotationN)
                     .Select(o.Lines)
                     .Select(o.ContactPersonName)
                     .Select(o.ContactPersonPhone)
                     .Select(o.CurrencyConversion)
                     .Select(o.Conversion)
                     .Select(o.ToCurrency)
                     .Select(o.Taxable)
                     .Select(o.Subject)
                     .Select(o.MessageId)
                     .Select(o.Message)
                     .Select(o.PurchaseOrderNo)
                     .Select(o.PurchaseOrderDate)
                     .Select(o.Total)
                     .Select(o.Tax1)
                     .Select(o.Tax2)
                     .Select(o.GrandTotal)
                     );

                //data.PONO = Convert.ToInt32(o.PurchaseOrderNo);

                //var po = PurchaseOrderRow.Fields;
                //data.PurchaseOrder = connection.List<PurchaseOrderRow>(q => q
                //  .SelectTableFields()
                //  .Select(po.Date)
                //  .Select(po.Id ==data.PONO));

                data.Name = data.Invoice.ContactsName;

                var od = InvoiceProductsRow.Fields;
                data.InvoiceProducts = connection.List<InvoiceProductsRow>(q => q
                    .SelectTableFields()
                    .Select(od.ProductsCode)
                    .Select(od.ProductsHsn)
                    .Select(od.ProductsName)
                    .Select(od.ProductsDescription)
                    .Select(od.Price)
                    .Select(od.Quantity)
                    .Select(od.Discount)
                    .Select(od.TaxType1)
                    .Select(od.Percentage1)
                    .Select(od.Tax1Amount)
                    .Select(od.TaxType2)
                    .Select(od.Percentage2)
                    .Select(od.Tax2Amount)
                    .Select(od.DiscPrice)
                    .Select(od.LineTotal)
                    .Select(od.GrandTotal)
                    .Select(od.Description)
                    .Select(od.ProductsTechSpecs)
                    .Select(od.ProductsImage)
                    .Select(od.Unit)
                    .Where(od.InvoiceId == this.Id));



                var it = InvoiceTermsRow.Fields;
                data.InvoiceTerms = connection.List<InvoiceTermsRow>(q => q
                    .SelectTableFields()
                    .Select(it.Terms)
                    .Where(it.InvoiceId == this.Id));

                var r = Administration.UserRow.Fields;
                data.Representative = connection.TryFirst<Administration.UserRow>(r.UserId == data.Invoice.AssignedId.Value)
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
                    var qcha = InvoiceChargesRow.Fields;
                    data.InvoiceCharges = connection.List<InvoiceChargesRow>(q => q
                        .SelectTableFields()
                        .Select(qcha.ChargesName)
                        .Select(qcha.ChargesPercentage)
                        .Where(qcha.InvoiceId == this.Id));
                }

                if (data.Company.EnableAdditionalConcessions == true)
                {
                    var qcha = InvoiceConcessionRow.Fields;
                    data.InvoiceConcession = connection.List<InvoiceConcessionRow>(q => q
                        .SelectTableFields()
                        .Select(qcha.ConcessionName)
                        .Select(qcha.ConcessionAmount)
                        .Select(qcha.ConcessionPercentage)
                        .Where(qcha.InvoiceId == this.Id));
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
            }

            return data;
        }

        public void Customize(IHtmlToPdfOptions options)
        {
            // you may customize HTML to PDF converter (WKHTML) parameters here, e.g. 
            // options.MarginsAll = "2cm";
        }
    }


    public class InvoiceReportData
    {
        public Boolean taxable { get; set; }
        public Int32 id { get; set; }
         public Int32 PONO { get; set; }
        public String modtype { get; set; }
        public String Name { get; set; }
        public InvoiceRow Invoice { get; set; }
        public List<InvoiceProductsRow> InvoiceProducts { get; set; }
        public Administration.UserRow Representative { get; set; }
        public ContactsRow Contacts { get; set; }
        public CompanyDetailsRow Company { get; set; }
         public PurchaseOrderRow PurchaseOrder { get; set; }
        public Template.InvoiceTemplateRow Template { get; set; }
        public List<InvoiceTermsRow> InvoiceTerms { get; set; }
        public List<InvoiceChargesRow> InvoiceCharges { get; set; }
        public List<InvoiceConcessionRow> InvoiceConcession { get; set; }
    }
}