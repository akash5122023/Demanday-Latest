using _Ext;
using AdvanceCRM.Administration;
using AdvanceCRM.Contacts;
using AdvanceCRM.Quotation;
using AdvanceCRM.Masters;
using AdvanceCRM.Services;
using AdvanceCRM.ThirdParty;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Serenity.Extensions.DependencyInjection;

namespace AdvanceCRM.Reports
{
    [Report("Reports.SignInReport")]
    [ReportDesign(MVC.Views.Reports.SignInReport.SignInReport_)]
    public class SignInReport : ListReportBase, IReport
    {
        public new LeadsReportRequest Request { get; set; }

        public SignInReport(IRequestContext context, ISqlConnections connections)
            : base(context, connections)
        {
        }

        public SignInReport()
            : this(Dependency.Resolve<IRequestContext>(), Dependency.Resolve<ISqlConnections>())
        {
        }

        public object GetData()
        {
            using (var connection = SqlConnections.NewFor<LogInOutLogRow>())
            {
                return new SignInReportModel(connection, Request);
            }
        }
    }

    public class SignInReportModel : ListReportModelBase
    {
       
        public List<LogInOutLogRow> all { get; set; }

        public List<UserRow> Alluser { get; set; }

        public CompanyDetailsRow Company { get; set; }
        public new LeadsReportRequest Request { get; set; }


    
    //public int Telecalls { get; set; }


    public SignInReportModel(IDbConnection connection, LeadsReportRequest request)
        {
            Request = request;
            var l = LogInOutLogRow.Fields;
            var u = UserRow.Fields;
          
            {

                Alluser = connection.List<UserRow>(q => q
                .SelectTableFields()
                    .Select(u.UserId)
                    .Select(u.DisplayName)
                    .Select(u.Username)
                    .Where(u.IsActive == 1)
                  );

                all = connection.List<LogInOutLogRow>(q => q
                     .SelectTableFields()
                     .Select(l.Id)
                     .Select(l.Date)
                     .Select(l.Type)
                     .Select(l.UserId)
                     .Select(l.UserDisplayName)
                     .Where(l.UserId == Request.Representative.Value)
                     .Where(l.Date >= Request.DateFrom)
                     .Where(l.Date <= Request.DateTo)
                     ); ;
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