using _Ext;
using AdvanceCRM.Administration;
using AdvanceCRM.Masters;
using AdvanceCRM.Products;
using AdvanceCRM.Purchase;
using AdvanceCRM.Contacts;
using AdvanceCRM.Sales;
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
    [Report("Reports.SalesReport")]
    [ReportDesign(MVC.Views.Reports.Sales.SalesReport)]
    public class SalesReport : ListReportBase, IReport
    {
        public new SalesReportRequest Request { get; set; }

        public SalesReport(IRequestContext context, ISqlConnections connections)
            : base(context, connections)
        {
        }

        public SalesReport()
            : this(Dependency.Resolve<IRequestContext>(), Dependency.Resolve<ISqlConnections>())
        {
        }

        public object GetData()
        {
            using (var connection = SqlConnections.NewFor<ProductsRow>())
            {
                return new SalesReportModel(connection, Request);
            }
        }
    }

    public class SalesReportModel : ListReportModelBase
    {
        public new SalesReportRequest Request { get; set; }

        public List<SalesRow> Closed { get; set; }
        public List<SalesRow> All { get; set; }
        public List<SalesFollowupsRow> Followup { get; set; }
        public ContactsRow Customers { get; set; }
        public List<ContactsRow> Contacts { get; set; }
        public UserRow Users { get; set; }

        public List<SalesRow> Open { get; set; }
    
        public List<SalesRow> Won { get; set; }
        public List<SalesRow> Lost { get; set; }
        public List<SalesRow> closeunassigned { get; set; }
        public List<SalesRow> Typeunassigned { get; set; }

        public List<UserRow> Alluser { get; set; }
        public CompanyDetailsRow Company { get; set; }
        public List<SalesProductsRow> Products { get; set; }
        public List<ProductsDivisionRow> Divisions { get; set; }

        public SalesReportModel(IDbConnection connection, SalesReportRequest request)
        {
            Request = request;
            var e = SalesRow.Fields;
            var c = ContactsRow.Fields;
            var f = SalesFollowupsRow.Fields;
            var p = SalesProductsRow.Fields;
            var d = ProductsDivisionRow.Fields;
            var u = UserRow.Fields;
          

            //var p = ProductsRow.Fields;
            //var pp = PurchaseProductsRow.Fields;
            var sp = SalesProductsRow.Fields;
            var prp = PurchaseReturnProductsRow.Fields;
            var srp = SalesReturnProductsRow.Fields;
            var cp = ChallanProductsRow.Fields;
            var stp = StockTransferProductsRow.Fields;

            if (Request.Type == Reports.SalesReportType.Customerwise)
            {
                Followup = connection.List<SalesFollowupsRow>(q => q
                 .SelectTableFields()
                 .Select(f.FollowupNote)
                 .Select(f.Details)
                 .Select(f.FollowupDate)
                 .Select(f.SalesId)
                 .Where(f.FollowupDate >= Request.DateFrom)
                 .Where(f.FollowupDate <= Request.DateTo)
                 .Where(f.SalesContactsId == Request.Contact.Value)
                 );

                Products = connection.List<SalesProductsRow>(q => q
                 .SelectTableFields()
                 .Select(p.ProductsName)
                 );

                Customers = connection.TryFirst<ContactsRow>
                 (c.Id == Convert.ToInt32(Request.Contact.Value));
            }
            else if (Request.Type == Reports.SalesReportType.Divisionwise)
            {
                Products = connection.List<SalesProductsRow>(q => q
                 .SelectTableFields()
                 .Select(p.ProductsName)
                 .Select(p.Quantity)
                 .Select(p.LineTotal)
                 .Select(p.ProductsDivisionId)
                 .Where(p.SalesDate >= Request.DateFrom)
                 .Where(p.SalesDate <= Request.DateTo)
                 );

                Divisions = connection.List<ProductsDivisionRow>(q => q
                 .SelectTableFields()
                 .Select(d.Id)
                 .Select(d.ProductsDivision)
                 );
            }           
            else if (Request.Type == Reports.SalesReportType.Mediawise)
            {
                All = connection.List<SalesRow>(q => q
                 .SelectTableFields()
                 .Select(e.Source)
                 .Select(e.SourceId)
                 .Where(e.Date >= Request.DateFrom)
                 .Where(e.Date <= Request.DateTo)
                 );
            }
            else if (Request.Type == Reports.SalesReportType.Productwise)
            {
                Products = connection.List<SalesProductsRow>(q => q
                 .SelectTableFields()
                 .Select(p.ProductsName)
                 .Select(p.Quantity)
                 .Select(p.LineTotal)
                 .Where(p.SalesDate >= Request.DateFrom)
                 .Where(p.SalesDate <= Request.DateTo)
                 );
            }
            else if (Request.Type == Reports.SalesReportType.Representativewise)
            {
                Followup = connection.List<SalesFollowupsRow>(q => q
                 .SelectTableFields()
                 .Select(f.FollowupNote)
                 .Select(f.Details)
                 .Select(f.FollowupDate)
                // .Select(f.sales)
              //  .Select(f.sales
                 .Select(f.SalesContactsId)
                 .Select(f.SalesId)
                 .Where(f.FollowupDate >= Request.DateFrom)
                 .Where(f.FollowupDate <= Request.DateTo)
                 .Where(f.SalesAssignedId == Request.Representative.Value)
                 );

                Products = connection.List<SalesProductsRow>(q => q
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
            //else if (Request.Type == Reports.SalesReportType.Detailed)
            //{

            //    Alluser = connection.List<UserRow>(q => q
            //      .SelectTableFields()
            //          .Select(u.UserId)
            //          .Select(u.DisplayName)
            //          .Select(u.Username)
            //          .Where(u.IsActive == 1)
            //        );


            //    Open = connection.List<SalesRow>(q => q
            //        .SelectTableFields()
            //         .Select(e.Id).Select(e.Total)
            //        .Select(e.AssignedUsername)
            //         .Select(e.AssignedDisplayName)
            //         .Select(e.AssignedIsActive)
            //        .Select(e.OwnerUsername)
            //        .Where(e.Status == 1)
            //        .Where(e.Date >= Request.DateFrom)
            //        .Where(e.Date <= Request.DateTo)
            //        );

               
            //    Typeunassigned = connection.List<SalesRow>(q => q
            //      .SelectTableFields()
            //       .Select(e.Id).Select(e.Total)
            //      .Select(e.AssignedUsername)
            //       .Select(e.AssignedDisplayName)
            //       .Select(e.AssignedIsActive)
            //      .Select(e.OwnerUsername)
            //      .Where(e.Type == "")
            //      .Where(e.Date >= Request.DateFrom)
            //      .Where(e.Date <= Request.DateTo)
            //      );               

            //    Closed = connection.List<SalesRow>(q => q
            //        .SelectTableFields()
            //        .Select(e.Id).Select(e.Total)
            //        .Select(e.AssignedUsername)
            //        .Select(e.AssignedIsActive)
            //         .Select(e.AssignedDisplayName)
            //        .Select(e.OwnerUsername)
            //        .Where(e.Status == 2)
            //        .Where(e.Date >= Request.DateFrom)
            //        .Where(e.Date <= Request.DateTo)
            //         );

            //    All = connection.List<SalesRow>(q => q
            //         .SelectTableFields()
            //         .Select(e.Id).Select(e.Total)
            //        .Select(e.AssignedUsername)
            //         .Select(e.AssignedDisplayName)
            //         .Select(e.AssignedIsActive)
            //        .Select(e.OwnerUsername)
            //         .Where(e.Date >= Request.DateFrom)
            //         .Where(e.Date <= Request.DateTo)
            //         );

             
            //    closeunassigned = connection.List<SalesRow>(q => q
            //        .SelectTableFields()
            //        .Select(e.Id).Select(e.Total)
            //       .Select(e.AssignedUsername)
            //        .Select(e.AssignedDisplayName)
            //        .Select(e.AssignedIsActive)
            //       .Select(e.OwnerUsername)                  
            //        .Where(e.Date >= Request.DateFrom)
            //        .Where(e.Date <= Request.DateTo)
            //        );
            //}
            else
            {
                Open = connection.List<SalesRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where("Status = 1")
                     .Where(e.Date >= Request.DateFrom)
                     .Where(e.Date <= Request.DateTo)
                     );

                Closed = connection.List<SalesRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
                     .Where("Status = 2")
                     .Where(e.Date >= Request.DateFrom)
                     .Where(e.Date <= Request.DateTo)
                     );

                All = connection.List<SalesRow>(q => q
                     .SelectTableFields()
                     .Select("Id")
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