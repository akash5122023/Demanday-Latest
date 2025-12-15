
namespace AdvanceCRM.Services
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Services;
    using Serenity.Data;
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;

    [Report("CMS.PrintCMS")]
    [ReportDesign(MVC.Views.Services.CMS.CMSPrint)]
    public class CMSReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _sqlConnections;
        private readonly IRequestContext _context;

        public CMSReport(ISqlConnections connections, IRequestContext context)
        {
            _sqlConnections = connections;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public CMSReport()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }

        public Int32 Id { get; set; }
        public Boolean Taxable { get; set; }
        public String ModType { get; set; }

        public object GetData()
        {
            var data = new CMSReportData();

            using (var connection = _sqlConnections.NewFor<QuotationRow>())
            {
                data.taxable = this.Taxable;
                data.id = this.Id;
                data.modtype = this.ModType;

                var o = CMSRow.Fields;
                data.CMS = connection.TryById<CMSRow>(this.Id, q => q
                     .SelectTableFields()
                     .Select(o.Id)
                     .Select(o.CMSNo)
                     .Select(o.Date)
                     .Select(o.ContactsName)
                     .Select(o.ContactsPhone)
                     .Select(o.ContactsAddress)
                     .Select(o.DealerDealerName)
                     .Select(o.DealerEmail)
                     .Select(o.DealerPhone)
                     .Select(o.DealerAddress)
                     .Select(o.ProductsName)
                     .Select(o.Category)
                     .Select(o.ComplaintComplaintType)
                     );

                data.Name = data.CMS.ContactsName;


                var r = Administration.UserRow.Fields;
                data.Representative = connection.TryFirst<Administration.UserRow>(r.UserId == data.CMS.AssignedTo.Value)
                    ?? new Administration.UserRow();

                var c = CompanyDetailsRow.Fields;
                var userDef = _context?.User?.ToUserDefinition() as UserDefinition;

                data.Company = userDef != null
                    ? connection.TryById<CompanyDetailsRow>(userDef.CompanyId, q => q
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
                         .Select(c.DealerInCms)
                         )
                    : new CompanyDetailsRow();
            }

            return data;
        }

        public void Customize(IHtmlToPdfOptions options)
        {
            // you may customize HTML to PDF converter (WKHTML) parameters here, e.g. 
            // options.MarginsAll = "2cm";
        }
    }

    public class CMSReportData
    {
        public Boolean taxable { get; set; }
        public Int32 id { get; set; }
        public String modtype { get; set; }
        public String Name { get; set; }
        public CMSRow CMS { get; set; }
        public Administration.UserRow Representative { get; set; }
        public List<QuotationRow> QuotationMail { get; set; }
        public ContactsRow Contacts { get; set; }
        public CompanyDetailsRow Company { get; set; }
    }
}