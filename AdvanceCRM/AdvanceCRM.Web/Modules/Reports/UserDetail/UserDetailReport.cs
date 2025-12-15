using _Ext;
using AdvanceCRM.Administration;
using AdvanceCRM.Contacts;
using AdvanceCRM.Quotation;
using AdvanceCRM.Enquiry;
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
    [Report("Reports.UserDetailReport")]
    [ReportDesign(MVC.Views.Reports.UserDetail.UserDetailReport)]
    public class UserDetailReport : ListReportBase, IReport
    {
        public new LeadsReportRequest Request { get; set; }

        public UserDetailReport(IRequestContext context, ISqlConnections connections)
            : base(context, connections)
        {
        }

        public UserDetailReport()
            : this(Dependency.Resolve<IRequestContext>(), Dependency.Resolve<ISqlConnections>())
        {
        }

        public object GetData()
        {
            using (var connection = SqlConnections.NewFor<QuotationRow>())
            {
                return new UserDetailReportModel(connection, Request);
            }
        }
    }

    public class UserDetailReportModel : ListReportModelBase
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

        public List<EnquiryRow> EOpen { get; set; }
        public List<EnquiryRow> EClosed { get; set; }
        public List<EnquiryRow> EAll { get; set; }
        public List<EnquiryRow> EWon { get; set; }
        public List<EnquiryRow> ELost { get; set; }
        public List<EnquiryRow> Ecloseunassigned { get; set; }
        public List<EnquiryRow> ETypeunassigned { get; set; }

        public List<EnquiryRow> EHot { get; set; }
        public List<EnquiryRow> EWarm { get; set; }
        public List<EnquiryRow> ECold { get; set; }


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
        public List<QuotationProductsRow> Products { get; set; }
        public List<ProductsDivisionRow> Divisions { get; set; }

        public UserDetailReportModel(IDbConnection connection, LeadsReportRequest request)
        {
            Request = request;
            var e = EnquiryRow.Fields;
            var quo = QuotationRow.Fields;
            var c = ContactsRow.Fields;
            var f = QuotationFollowupsRow.Fields;
            var p = QuotationProductsRow.Fields;
            var d = ProductsDivisionRow.Fields;
            var u = UserRow.Fields;
            var t = TeleCallingRow.Fields;
            var v = VisitRow.Fields;

         
            
               
                Alluser = connection.List<UserRow>(q => q
                  .SelectTableFields()
                      .Select(u.UserId)
                      .Select(u.DisplayName)
                      .Select(u.Username)
                      .Where(u.IsActive == 1)
                    );
            //////

            Alluser = connection.List<UserRow>(q => q
                  .SelectTableFields()
                      .Select(u.UserId)
                      .Select(u.DisplayName)
                      .Select(u.Username)
                      .Where(u.IsActive == 1)
                    );

            //Telecaller = connection.List<TeleCallingRow>(q => q
            //      .SelectTableFields()
            //      .Select(t.Id)
            //      .Select(t.AssignedToUsername)
            //       .Select(t.AssignedToIsActive)

            //       .Select(t.AssignedToDisplayName)
            //       .Where(t.Date >= Request.DateFrom)
            //      .Where(t.Date <= Request.DateTo)
            //    );

            //TeleCallInterest = connection.List<EnquiryRow>(q => q
            //    .SelectTableFields()
            //    .Select(e.Id)
            //    .Select(e.Total)
            //    .Select(e.AssignedUsername)
            //    .Select(e.AssignedIsActive)
            //    .Select(e.OwnerUsername)
            //    .Select(e.AssignedDisplayName)
            //    .Where(e.Source == "TeleCalling")
            //    .Where(e.Date >= Request.DateFrom)
            //    .Where(e.Date <= Request.DateTo)
            //    );

            //Visits = connection.List<VisitRow>(q => q
            //     .SelectTableFields()
            //     .Select(v.Id)
            //     .Select(v.CreatedByDisplayName)
            //     .Select(v.CreatedByIsActive)
            //      .Where(v.DateNTime >= Request.DateFrom)
            //     .Where(v.DateNTime <= Request.DateTo)
            //   );

            //visitor = connection.List<EnquiryRow>(q => q
            //    .SelectTableFields()
            //    .Select(e.Id).Select(e.Total)
            //    .Select(e.AssignedUsername)
            //    .Select(e.AssignedIsActive)
            //     .Select(e.AssignedDisplayName)
            //    .Select(e.OwnerUsername)
            //    .Where(e.Source == "Visit")
            //    .Where(e.Date >= Request.DateFrom)
            //    .Where(e.Date <= Request.DateTo)
            //    );
            //other = connection.List<EnquiryRow>(q => q
            //    .SelectTableFields()
            //    .Select(e.Id).Select(e.Total)
            //    .Select(e.AssignedUsername)
            //     .Select(e.AssignedDisplayName)
            //     .Select(e.AssignedIsActive)
            //    .Select(e.OwnerUsername)
            //    .Where(e.Source != "Visit")
            //    .Where(e.Source != "TeleCalling")
            //    .Where(e.Date >= Request.DateFrom)
            //    .Where(e.Date <= Request.DateTo)
            //    );


            EOpen = connection.List<EnquiryRow>(q => q
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


            EClosed = connection.List<EnquiryRow>(q => q
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

            EAll = connection.List<EnquiryRow>(q => q
                 .SelectTableFields()
                 .Select(e.Id).Select(e.Total)
                .Select(e.AssignedUsername)
                 .Select(e.AssignedDisplayName)
                 .Select(e.AssignedIsActive)
                .Select(e.OwnerUsername)
                 .Where(e.Date >= Request.DateFrom)
                 .Where(e.Date <= Request.DateTo)
                 );

            EWon = connection.List<EnquiryRow>(q => q
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

            ELost = connection.List<EnquiryRow>(q => q
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

            //////


            Open = connection.List<QuotationRow>(q => q
                    .SelectTableFields()
                     .Select(quo.Id).Select(quo.Total)
                    .Select(quo.AssignedUsername)
                     .Select(quo.AssignedDisplayName)
                     .Select(quo.AssignedIsActive)
                    .Select(quo.OwnerUsername)
                    .Where(quo.Status == 1)
                    .Where(quo.Date >= Request.DateFrom)
                    .Where(quo.Date <= Request.DateTo)
                    );

                Hot = connection.List<QuotationRow>(q => q
                   .SelectTableFields()
                    .Select(quo.Id).Select(quo.Total)
                   .Select(quo.AssignedUsername)
                    .Select(quo.AssignedDisplayName)
                    .Select(quo.AssignedIsActive)
                   .Select(quo.OwnerUsername)
                   .Where(quo.Type == 1)
                   .Where(quo.Date >= Request.DateFrom)
                   .Where(quo.Date <= Request.DateTo)
                   );
                Warm = connection.List<QuotationRow>(q => q
                  .SelectTableFields()
                   .Select(quo.Id).Select(quo.Total)
                  .Select(quo.AssignedUsername)
                   .Select(quo.AssignedDisplayName)
                   .Select(quo.AssignedIsActive)
                  .Select(quo.OwnerUsername)
                  .Where(quo.Type == 2)
                  .Where(quo.Date >= Request.DateFrom)
                  .Where(quo.Date <= Request.DateTo)
                  );
               Typeunassigned = connection.List<QuotationRow>(q => q
                 .SelectTableFields()
                  .Select(quo.Id).Select(quo.Total)
                 .Select(quo.AssignedUsername)
                  .Select(quo.AssignedDisplayName)
                  .Select(quo.AssignedIsActive)
                 .Select(quo.OwnerUsername)
                 .Where(quo.Type == "")
                 .Where(quo.Date >= Request.DateFrom)
                 .Where(quo.Date <= Request.DateTo)
                 );

                Cold = connection.List<QuotationRow>(q => q
                .SelectTableFields()
                 .Select(quo.Id).Select(quo.Total)
                .Select(quo.AssignedUsername)
                 .Select(quo.AssignedDisplayName)
                 .Select(quo.AssignedIsActive)
                .Select(quo.OwnerUsername)
                .Where(quo.Type == 3)
                .Where(quo.Date >= Request.DateFrom)
                .Where(quo.Date <= Request.DateTo)
                );


                Closed = connection.List<QuotationRow>(q => q
                    .SelectTableFields()
                    .Select(quo.Id).Select(quo.Total)
                    .Select(quo.AssignedUsername)
                    .Select(quo.AssignedIsActive)
                     .Select(quo.AssignedDisplayName)
                    .Select(quo.OwnerUsername)
                    .Where(quo.Status == 2)
                    .Where(quo.Date >= Request.DateFrom)
                    .Where(quo.Date <= Request.DateTo)
                     );

                All = connection.List<QuotationRow>(q => q
                     .SelectTableFields()
                     .Select(quo.Id).Select(quo.Total)
                    .Select(quo.AssignedUsername)
                     .Select(quo.AssignedDisplayName)
                     .Select(quo.AssignedIsActive)
                    .Select(quo.OwnerUsername)
                     .Where(quo.Date >= Request.DateFrom)
                     .Where(quo.Date <= Request.DateTo)
                     );

                Won = connection.List<QuotationRow>(q => q
                     .SelectTableFields()
                     .Select(quo.Id).Select(quo.Total)
                    .Select(quo.AssignedUsername)
                     .Select(quo.AssignedDisplayName)
                     .Select(quo.AssignedIsActive)
                    .Select(quo.OwnerUsername)
                     .Where(quo.ClosingType == 1)
                     .Where(quo.Date >= Request.DateFrom)
                     .Where(quo.Date <= Request.DateTo)
                     );
                closeunassigned = connection.List<QuotationRow>(q => q
                    .SelectTableFields()
                    .Select(quo.Id).Select(quo.Total)
                   .Select(quo.AssignedUsername)
                    .Select(quo.AssignedDisplayName)
                    .Select(quo.AssignedIsActive)
                   .Select(quo.OwnerUsername)
                    .Where(quo.ClosingType == 3 || quo.ClosingType==0)
                    .Where(quo.Date >= Request.DateFrom)
                    .Where(quo.Date <= Request.DateTo)
                    );


                Lost = connection.List<QuotationRow>(q => q
                     .SelectTableFields()
                      .Select(quo.Id).Select(quo.Total)
                    .Select(quo.AssignedUsername)
                    .Select(quo.AssignedIsActive)
                    .Select(quo.OwnerUsername)
                     .Select(quo.AssignedDisplayName)
                     .Where(quo.ClosingType == 2)
                     .Where(quo.Date >= Request.DateFrom)
                     .Where(quo.Date <= Request.DateTo)
                     );
            
       


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