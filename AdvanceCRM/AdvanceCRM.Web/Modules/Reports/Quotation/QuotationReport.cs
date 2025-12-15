using _Ext;
using AdvanceCRM.Administration;
using AdvanceCRM.Contacts;
using AdvanceCRM.Quotation;
using QuoProdRow = AdvanceCRM.Quotation.QuotationProductsRow;
using AdvanceCRM.Masters;
using AdvanceCRM.Services;
using AdvanceCRM.ThirdParty;
using Serenity.Data;
using Serenity.Reporting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Serenity.Services;
using Serenity.Extensions.DependencyInjection;

namespace AdvanceCRM.Reports
{
    [Report("Reports.QuotationReport")]
    [ReportDesign(MVC.Views.Reports.Quotation.QuotationReport)]
    public class QuotationReport : ListReportBase, IReport
    {
        public new LeadsReportRequest Request { get; set; }

        public QuotationReport(IRequestContext context, ISqlConnections connections)
            : base(context, connections)
        {
        }

        public QuotationReport()
            : this(Dependency.Resolve<IRequestContext>(), Dependency.Resolve<ISqlConnections>())
        {
        }

        public object GetData()
        {
            using (var connection = SqlConnections.NewFor<QuotationRow>())
            {
                return new QuotationReportModel(connection, Request);
            }
        }
    }

    public class QuotationReportModel : ListReportModelBase
    {
        public new LeadsReportRequest Request { get; set; }
        public List<QuotationRow> Open { get; set; }
        public List<QuotationRow> Closed { get; set; }
        public List<QuotationRow> All { get; set; }
        public List<QuotationRow> Won { get; set; }
        public List<QuotationRow> Lost { get; set; }
        public List<QuotationRow> closeunassigned { get; set; }
        public List<QuotationRow> Typeunassigned { get; set; }

        public List<QuotationRow> Hot { get; set; }
        public List<QuotationRow> Warm { get; set; }
        public List<QuotationRow> Cold { get; set; }


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
        
        public List<QuotationFollowupsRow> Followup { get; set; }
        public ContactsRow Customers { get; set; }
        public List<ContactsRow> Contacts { get; set; }
        public UserRow Users { get; set; }
        public CompanyDetailsRow Company { get; set; }
        public List<QuoProdRow> Products { get; set; }
        public List<ProductsDivisionRow> Divisions { get; set; }

        public QuotationReportModel(IDbConnection connection, LeadsReportRequest request)
        {
            Request = request;
            var e = QuotationRow.Fields;
            var c = ContactsRow.Fields;
            var f = QuotationFollowupsRow.Fields;
            var p = QuoProdRow.Fields;
            var d = ProductsDivisionRow.Fields;
            var u = UserRow.Fields;
            var t = TeleCallingRow.Fields;
            var v = VisitRow.Fields;

            if (Request.Type == Reports.LeadsReportType.Customerwise)
            {
                Followup = connection.List<QuotationFollowupsRow>(q => q
                 .SelectTableFields()
                 .Select(f.FollowupNote)
                 .Select(f.Details)
                 .Select(f.FollowupDate)
                 .Select(f.QuotationId)
                 .Where(f.FollowupDate >=  Request.DateFrom)
                 .Where(f.FollowupDate <=  Request.DateTo)
                 .Where(f.QuotationContactsId == Request.Contact.Value)
                 );

                Products = connection.List<QuoProdRow>(q => q
                 .SelectTableFields()
                 .Select(p.ProductsName)
                 );

                Customers = connection.TryFirst<ContactsRow>
                 (c.Id == Convert.ToInt32(Request.Contact.Value));
            }
            else if (Request.Type == Reports.LeadsReportType.Divisionwise)
            {
                Products = connection.List<QuoProdRow>(q => q
                 .SelectTableFields()
                 .Select(p.ProductsName)
                 .Select(p.Quantity)
                 .Select(p.LineTotal)
                 .Select(p.ProductsDivisionId)
                 .Where(p.QuotationDate >=  Request.DateFrom)
                 .Where(p.QuotationDate <=  Request.DateTo)
                 );

                Divisions = connection.List<ProductsDivisionRow>(q => q
                 .SelectTableFields()
                 .Select(d.Id)
                 .Select(d.ProductsDivision)
                 );
            }
            else if (Request.Type == Reports.LeadsReportType.LostReasons)
            {
                Lost = connection.List<QuotationRow>(q => q
                 .SelectTableFields()
                 .Select(e.LostReason)
                 .Where(e.Date >=  Request.DateFrom)
                 .Where(e.Date <=  Request.DateTo)
                 .Where(e.ClosingType == 2)
                 );
            }
            else if (Request.Type == Reports.LeadsReportType.Mediawise)
            {
                All = connection.List<QuotationRow>(q => q
                 .SelectTableFields()
                 .Select(e.Source)
                 .Select(e.SourceId)
                 .Where(e.Date >=  Request.DateFrom)
                 .Where(e.Date <=  Request.DateTo)
                 );
            }
            else if (Request.Type == Reports.LeadsReportType.Productwise)
            {
                Products = connection.List<QuoProdRow>(q => q
                 .SelectTableFields()
                 .Select(p.ProductsName)
                 .Select(p.Quantity)
                 .Select(p.LineTotal)
                 .Where(p.QuotationDate >=  Request.DateFrom)
                 .Where(p.QuotationDate <=  Request.DateTo)
                 );
            }
            else if (Request.Type == Reports.LeadsReportType.Representativewise)
            {
                Followup = connection.List<QuotationFollowupsRow>(q => q
                 .SelectTableFields()
                 .Select(f.FollowupNote)
                 .Select(f.Details)
                 .Select(f.FollowupDate)
                 .Select(f.QuotationTotal)
                 .Select(f.QuotationContactsId)
                 .Select(f.QuotationId)
                 .Where(f.FollowupDate >=  Request.DateFrom)
                 .Where(f.FollowupDate <=  Request.DateTo)
                 .Where(f.QuotationAssignedId == Request.Representative.Value)
                 );

                Products = connection.List<QuoProdRow>(q => q
                 .SelectTableFields()
                 .Select(p.ProductsName)
                 );

                Users = connection.TryById<UserRow>(Request.Representative.Value, q => q
                 .SelectTableFields()
                 .Select(u.UserId)
                 .Select(u.Username)
                 .Select(u.DisplayName)
                 );

                Contacts = connection.List<ContactsRow>(q => q
                    .SelectTableFields()
                    .Select(c.Id)
                    .Select(c.Name)
                    .Select(c.Phone)
                    );
            }
            else if (Request.Type == Reports.LeadsReportType.Detailed)
            {
               
                Alluser = connection.List<UserRow>(q => q
                  .SelectTableFields()
                      .Select(u.UserId)
                      .Select(u.DisplayName)
                      .Select(u.Username)
                      .Where(u.IsActive == 1)
                    );

               

                Open = connection.List<QuotationRow>(q => q
                    .SelectTableFields()
                     .Select(e.Id).Select(e.Total)
                    .Select(e.AssignedUsername)
                     .Select(e.AssignedDisplayName)
                     .Select(e.AssignedIsActive)
                    .Select(e.OwnerUsername)
                    .Where(e.Status == 1)
                    .Where(e.Date >= Request.DateFrom)
                    .Where(e.Date <= Request.DateTo)
                    );

                Hot = connection.List<QuotationRow>(q => q
                   .SelectTableFields()
                    .Select(e.Id).Select(e.Total)
                   .Select(e.AssignedUsername)
                    .Select(e.AssignedDisplayName)
                    .Select(e.AssignedIsActive)
                   .Select(e.OwnerUsername)
                   .Where(e.Type == 1)
                   .Where(e.Date >= Request.DateFrom)
                   .Where(e.Date <= Request.DateTo)
                   );
                Warm = connection.List<QuotationRow>(q => q
                  .SelectTableFields()
                   .Select(e.Id).Select(e.Total)
                  .Select(e.AssignedUsername)
                   .Select(e.AssignedDisplayName)
                   .Select(e.AssignedIsActive)
                  .Select(e.OwnerUsername)
                  .Where(e.Type == 2)
                  .Where(e.Date >= Request.DateFrom)
                  .Where(e.Date <= Request.DateTo)
                  );
               Typeunassigned = connection.List<QuotationRow>(q => q
                 .SelectTableFields()
                  .Select(e.Id).Select(e.Total)
                 .Select(e.AssignedUsername)
                  .Select(e.AssignedDisplayName)
                  .Select(e.AssignedIsActive)
                 .Select(e.OwnerUsername)
                 .Where(e.Type == "")
                 .Where(e.Date >= Request.DateFrom)
                 .Where(e.Date <= Request.DateTo)
                 );

                Cold = connection.List<QuotationRow>(q => q
                .SelectTableFields()
                 .Select(e.Id).Select(e.Total)
                .Select(e.AssignedUsername)
                 .Select(e.AssignedDisplayName)
                 .Select(e.AssignedIsActive)
                .Select(e.OwnerUsername)
                .Where(e.Type == 3)
                .Where(e.Date >= Request.DateFrom)
                .Where(e.Date <= Request.DateTo)
                );


                Closed = connection.List<QuotationRow>(q => q
                    .SelectTableFields()
                    .Select(e.Id).Select(e.Total)
                    .Select(e.AssignedUsername)
                    .Select(e.AssignedIsActive)
                     .Select(e.AssignedDisplayName)
                    .Select(e.OwnerUsername)
                    .Where(e.Status == 2)
                    .Where(e.Date >= Request.DateFrom)
                    .Where(e.Date <= Request.DateTo)
                     );

                All = connection.List<QuotationRow>(q => q
                     .SelectTableFields()
                     .Select(e.Id).Select(e.Total)
                    .Select(e.AssignedUsername)
                     .Select(e.AssignedDisplayName)
                     .Select(e.AssignedIsActive)
                    .Select(e.OwnerUsername)
                     .Where(e.Date >= Request.DateFrom)
                     .Where(e.Date <= Request.DateTo)
                     );

                Won = connection.List<QuotationRow>(q => q
                     .SelectTableFields()
                     .Select(e.Id).Select(e.Total)
                    .Select(e.AssignedUsername)
                     .Select(e.AssignedDisplayName)
                     .Select(e.AssignedIsActive)
                    .Select(e.OwnerUsername)
                     .Where(e.ClosingType == 1)
                     .Where(e.Date >= Request.DateFrom)
                     .Where(e.Date <= Request.DateTo)
                     );
                closeunassigned = connection.List<QuotationRow>(q => q
                    .SelectTableFields()
                    .Select(e.Id).Select(e.Total)
                   .Select(e.AssignedUsername)
                    .Select(e.AssignedDisplayName)
                    .Select(e.AssignedIsActive)
                   .Select(e.OwnerUsername)
                    .Where(e.ClosingType == 3 || e.ClosingType==0)
                    .Where(e.Date >= Request.DateFrom)
                    .Where(e.Date <= Request.DateTo)
                    );


                Lost = connection.List<QuotationRow>(q => q
                     .SelectTableFields()
                      .Select(e.Id).Select(e.Total)
                    .Select(e.AssignedUsername)
                    .Select(e.AssignedIsActive)
                    .Select(e.OwnerUsername)
                     .Select(e.AssignedDisplayName)
                     .Where(e.ClosingType == 2)
                     .Where(e.Date >= Request.DateFrom)
                     .Where(e.Date <= Request.DateTo)
                     );
            }
            else
            {
                Open = connection.List<QuotationRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where("Status = 1")
                     .Where(e.Date >=  Request.DateFrom)
                     .Where(e.Date <=  Request.DateTo)
                     );

                Closed = connection.List<QuotationRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where("Status = 2")
                     .Where(e.Date >=  Request.DateFrom)
                     .Where(e.Date <=  Request.DateTo)
                     );

                All = connection.List<QuotationRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where(e.Date >=  Request.DateFrom)
                     .Where(e.Date <=  Request.DateTo)
                     );

                Won = connection.List<QuotationRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where("ClosingType = 1")
                     .Where(e.Date >=  Request.DateFrom)
                     .Where(e.Date <=  Request.DateTo)
                     );

                Lost = connection.List<QuotationRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where("ClosingType = 2")
                     .Where(e.Date >=  Request.DateFrom)
                     .Where(e.Date <=  Request.DateTo)
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