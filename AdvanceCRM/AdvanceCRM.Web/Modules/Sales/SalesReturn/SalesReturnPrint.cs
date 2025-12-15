
namespace AdvanceCRM.Sales
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
   
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;

    [Report("SalesReturn.PrintSalesReturn")]
    [ReportDesign(MVC.Views.Sales.SalesReturn.SalesReturnPrint)]
    public class SalesReturnReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public SalesReturnReport(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public SalesReturnReport()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }

        public Int32 Id { get; set; }
        public Boolean Taxable { get; set; }
        public String ModType { get; set; }

        public object GetData()
        {
            var data = new SalesReturnReportData();

            using (var connection = _connections.NewFor<SalesReturnRow>())
            {
                try
                {
                    data.taxable = this.Taxable;
                    data.id = this.Id;
                    data.modtype = this.ModType;


                    var o = SalesReturnRow.Fields;

                    data.SalesReturn = connection.TryById<SalesReturnRow>(this.Id, q => q
                         .SelectTableFields()
                         .Select(o.Id)
                         .Select(o.InvoiceNo)
                         .Select(o.InvoiceDate)
                         .Select(o.ContactsName)
                         .Select(o.ContactsPhone)
                         .Select(o.ContactsAddress)
                         .Select(o.ContactsEmail)
                         .Select(o.ContactsStateId)
                         .Select(o.ContactsGstin)
                         .Select(o.ContactsPanNo)
                         //.Select(o.Total)
                         //.Select(o.Tax1)
                         //.Select(o.Tax2)
                         //.Select(o.GrandTotal)
                         .Select(o.Amount)
                         .Select(o.Roundup)

                         );

                    if (data.SalesReturn != null)
                        data.Name = data.SalesReturn.ContactsName;

                    var od = SalesReturnProductsRow.Fields;
                    data.SalesReturnProducts = connection.List<SalesReturnProductsRow>(q => q
                        .SelectTableFields()
                        .Select(od.ProductsCode)
                        .Select(od.ProductsHsn)
                        .Select(od.ProductsName)
                        .Select(od.ProductsDescription)
                        .Select(od.Serial)
                        .Select(od.Price)
                        .Select(od.Quantity)
                        .Select(od.LineTotal)
                        .Select(od.Description)
                        .Where(od.SalesReturnId == this.Id));

                    // var it = Template.ChallanTemplateRow.Fields;
                    // data.Template = connection.TryFirst<Template.ChallanTemplateRow>( q => q
                    //     .SelectTableFields()
                    //     .Select(it.TermsConditions)
                    //.Where(it.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    //     );

                    var r = Administration.UserRow.Fields;
                    if (data.SalesReturn?.AssignedId != null)
                    {
                        data.Representative = connection.TryFirst<Administration.UserRow>(r.UserId == data.SalesReturn.AssignedId.Value)
                            ?? new Administration.UserRow();
                    }
                    else
                    {
                        data.Representative = new Administration.UserRow();
                    }

                    //var c = CompanyDetailsRow.Fields;

                    //var userDef = Context.User?.ToUserDefinition() as UserDefinition;
                    //var companyId = userDef?.CompanyId;

                    //if (companyId == null || companyId == 0)
                    //    companyId = 1;

                    //if (userDef != null)
                    //{
                    //    data.Company = connection.TryById<CompanyDetailsRow>(userDef.CompanyId, q => q
                    //         .SelectTableFields()
                    //     .Select(c.Name)
                    //     .Select(c.Slogan)
                    //     .Select(c.Address)
                    //     .Select(c.Phone)
                    //     .Select(c.Logo)
                    //     .Select(c.LogoHeight)
                    //     .Select(c.LogoWidth)
                    //     .Select(c.DcHeaderImage)
                    //     .Select(c.DcHeaderHeight)
                    //     .Select(c.DcHeaderWidth)
                    //     .Select(c.DcFooterImage)
                    //     .Select(c.DcFooterHeight)
                    //     .Select(c.DcFooterWidth)
                    //     .Select(c.DcHeaderContent)
                    //     .Select(c.DcFooterContent)
                    //     .Select(c.GSTIN)
                    //     .Select(c.PANNo)
                    //     //.Select(c.ChallanSuffix)
                    //     //.Select(c.ChallanPrefix)
                    //     );
                    //}
                    var c = CompanyDetailsRow.Fields;

                    var userDef = Context?.User?.ToUserDefinition();
                    var compId = userDef?.CompanyId;

                    if (compId == null || compId == 0)
                        compId = 1;


                    // data.Company = connection.TryById<CompanyDetailsRow>(((UserDefinition)Context.User.ToUserDefinition()).CompanyId, q => q
                    data.Company = connection.TryById<CompanyDetailsRow>(compId.Value, q => q
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

    public class SalesReturnReportData
    {
        public Boolean taxable { get; set; }
        public Int32 id { get; set; }
        public String modtype { get; set; }
        public String Name { get; set; }
        public SalesReturnRow SalesReturn { get; set; }
        public List<SalesReturnProductsRow> SalesReturnProducts { get; set; }
        //public List<QuotationTermsRow> QuotationTerms { get; set; }
        public Administration.UserRow Representative { get; set; }
        public ContactsRow Contacts { get; set; }
        public CompanyDetailsRow Company { get; set; }
        //public Template.ChallanTemplateRow Template { get; set; }
    }
}