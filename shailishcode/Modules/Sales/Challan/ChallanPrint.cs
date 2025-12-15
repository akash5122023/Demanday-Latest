
namespace AdvanceCRM.Sales
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using System;
    using System.Collections.Generic;
    using Serenity.Services;
    using Serenity.Extensions.DependencyInjection;

    [Report("Challan.PrintChallan")]
    [ReportDesign(MVC.Views.Sales.Challan.ChallanPrint)]
    public class ChallanReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;


        public ChallanReport(

            ISqlConnections connections,
            IRequestContext context
          )
        {
            this._connections = connections;
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ChallanReport()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }
        public Int32 Id { get; set; }
        public Boolean Taxable { get; set; }
        public String ModType { get; set; }

        public object GetData()
        {
            var data = new ChallanReportData();

            using (var connection = _connections.NewFor<ChallanRow>())
            {
                try
                {
                    data.taxable = this.Taxable;
                    data.id = this.Id;
                    data.modtype = this.ModType;


                    var o = ChallanRow.Fields;

                    data.Challan = connection.TryById<ChallanRow>(this.Id, q => q
                         .SelectTableFields()
                         .Select(o.Id)
                         .Select(o.ChallanNo)
                         .Select(o.Date)
                         .Select(o.ContactsName)
                         .Select(o.ContactsPhone)
                         .Select(o.ContactsAddress)
                         .Select(o.ContactsStateId)
                         .Select(o.ShippingAddress)
                         .Select(o.ContactsGstin)
                         .Select(o.ContactsPanNo)
                         .Select(o.PackagingCharges)
                         .Select(o.FreightCharges)
                         .Select(o.Advacne)
                         .Select(o.DueDate)
                         .Select(o.DispatchDetails)
                         );

                    data.Name = data.Challan.ContactsName;

                    var od = ChallanProductsRow.Fields;
                    data.ChallanProducts = connection.List<ChallanProductsRow>(q => q
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
                        .Select(od.LineTotal)
                        .Select(od.Description)
                        .Select(od.Unit)
                        .Where(od.ChallanId == this.Id));

                    var it = Template.ChallanTemplateRow.Fields;

                    //var userDefinition = Context?.User?.ToUserDefinition() as UserDefinition;
                    //var companyId = userDefinition?.CompanyId ?? 0; // or use a default/fallback ID
                    var userDefination = Context?.User?.ToUserDefinition();
                    var companyId = userDefination?.CompanyId;

                    if (companyId == null || companyId == 0)
                        companyId = 1;


                    data.Template = connection.TryFirst<Template.ChallanTemplateRow>( q => q
                        .SelectTableFields()
                        .Select(it.TermsConditions)
                   //.Where(it.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                   //     );
                   .Where(it.CompanyId == companyId.Value));

                    var r = Administration.UserRow.Fields;
                    data.Representative = connection.TryFirst<Administration.UserRow>(r.UserId == data.Challan.AssignedId.Value)
                        ?? new Administration.UserRow();

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
                         .Select(c.ChallanTaxColumnIncluded)
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

    public class ChallanReportData
    {
        public Boolean taxable { get; set; }
        public Int32 id { get; set; }
        public String modtype { get; set; }
        public String Name { get; set; }
        public ChallanRow Challan { get; set; }
        public List<ChallanProductsRow> ChallanProducts { get; set; }
        //public List<QuotationTermsRow> QuotationTerms { get; set; }
        public Administration.UserRow Representative { get; set; }
        public ContactsRow Contacts { get; set; }
        public CompanyDetailsRow Company { get; set; }
        public Template.ChallanTemplateRow Template { get; set; }
    }
}