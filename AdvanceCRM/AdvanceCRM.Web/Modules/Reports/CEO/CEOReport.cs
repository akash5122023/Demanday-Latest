using _Ext;
using AdvanceCRM.Administration;
using AdvanceCRM.Contacts;
using AdvanceCRM.Enquiry;
using AdvanceCRM.Quotation;
using AdvanceCRM.Masters;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Serenity.Services;

namespace AdvanceCRM.Reports
{
    [Report("Reports.CEOReport")]
    [ReportDesign(MVC.Views.Reports.CEO.CEOReport)]
    public class CEOReport : ListReportBase, IReport
    {
        public new LeadsReportRequest Request { get; set; }

        public CEOReport(IRequestContext context, ISqlConnections connections)
            : base(context, connections)
        {
        }

        public CEOReport()
            : this(Dependency.Resolve<IRequestContext>(), Dependency.Resolve<ISqlConnections>())
        {
        }

        public object GetData()
        {
            using (var connection = SqlConnections.NewFor<EnquiryRow>())
            {
                return new CEOReportModel(connection, Request);
            }
        }

    }

    public class CEOReportModel : ListReportModelBase
    {
        public new LeadsReportRequest Request { get; set; }
        public List<EnquiryRow> Open { get; set; } = new();
        public List<EnquiryRow> Closed { get; set; } = new();
        public List<EnquiryRow> All { get; set; } = new();
        public List<EnquiryRow> TotEnq { get; set; } = new();
        public List<EnquiryRow> Won { get; set; } = new();
        public List<EnquiryRow> OpenAmt { get; set; } = new();
        public List<EnquiryRow> CloseAmt { get; set; } = new();
        public List<EnquiryRow> Hot { get; set; } = new();
        public List<EnquiryRow> Warm { get; set; } = new();
        public List<EnquiryRow> Cold { get; set; } = new();
        public List<EnquiryRow> Lost { get; set; } = new();
        public List<EnquiryFollowupsRow> Followup { get; set; } = new();
        public ContactsRow Customers { get; set; }
        public List<EnquiryRow> Total { get; set; } = new();
        public List<ContactsRow> Contacts { get; set; } = new();
        public UserRow Users { get; set; }
        public CompanyDetailsRow Company { get; set; }
        public List<EnquiryProductsRow> Products { get; set; } = new();
        public List<ProductsDivisionRow> Divisions { get; set; } = new();

        public CEOReportModel(IDbConnection connection, LeadsReportRequest request)
        {
            Request = request;
            var e = EnquiryRow.Fields;
            var c = ContactsRow.Fields;
            var f = EnquiryFollowupsRow.Fields;
            var p = EnquiryProductsRow.Fields;
            var d = ProductsDivisionRow.Fields;
            var u = UserRow.Fields;

             {
                Open = connection.List<EnquiryRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where("Status = 1")
                     .Where(e.Date >= Request.DateFrom)
                     .Where(e.Date <= Request.DateTo)
                     );

                Closed = connection.List<EnquiryRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where("Status = 2")
                     .Where(e.Date >= Request.DateFrom)
                     .Where(e.Date <= Request.DateTo)
                     );

                All = connection.List<EnquiryRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where(e.Date >= Request.DateFrom)
                     .Where(e.Date <= Request.DateTo)
                     );

                Won = connection.List<EnquiryRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where("ClosingType = 1")
                     .Where(e.Date >= Request.DateFrom)
                     .Where(e.Date <= Request.DateTo)
                     );

                Lost = connection.List<EnquiryRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where("ClosingType = 2")
                     .Where(e.Date >= Request.DateFrom)
                     .Where(e.Date <= Request.DateTo)
                     );

            }


            var cmp = CompanyDetailsRow.Fields;
            Company = connection.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.Name)
                .Select(cmp.Slogan)
                .Select(cmp.Address)
                .Select(cmp.Phone)
                .Select(cmp.Logo)
                .Select(cmp.LogoHeight)
                .Select(cmp.LogoWidth)
                );
        }
    }
}