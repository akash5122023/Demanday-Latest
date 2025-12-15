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

    [Report("Indent.PrintIndent")]
    [ReportDesign(MVC.Views.Sales.Indent.IndentPrint)]
    public class IndentReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;
        public IndentReport(ISqlConnections connections, IRequestContext context)
        {
            this._connections = connections;
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IndentReport()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }
        public Int32 Id { get; set; }
        public Boolean Taxable { get; set; }
        public String ModType { get; set; }
        public object GetData()
        {
            var data = new IndentReportData();

            using (var connection = _connections.NewFor<IndentRow>())
            {
                try
                {
                    data.taxable = this.Taxable;
                    data.id = this.Id;
                    data.modtype = this.ModType;

                    var o = IndentRow.Fields;

                    data.Indent = connection.TryById<IndentRow>(this.Id, q => q
                         .SelectTableFields()
                         .Select(o.Id)
                         .Select(o.Date)
                         .Select(o.ContactsName)
                         .Select(o.ContactsPhone)
                         .Select(o.ContactsAddress)
                         .Select(o.ContactsGstin)
                         .Select(o.ContactsPanNo)
                         );

                    data.Name = data.Indent.ContactsName;

                    var od = IndentProductsRow.Fields;
                    data.Products = connection.List<IndentProductsRow>(q => q
                        .SelectTableFields()
                        .Select(od.ProductsCode)
                        .Select(od.ProductsHsn)
                        .Select(od.ProductsName)
                        .Select(od.ProductsDescription)
                        .Select(od.Quantity)
                        .Select(od.Description)
                        .Where(od.IndentId == this.Id));


                    var it = Template.ChallanTemplateRow.Fields;                    
                    data.Template = connection.TryFirst<Template.ChallanTemplateRow>(q => q
                        .SelectTableFields()
                        .Select(it.TermsConditions)
                        .Where(it.CompanyId == this.Id)) ?? new Template.ChallanTemplateRow();

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
                         .Select(c.ChallanPrefix));

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

        }
    }

    public class IndentReportData
    {
        public Boolean taxable { get; set; }
        public Int32 id { get; set; }
        public String modtype { get; set; }
        public String Name { get; set; }
        public IndentRow Indent { get; set; }
        public List<IndentProductsRow> Products { get; set; }
        public Administration.UserRow Representative { get; set; }
        public ContactsRow Contacts { get; set; }
        public CompanyDetailsRow Company { get; set; }
        public Template.ChallanTemplateRow Template { get; set; }
    }
}