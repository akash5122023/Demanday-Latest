using _Ext;
using AdvanceCRM.Administration;
using AdvanceCRM.Attendance;
using AdvanceCRM.Masters;
using AdvanceCRM.ThirdParty;
using Serenity;
using Serenity.Data;
using Serenity.Services;
using Serenity.Reporting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Serenity.Extensions.DependencyInjection;

namespace AdvanceCRM.Reports
{
    [Report("Reports.VisitReport")]
    [ReportDesign(MVC.Views.Reports.Visit.VisitReport)]
    public class VisitReport : ListReportBase, IReport
    {
        public VisitReport(IRequestContext context, ISqlConnections connections) : base(context, connections)
        {
        }

        public VisitReport() : this(Dependency.Resolve<IRequestContext>(), Dependency.Resolve<ISqlConnections>())
        {
        }
        public new VisitReportRequest Request { get; set; }

        public object GetData()
        {
            using (var connection = SqlConnections.NewFor<VisitRow>())
            {
                return new VisitReportModel(connection, Request);
            }
        }
    }

    public class VisitReportModel : ListReportModelBase
    {
        public new VisitReportRequest Request { get; set; }
        public List<VisitRow> visit { get; set; }
        public List<UserRow> Users { get; set; }
        public UserRow User { get; set; }

        public CompanyDetailsRow Company { get; set; }

        public VisitReportModel(IDbConnection connection, VisitReportRequest request)
        {
            Request = request;
            var a = VisitRow.Fields;
            var u = UserRow.Fields;

            if (Request.Type == Reports.AttendanceReportType.Representativewise)
            {
                visit = connection.List<VisitRow>(q => q
               .SelectTableFields()
               .Select(a.CompanyName)
                .Select(a.Name)
                .Select(a.MobileNo)
                .Select(a.Email)
                .Select(a.Purpose)
                .Select(a.DateNTime)
                .Select(a.CreatedByDisplayName)
               .Where(a.CreatedBy==Request.Representative.Value)
               .Where(new Criteria("CAST(DateNTime as DATE) >= " + Request.DateFrom.ToSqlDate()))
               .Where(new Criteria("CAST(DateNTime as DATE) <= " + Request.DateTo.ToSqlDate()))
               );

             
                User = connection.TryById<UserRow>(Request.Representative.Value, q => q
               .SelectTableFields()
               .Select(u.UserId)
               .Select(u.Username)
               .Select(u.DisplayName)
               );

            }
            else //if (Request.Type == Reports.AttendanceReportType.All)
            {
                visit = connection.List<VisitRow>(q => q
                .SelectTableFields()
                .Select(a.CompanyName)
                .Select(a.Name)
                .Select(a.MobileNo)
                .Select(a.Email)
                .Select(a.Purpose)  
                
                .Select(a.DateNTime)
                .Select(a.CreatedByDisplayName)
                .Where(new Criteria("CAST(DateNTime as DATE) >= " + Request.DateFrom.ToSqlDate()))
                .Where(new Criteria("CAST(DateNTime as DATE) <= " + Request.DateTo.ToSqlDate()))
                );

                Users = connection.List<UserRow>(q => q
                 .SelectTableFields()
                 .Select(u.UserId)
               .Select(u.Username)
               .Select(u.DisplayName)
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