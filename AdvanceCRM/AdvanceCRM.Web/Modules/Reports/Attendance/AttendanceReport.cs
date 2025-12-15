using _Ext;
using AdvanceCRM.Administration;
using AdvanceCRM.Attendance;
using AdvanceCRM.Masters;
using AdvanceCRM.Products;
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
    [Report("Reports.AttendanceReport")]
    [ReportDesign(MVC.Views.Reports.Attendance.AttendanceReport)]
    public class AttendanceReport : ListReportBase, IReport
    {
        public new AttendanceReportRequest Request { get; set; }

        public AttendanceReport(IRequestContext context, ISqlConnections connections)
            : base(context, connections)
        {
        }

        public AttendanceReport()
            : this(Dependency.Resolve<IRequestContext>(), Dependency.Resolve<ISqlConnections>())
        {
        }

        public object GetData()
        {
            using (var connection = SqlConnections.NewFor<AttendanceRow>())
            {
                return new AttendanceReportModel(connection, Request);
            }
        }
    }

    public class AttendanceReportModel : ListReportModelBase
    {
        public new AttendanceReportRequest Request { get; set; }
        public List<AttendanceRow> Attendance { get; set; }
        public List<UserRow> Users { get; set; }
        public UserRow User { get; set; }

        public CompanyDetailsRow Company { get; set; }

        public AttendanceReportModel(IDbConnection connection, AttendanceReportRequest request)
        {
            Request = request;
            var a = AttendanceRow.Fields;
            var u = UserRow.Fields;

            if (Request.Type == Reports.AttendanceReportType.Representativewise)
            {
                Attendance = connection.List<AttendanceRow>(q => q
               .SelectTableFields()
               .Select(a.NameDisplayName)
               .Select(a.DateNTime)
               .Select(a.Coordinates)
               .Select(a.PunchIn)
               .Select(a.PunchOut)
               .Select(a.Distance)
               .Where(a.Name==Request.Representative.Value)
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
                Attendance = connection.List<AttendanceRow>(q => q
                .SelectTableFields()
                .Select(a.NameDisplayName)
                .Select(a.DateNTime)
                .Select(a.Coordinates)
                .Select(a.PunchIn)
                .Select(a.PunchOut)
                .Select(a.Distance)
             //   .Where(a.Name==Request.
                .Where(new Criteria("CAST(DateNTime as DATE) >= " + Request.DateFrom.ToSqlDate()))
                .Where(new Criteria("CAST(DateNTime as DATE) <= " + Request.DateTo.ToSqlDate()))
                );

                Users = connection.List<UserRow>(q => q
                 .SelectTableFields()
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