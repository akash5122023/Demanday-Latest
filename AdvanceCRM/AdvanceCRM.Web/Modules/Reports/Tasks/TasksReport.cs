using _Ext;
using AdvanceCRM.Administration;
using AdvanceCRM.Contacts;
using AdvanceCRM.Tasks;
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
    [Report("Reports.TasksReport")]
    [ReportDesign(MVC.Views.Reports.Tasks.TasksReport)]
    public class TasksReport : ListReportBase, IReport
    {
        public new LeadsReportRequest Request { get; set; }

        public TasksReport(IRequestContext context, ISqlConnections connections)
            : base(context, connections)
        {
        }

        public TasksReport()
            : this(Dependency.Resolve<IRequestContext>(), Dependency.Resolve<ISqlConnections>())
        {
        }

        public object GetData()
        {
            using (var connection = SqlConnections.NewFor<TasksRow>())
            {
                return new TasksReportModel(connection, Request);
            }
        }
    }

    public class TasksReportModel : ListReportModelBase
    {
       
        public List<TasksRow> all { get; set; }

        public List<UserRow> Alluser { get; set; }

        public UserRow Users { get; set; }

        public CompanyDetailsRow Company { get; set; }
        public new LeadsReportRequest Request { get; set; }


    
    //public int Telecalls { get; set; }


    public TasksReportModel(IDbConnection connection, LeadsReportRequest request)
        {
            Request = request;
            var l = TasksRow.Fields;
            var u = UserRow.Fields;
          
            {

                Alluser = connection.List<UserRow>(q => q
                .SelectTableFields()
                    .Select(u.UserId)
                    .Select(u.DisplayName)
                    .Select(u.Username)
                    .Where(u.IsActive == 1)
                    .Where(u.UserId== Request.Representative.Value)
                  );

                Users = connection.TryById<UserRow>(Request.Representative.Value, q => q
               .SelectTableFields()
               .Select(u.UserId)
               .Select(u.Username)
               .Select(u.DisplayName)
               );

                var dt1 = Request.DateFrom.ToString("yyyy-MM-dd 00:00:00.000");//string dt1 = DateTime.Now.ToString("yyyy-MM-dd 23:59:59.000");
                var dt2 = Request.DateTo.ToString("yyyy-MM-dd 23:59:59.000"); ;


                all = connection.List<TasksRow>(q => q
                     .SelectTableFields()
                     .Select(l.Id)
                     .Select(l.CreationDate)
                     .Select(l.ExpectedCompletion)
                     .Select(l.CompletionDate)
                     .Select(l.ProjectId)
                     .Select(l.Project)
                     .Select(l.Status)
                     .Select(l.StatusId)
                     .Select(l.Task)
                     .Select(l.Type)
                     .Select(l.AssignedByInsertUserId)
                     .Select(l.AssignedByDisplayName)
                     .Select(l.AssignedToInsertUserId)
                     .Select(l.AssignedToDisplayName)
                     .Where(l.AssignedTo == Request.Representative.Value)
                     .Where(l.CreationDate >= dt1)
                     .Where(l.CreationDate <= dt2)
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