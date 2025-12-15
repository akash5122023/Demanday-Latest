
namespace AdvanceCRM.Purchase
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Purchase;
    using AdvanceCRM.PurchaseOrder;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;

    [Report("PurchaseOrder.PrintPurchaseOrder")]
    [ReportDesign(MVC.Views.Purchase.PurchaseOrder.PurchaseOrderPrint)]
public class PurchaseOrderReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public PurchaseOrderReport(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public PurchaseOrderReport()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }

        public Int32 Id { get; set; }
        public Boolean Taxable { get; set; }
        public String ModType { get; set; }

        public object GetData()
        {
            var data = new PurchaseOrderReportData();

            using (var connection = _connections.NewFor<PurchaseOrderRow>())
            {
                data.taxable = this.Taxable;
                data.id = this.Id;
                data.modtype = this.ModType;


                var o = PurchaseOrderRow.Fields;

                data.PurchaseOrder = connection.TryById<PurchaseOrderRow>(this.Id, q => q
                     .SelectTableFields()
                     .Select(o.Id)
                     .Select(o.PurchaseOrderNo)
                     .Select(o.Date)
                     .Select(o.DueDate)
                     .Select(o.ContactsName)
                     .Select(o.ContactsPhone)
                     .Select(o.ContactsAddress)
                     .Select(o.ShippingAddress)
                     .Select(o.ContactsStateId)
                     .Select(o.Description)
                     .Select(o.ContactsGstin)
                     .Select(o.Conversion)
                     .Select(o.CurrencyConversion)
                     .Select(o.FromCurrency)
                     .Select(o.ToCurrency)
                     .Select(o.ContactsPanNo)
                     .Select(o.Lines)
                     );

                data.Name = data.PurchaseOrder.ContactsName;

                var od = PurchaseOrderProductsRow.Fields;
                data.PurchaseOrderProducts = connection.List<PurchaseOrderProductsRow>(q => q
                    .SelectTableFields()
                    .Select(od.ProductsCode)
                    .Select(od.ProductsHsn)
                    .Select(od.ProductsName)
                    .Select(od.ProductsDescription)
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
                    .Select(od.LineTotal)
                    .Select(od.GrandTotal)
                    .Select(od.ProductsImage)
                    .Select(od.ProductsDescription)
                    .Select(od.ProductsTechSpecs)

                    .Select(od.Unit)
                    .Where(od.PurchaseOrderId == this.Id));


                var it = PurchaseOrderTermsRow.Fields;
                data.PurchaseOrderTerms = connection.List<PurchaseOrderTermsRow>(q => q
                    .SelectTableFields()
                    .Select(it.Terms)
                    .Where(it.PurchaseOrderId == this.Id));


                var r = UserRow.Fields;
                data.Representative = connection.TryFirst<UserRow>(r.UserId == data.PurchaseOrder.AssignedId.Value)
                    ?? new UserRow();



                var c = CompanyDetailsRow.Fields;

                var userDef = Context?.User?.ToUserDefinition() as UserDefinition;
                var companyId = userDef?.CompanyId ?? 1;

                data.Company = connection.TryById<CompanyDetailsRow>(companyId, q => q
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
                     .Select(c.GSTIN)
                     .Select(c.PANNo)
                     .Select(c.StateId)
                     );

                var QuotationSettings = connection.TryById<CompanyDetailsRow>(1, q => q
                    .SelectTableFields()
                    .Select(c.MultiCurrency)
                    .Select(c.EnableAdditionalCharges)
                    .Select(c.EnableAdditionalConcessions)
                    );

                data.Company.MultiCurrency = QuotationSettings.MultiCurrency;
                if (data.Company.MultiCurrency == true)
                {
                    if (data.PurchaseOrder.CurrencyConversion != null && data.PurchaseOrder.CurrencyConversion == true)
                    {
                        data.taxable = false;
                        foreach (var pro in data.PurchaseOrderProducts)
                        {
                            pro.Price = pro.Price * data.PurchaseOrder.Conversion;
                            pro.Discount = pro.Discount * data.PurchaseOrder.Conversion;
                            pro.Tax1Amount = pro.Tax1Amount * (Decimal)data.PurchaseOrder.Conversion;
                            pro.Tax2Amount = pro.Tax2Amount * (Decimal)data.PurchaseOrder.Conversion;
                            pro.LineTotal = pro.LineTotal * (Decimal)data.PurchaseOrder.Conversion;
                            pro.GrandTotal = pro.GrandTotal * (Decimal)data.PurchaseOrder.Conversion;
                        }
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

    public class PurchaseOrderReportData
    {
        public Boolean taxable { get; set; }
        public Int32 id { get; set; }
        public String modtype { get; set; }
        public String Name { get; set; }
        public PurchaseOrderRow PurchaseOrder { get; set; }
        public List<PurchaseOrderProductsRow> PurchaseOrderProducts { get; set; }
        public UserRow Representative { get; set; }
        public List<PurchaseOrderRow> QuotationMail { get; set; }
        public ContactsRow Contacts { get; set; }
        public CompanyDetailsRow Company { get; set; }
        public List<PurchaseOrderTermsRow> PurchaseOrderTerms { get; set; }

    }
}