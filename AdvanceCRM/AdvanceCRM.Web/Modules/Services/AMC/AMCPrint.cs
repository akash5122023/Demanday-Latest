
namespace AdvanceCRM.Services
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Services;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;

    [Report("AMC.PrintAMC")]
    [ReportDesign(MVC.Views.Services.AMC.AMCPrint)]
    public class AMCReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public AMCReport(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public AMCReport()
            : this(Serenity.Extensions.DependencyInjection.Dependency.Resolve<ISqlConnections>(),
                  Serenity.Extensions.DependencyInjection.Dependency.Resolve<IRequestContext>())
        {
        }

        public Int32 Id { get; set; }
        public Boolean Taxable { get; set; }
        public String ModType { get; set; }

        public object GetData()
        {
            var data = new AMCReportData();

            using (var connection = _connections.NewFor<AMCRow>())
            {
                data.taxable = this.Taxable;
                data.id = this.Id;
                data.modtype = this.ModType;

                var o = AMCRow.Fields;

                data.AMC = connection.TryById<AMCRow>(this.Id, q => q
                     .SelectTableFields()
                     .Select(o.Id)
                     .Select(o.AMCNo)
                     .Select(o.Date)
                     .Select(o.DueDate)
                     .Select(o.ContactsName)
                     .Select(o.ContactsPhone)
                     .Select(o.ContactsAddress)
                     .Select(o.Lines)
                     );

                data.Name = data.AMC.ContactsName;

                var it = AMCTermsRow.Fields;
                data.AMCTerms = connection.List<AMCTermsRow>(q => q
                    .SelectTableFields()
                    .Select(it.Terms)
                    .Where(it.AMCId == this.Id));

                var od = AMCProductsRow.Fields;
                data.AMCProducts = connection.List<AMCProductsRow>(q => q
                    .SelectTableFields()
                    .Select(od.ProductsCode)
                    .Select(od.ProductsName)
                    .Select(od.ProductsDescription)
                    .Select(od.Rate)
                    .Select(od.Quantity)
                    .Select(od.Visits)
                    .Select(od.Discount)
                    .Select(od.TaxType1)
                    .Select(od.Percentage1)
                    .Select(od.Tax1Amount)
                    .Select(od.TaxType2)
                    .Select(od.Percentage2)
                    .Select(od.Tax2Amount)
                    .Select(od.LineTotal)
                    .Select(od.GrandTotal)
                    .Where(od.AMCId == this.Id));

                var at = AMCTermsRow.Fields;
                data.AMCTerms = connection.List<AMCTermsRow>(q => q
                    .SelectTableFields()
                    .Select(at.Terms)
                    .Where(at.AMCId == this.Id));

                var qt = AMCVisitPlannerRow.Fields;
                data.Visist = connection.List<AMCVisitPlannerRow>(q => q
                    .SelectTableFields()
                    .Select(qt.VisitDate)
                    .Select(qt.AssignedToDisplayName)
                    .Where(qt.AMCId == this.Id));

                var r = Administration.UserRow.Fields;
                data.Representative = connection.TryFirst<Administration.UserRow>(r.UserId == data.AMC.AssignedId.Value)
                    ?? new Administration.UserRow();

                var c = CompanyDetailsRow.Fields;

                var user = this.Context?.User?.ToUserDefinition() as UserDefinition;
                if (user != null)
                    data.Company = connection.TryById<CompanyDetailsRow>(user.CompanyId, q => q
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
                     );
                else
                    data.Company = new CompanyDetailsRow();

            }

            return data;
        }

        public void Customize(IHtmlToPdfOptions options)
        {
            // you may customize HTML to PDF converter (WKHTML) parameters here, e.g. 
            // options.MarginsAll = "2cm";
        }
    }

    public class AMCReportData
    {
        public Boolean taxable { get; set; }
        public Int32 id { get; set; }
        public String modtype { get; set; }
        public String Name { get; set; }
        public AMCRow AMC { get; set; }
        public List<AMCProductsRow> AMCProducts { get; set; }
        public List<AMCVisitPlannerRow> Visist { get; set; }
        public Administration.UserRow Representative { get; set; }
        public CompanyDetailsRow Company { get; set; }
        public Template.AMCTemplateRow AMCTemplate { get; set; }
        public List<AMCTermsRow> AMCTerms { get; set; }
    }
}