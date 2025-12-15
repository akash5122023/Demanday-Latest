
namespace AdvanceCRM.Sales
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    using Serenity.Data;
    using Serenity.Extensions.DependencyInjection;
    using Serenity.Reporting;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;

    [Report("Inward.PrintInward")]
    [ReportDesign(MVC.Views.Sales.Inward.InwardPrint)]
    public class InwardReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;
        public InwardReport(ISqlConnections connections, IRequestContext context)
        {
            this._connections = connections;
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public InwardReport()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }
        public Int32 Id { get; set; }
        public Boolean Taxable { get; set; }
        public String ModType { get; set; }

        public object GetData()
        {
            var data = new InwardReportData();

            using (var connection = _connections.NewFor<InwardRow>())
            {
                try
                {
                    data.taxable = this.Taxable;
                    data.id = this.Id;
                    data.modtype = this.ModType;

                    var o = InwardRow.Fields;

                    data.Inward = connection.TryById<InwardRow>(this.Id, q => q
                         .SelectTableFields()
                         .Select(o.Id)
                         .Select(o.ChallanNo)
                         .Select(o.Date)
                         .Select(o.ContactsName)
                         .Select(o.ContactsPhone)
                         .Select(o.ContactsAddress)
                         .Select(o.ShippingAddress)
                         .Select(o.ContactsGstin)
                         .Select(o.ContactsPanNo)
                         .Select(o.PackagingCharges)
                         .Select(o.FreightCharges)
                         .Select(o.Advacne)
                         .Select(o.DueDate)
                         .Select(o.DispatchDetails)
                         );

                    data.Name = data.Inward.ContactsName;

                    var od = InwardProductsRow.Fields;
                    data.InwardProducts = connection.List<InwardProductsRow>(q => q
                        .SelectTableFields()
                        .Select(od.ProductsCode)
                        .Select(od.ProductsHsn)
                        .Select(od.ProductsName)
                        .Select(od.ProductsDescription)
                        .Select(od.Serial)
                        .Select(od.Price)
                        .Select(od.Quantity)
                        .Select(od.Discount)
                        .Select(od.LineTotal)
                        .Select(od.Description)
                        .Select(od.Unit)
                        .Where(od.InwardId == this.Id));


                    var userDef = Context?.User?.ToUserDefinition() as UserDefinition;
                    var companyId = userDef?.CompanyId ?? 1;

                    var it = Template.ChallanTemplateRow.Fields;
                    data.Template = connection.TryFirst<Template.ChallanTemplateRow>(q => q
                        .SelectTableFields()
                        .Select(it.TermsConditions)
                   .Where(it.CompanyId == this.Id)) ?? new Template.ChallanTemplateRow();

                    
                    var c = CompanyDetailsRow.Fields;               

                    data.Company = connection.TryById<CompanyDetailsRow>(companyId, q => q
                             .SelectTableFields()
                         .Select(c.Name)
                         .Select(c.Slogan)
                         .Select(c.Address)
                         .Select(c.Phone)
                         .Select(c.Logo)
                         .Select(c.LogoHeight)
                         .Select(c.LogoWidth)
                         .Select(c.DcHeaderImage)
                         .Select(c.DcHeaderHeight)
                         .Select(c.DcHeaderWidth)
                         .Select(c.DcFooterImage)
                         .Select(c.DcFooterHeight)
                         .Select(c.DcFooterWidth)
                         .Select(c.DcHeaderContent)
                         .Select(c.DcFooterContent)
                         .Select(c.GSTIN)
                         .Select(c.PANNo)
                         .Select(c.ChallanSuffix)
                         .Select(c.ChallanPrefix)
                         );
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.ToString());
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

    public class InwardReportData
    {
        public Boolean taxable { get; set; }
        public Int32 id { get; set; }
        public String modtype { get; set; }
        public String Name { get; set; }
        public InwardRow Inward { get; set; }
        public List<InwardProductsRow> InwardProducts { get; set; }
        //public List<QuotationTermsRow> QuotationTerms { get; set; }
        public Administration.UserRow Representative { get; set; }
        public ContactsRow Contacts { get; set; }
        public CompanyDetailsRow Company { get; set; }
        public Template.ChallanTemplateRow Template { get; set; }
    }
}