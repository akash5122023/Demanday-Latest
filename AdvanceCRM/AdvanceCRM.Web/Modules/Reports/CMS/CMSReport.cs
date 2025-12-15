using _Ext;
using AdvanceCRM.Administration;
using AdvanceCRM.Contacts;
using AdvanceCRM.Masters;
using AdvanceCRM.Enquiry;
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
    [Report("Reports.CMSReport")]
    [ReportDesign(MVC.Views.Reports.CMS.CMSReport)]
    public class CMSReport : ListReportBase, IReport
    {
        public new CMSReportRequest Request { get; set; }

        public CMSReport(IRequestContext context, ISqlConnections connections)
            : base(context, connections)
        {
        }

        public CMSReport()
            : this(Dependency.Resolve<IRequestContext>(), Dependency.Resolve<ISqlConnections>())
        {
        }

        public object GetData()
        {
            using (var connection = SqlConnections.NewFor<CMSRow>())
            {
                return new CMSReportModel(connection, Request);
            }
        }
    }

    public class CMSReportModel : ListReportModelBase
    {
        public new CMSReportRequest Request { get; set; }
        public List<CMSRow> Open { get; set; }
        public List<CMSRow> Closed { get; set; }
        public List<CMSRow> All { get; set; }
        public int Telecalls { get; set; }
        public int Teleinterest { get; set; }
        public int visitno { get; set; }
        public int visitInterent { get; set; }
        public int Others { get; set; }
        public double TelecallsAmt { get; set; }
        public double TeleinterestAmt { get; set; }
        public double visitnoAmt { get; set; }
        public double visitInterentAmt { get; set; }
        public double OthersAmt { get; set; }
        //public int Telecalls { get; set; }
        public List<UserRow> Alluser { get; set; }
        public List<CMSRow> Won { get; set; }
        public List<CMSRow> Lost { get; set; }
        public List<CMSFollowupsRow> Followup { get; set; }
        public ContactsRow Customers { get; set; }
        public List<ContactsRow> Contacts { get; set; }
        public TeleCallingRow Telecall { get; set; }
        public List<TeleCallingRow> Telecaller { get; set; }
        public List<CMSRow> TeleCallInterest { get; set; }
        public List<VisitRow> Visits { get; set; }
        public List<CMSRow> visitor { get; set; }
        public List<CMSRow> other { get; set; }
        public UserRow Users { get; set; }
        public ProjectRow ProjectName { get; set; }
        public CompanyDetailsRow Company { get; set; }
        public List<CMSProductsRow> Products { get; set; }
        public List<ProductsDivisionRow> Divisions { get; set; }
        public List<CMSRow> Projects { get; set; }
        public List<EnquiryRow> TotEnq { get; set; } = new();
        public List<CMSRow> Hot { get; set; } = new();
        public List<CMSRow> Warm { get; set; } = new();
        public List<CMSRow> Cold { get; set; } = new();

        public CMSReportModel(IDbConnection connection, CMSReportRequest request)
        {

            Request = request;
            var e = CMSRow.Fields;
            var c = ContactsRow.Fields;
            var f = CMSFollowupsRow.Fields;
            var p = CMSProductsRow.Fields;
            var d = ProductsDivisionRow.Fields;
            var u = UserRow.Fields;
            var t = TeleCallingRow.Fields;
            var v = VisitRow.Fields;
            var pr = ProjectRow.Fields;


            if (Request.Type == CMSReportType.Customerwise)
            {
             Projects = connection.List<CMSRow>(q => q
            .SelectTableFields()
            .Select(e.Project)
            .Select(e.ContactsName)
            .Select(e.ComplaintComplaintType)
            .Select(e.Stage)
            .Select(e.Instructions)
            .Select(e.ExpectedCompletion)
            .Where(e.Date >= Request.DateFrom)
            .Where(e.Date <= Request.DateTo)
            .Where(e.ContactsId == Request.Contact.Value)
            );


                Customers = connection.TryFirst<ContactsRow>
                 (c.Id == Convert.ToInt32(Request.Contact.Value));
            }

            //else if (Request.Type == CMSReportType.ProjectWise )
            //{
            //    Projects = connection.List<CMSRow>(q => q
            //     .SelectTableFields()
            //     .Select(e.Project)
            //     .Select(e.ContactsName)
            //     .Select(e.ProductsName)
            //     .Select(e.AssignedToDisplayName)
            //     .Select(e.ComplaintComplaintType)
            //     .Select(e.Stage)
            //     .Select(e.Instructions)
            //     .Select(e.ExpectedCompletion)
            //     .Where(e.Date >= Request.DateFrom)
            //     .Where(e.Date <= Request.DateTo)
            //     .Where(e.ProjectId == Request.Project.Value)
            //     );

            //    ProjectName = connection.TryById<ProjectRow>(Request.Project.Value, q => q
            //   .SelectTableFields()
            //   .Select(pr.Project)
            //   .Where(pr.Id == Request.Project.Value)
            //   );
            //}

            else if (Request.Type == CMSReportType.ProjectWise && Request.Project.HasValue)
            {
                Projects = connection.List<CMSRow>(q => q
                 .SelectTableFields()
                 .Select(e.Project)
                 .Select(e.ContactsName)
                 .Select(e.ProductsName)
                 .Select(e.AssignedToDisplayName)
                 .Select(e.ComplaintComplaintType)
                 .Select(e.Stage)
                 .Select(e.Instructions)
                 .Select(e.ExpectedCompletion)
                 .Where(e.Date >= Request.DateFrom)
                 .Where(e.Date <= Request.DateTo)
                 .Where(e.ProjectId == Request.Project.Value)
                 );

                ProjectName = connection.TryById<ProjectRow>(Request.Project.Value, q => q
               .SelectTableFields()
               .Select(pr.Project)
               .Where(pr.Id == Request.Project.Value)
               );
            }

            //else if (Request.Type == CMSReportType.Representativewise)
            //{
            //    Followup = connection.List<CMSFollowupsRow>(q => q
            //     .SelectTableFields()
            //     .Select(f.FollowupNote)
            //     .Select(f.Details)
            //     .Select(f.FollowupDate)
            //     //.Select(f.CMSTotal)
            //     .Select(f.CMSContactsId)

            //     .Select(f.CMSId)
            //     .Where(f.FollowupDate >= Request.DateFrom)
            //     .Where(f.FollowupDate <=  Request.DateTo)
            //     .Where(f.CMSAssignedBy == Request.Representative.Value)
            //     );

            //    Products = connection.List<CMSProductsRow>(q => q
            //     .SelectTableFields()
            //     .Select(p.ProductsName)
            //     );

            //    Users = connection.TryById<UserRow>(Request.Representative.Value, q => q
            //     .SelectTableFields()
            //     .Select(u.UserId)
            //     .Select(u.Username)
            //     .Select(u.DisplayName)
            //     );

            //    Contacts = connection.List<ContactsRow>(q => q
            //        .SelectTableFields()
            //        .Select(c.Id)
            //        .Select(c.Name)
            //        .Select(c.Phone)
            //        );
            //}

            else
            {

                Open = connection.List<CMSRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where("Status = 1")
                     .Where(e.Date >= Request.DateFrom)
                     .Where(e.Date <= Request.DateTo)
                     );

                Closed = connection.List<CMSRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where("Status = 2")
                     .Where(e.Date >= Request.DateFrom)
                     .Where(e.Date <= Request.DateTo)
                     );

                All = connection.List<CMSRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where(e.Date >= Request.DateFrom)
                     .Where(e.Date <= Request.DateTo)
                     );

                //Won = connection.List<CMSRow>(q => q
                //     .SelectTableFields()
                //     .Select("Id")
                //     .Where("ClosingType = 1")
                //     .Where(e.Date >= Request.DateFrom)
                //     .Where(e.Date <= Request.DateTo)
                //     );

                //Lost = connection.List<CMSRow>(q => q
                //     .SelectTableFields()
                //     .Select("Id")
                //     .Where("ClosingType = 2")
                //     .Where(e.Date >= Request.DateFrom)
                //     .Where(e.Date <= Request.DateTo)
                //     );

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