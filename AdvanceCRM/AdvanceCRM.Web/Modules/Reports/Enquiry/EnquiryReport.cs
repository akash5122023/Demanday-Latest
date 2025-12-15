using _Ext;
using AdvanceCRM.Administration;
using AdvanceCRM.Contacts;
using AdvanceCRM.Enquiry;
using Serenity.Extensions.DependencyInjection;
using EnquiryProductsRow = AdvanceCRM.Enquiry.EnquiryProductsRow;
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
    [Report("Reports.EnquiryReport")]
    [ReportDesign(MVC.Views.Reports.Enquiry.EnquiryReport)]
    public class EnquiryReport : ListReportBase, IReport
    {
        public new LeadsReportRequest Request { get; set; }

        public EnquiryReport(IRequestContext context, ISqlConnections connections)
            : base(context, connections)
        {
        }

        public EnquiryReport()
            : this(Dependency.Resolve<IRequestContext>(), Dependency.Resolve<ISqlConnections>())
        {
        }

        public object GetData()
        {
            using (var connection = SqlConnections.NewFor<EnquiryRow>())
            {
                return new EnquiryReportModel(connection, Request);
            }
        }
    }

    public class EnquiryReportModel : ListReportModelBase
    {
        public new LeadsReportRequest Request { get; set; }
        public List<EnquiryRow> Open { get; set; }
        public List<EnquiryRow> Closed { get; set; }
        public List<EnquiryRow> All { get; set; }
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
        public List<EnquiryRow> Won { get; set; }
        public List<EnquiryRow> Lost { get; set; }
        public List<EnquiryFollowupsRow> Followup { get; set; }
        public ContactsRow Customers { get; set; }
        public List<ContactsRow> Contacts { get; set; }
        public TeleCallingRow Telecall { get; set; }
        public List<TeleCallingRow> Telecaller { get; set; }
        public List<EnquiryRow> TeleCallInterest { get; set; }
        public List<VisitRow> Visits { get; set; }
        public List<EnquiryRow> visitor { get; set; }
        public List<EnquiryRow> other { get; set; }
        public UserRow Users { get; set; }        
        public CompanyDetailsRow Company { get; set; }
        public List<EnquiryProductsRow> Products { get; set; } = new();
        public List<ProductsDivisionRow> Divisions { get; set; } = new();
        public List<EnquiryRow> TotEnq { get; set; } = new();
        public List<EnquiryRow> Hot { get; set; } = new();
        public List<EnquiryRow> Warm { get; set; } = new();
        public List<EnquiryRow> Cold { get; set; } = new();

        public EnquiryReportModel(IDbConnection connection, LeadsReportRequest request)
        {

            Request = request;
            var e = EnquiryRow.Fields;
            var c = ContactsRow.Fields;
            var f = EnquiryFollowupsRow.Fields;
            var p = EnquiryProductsRow.Fields;
            var d = ProductsDivisionRow.Fields;
            var u = UserRow.Fields;
            var t = TeleCallingRow.Fields;
            var v = VisitRow.Fields;

            TotEnq = connection.List<EnquiryRow>(q => q
                .SelectTableFields()
                .Select(e.Id)
                .Select(e.Total)
                .Select(e.Source)
                .Where(e.Date >= Request.DateFrom)
                .Where(e.Date <= Request.DateTo));

            Hot = connection.List<EnquiryRow>(q => q
                .SelectTableFields()
                .Select(e.Id)
                .Select(e.Total)
                .Where(e.Type == (int)EnquiryTypeMaster.Hot)
                .Where(e.Date >= Request.DateFrom)
                .Where(e.Date <= Request.DateTo));

            Warm = connection.List<EnquiryRow>(q => q
                .SelectTableFields()
                .Select(e.Id)
                .Select(e.Total)
                .Where(e.Type == (int)EnquiryTypeMaster.Warm)
                .Where(e.Date >= Request.DateFrom)
                .Where(e.Date <= Request.DateTo));

            Cold = connection.List<EnquiryRow>(q => q
                .SelectTableFields()
                .Select(e.Id)
                .Select(e.Total)
                .Where(e.Type == (int)EnquiryTypeMaster.Cold)
                .Where(e.Date >= Request.DateFrom)
                .Where(e.Date <= Request.DateTo));

            if (Request.Type == Reports.LeadsReportType.Customerwise)
            {
                Followup = connection.List<EnquiryFollowupsRow>(q => q
                 .SelectTableFields()
                 .Select(f.FollowupNote)
                 .Select(f.Details)
                 .Select(f.FollowupDate)
                 .Select(f.EnquiryId)
                 .Where(f.FollowupDate >= Request.DateFrom)
                 .Where(f.FollowupDate <=  Request.DateTo)
                 .Where(f.EnquiryContactsId == Request.Contact.Value)
                 );

                Products = connection.List<EnquiryProductsRow>(q => q
                 .SelectTableFields()
                 .Select(p.ProductsName)
                 );

                Customers = connection.TryFirst<ContactsRow>
                 (c.Id == Convert.ToInt32(Request.Contact.Value));
            }
            else if (Request.Type == Reports.LeadsReportType.Divisionwise)
            {
                Products = connection.List<EnquiryProductsRow>(q => q
                 .SelectTableFields()
                 .Select(p.ProductsName)
                 .Select(p.Quantity)
                 .Select(p.LineTotal)
                 .Select(p.ProductsDivisionId)
                 .Where(p.EnquiryDate >= Request.DateFrom)
                 .Where(p.EnquiryDate <=  Request.DateTo)
                 );

                Divisions = connection.List<ProductsDivisionRow>(q => q
                 .SelectTableFields()
                 .Select(d.Id)
                 .Select(d.ProductsDivision)
                 );
            }
            else if (Request.Type == Reports.LeadsReportType.LostReasons)
            {
                Lost = connection.List<EnquiryRow>(q => q
                 .SelectTableFields()
                 .Select(e.LostReason)
                 .Where(e.Date >= Request.DateFrom)
                 .Where(e.Date <=  Request.DateTo)
                 .Where(e.ClosingType == 2)
                 );
            }
            else if (Request.Type == Reports.LeadsReportType.Mediawise)
            {
                All = connection.List<EnquiryRow>(q => q
                 .SelectTableFields()
                 .Select(e.Source)
                 .Select(e.SourceId)
                 .Where(e.Date >= Request.DateFrom)
                 .Where(e.Date <=  Request.DateTo)
                 );
            }
            else if (Request.Type == Reports.LeadsReportType.Productwise)
            {
                Products = connection.List<EnquiryProductsRow>(q => q
                 .SelectTableFields()
                 .Select(p.ProductsName)
                 .Select(p.Quantity)
                 .Select(p.LineTotal)
                 .Where(p.EnquiryDate >= Request.DateFrom)
                 .Where(p.EnquiryDate <=  Request.DateTo)
                 );
            }
            else if (Request.Type == Reports.LeadsReportType.Representativewise)
            {
                Followup = connection.List<EnquiryFollowupsRow>(q => q
                 .SelectTableFields()
                 .Select(f.FollowupNote)
                 .Select(f.Details)
                 .Select(f.FollowupDate)
                 .Select(f.EnquiryTotal)
                 .Select(f.EnquiryContactsId)
                
                 .Select(f.EnquiryId)
                 .Where(f.FollowupDate >= Request.DateFrom)
                 .Where(f.FollowupDate <=  Request.DateTo)
                 .Where(f.EnquiryAssignedId == Request.Representative.Value)
                 );

                Products = connection.List<EnquiryProductsRow>(q => q
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
            else if(Request.Type==Reports.LeadsReportType.Detailed)
            {


                Telecalls = connection.Count<TeleCallingRow>(t.Date >= Request.DateFrom && t.Date <= Request.DateTo);
                Teleinterest = connection.Count<EnquiryRow>(e.Source == "TeleCalling" && e.Date >= Request.DateFrom && e.Date <= Request.DateTo);
                visitno = connection.Count<VisitRow>(v.DateNTime >= Request.DateFrom && v.DateNTime <= Request.DateTo);
                visitInterent = connection.Count<EnquiryRow>(e.Source == "Visit" && e.Date >= Request.DateFrom && e.Date <= Request.DateTo);
                Others = connection.Count<EnquiryRow>(e.Source == "TeleCalling" && e.Source != "Visit" && e.Date >= Request.DateFrom && e.Date <= Request.DateTo);

                //TelecallsAmt = TeleCallInterest.Total.Where(t.Date >= Request.DateFrom && t.Date <= Request.DateTo);
                //TeleinterestAmt = connection.Count<EnquiryRow>(e.Source == "TeleCalling" && e.Date >= Request.DateFrom && e.Date <= Request.DateTo);
                //visitnoAmt = connection.Count<VisitRow>(v.DateNTime >= Request.DateFrom && v.DateNTime <= Request.DateTo);
                //visitInterentAmt = connection.Count<EnquiryRow>(e.Source == "Visit" && e.Date >= Request.DateFrom && e.Date <= Request.DateTo);
                //OthersAmt = connection.Count<EnquiryRow>(e.Source == "TeleCalling" && e.Source != "Visit" && e.Date >= Request.DateFrom && e.Date <= Request.DateTo);


                Alluser = connection.List<UserRow>(q => q
                  .SelectTableFields()
                      .Select(u.UserId)
                      .Select(u.DisplayName)
                      .Select(u.Username)
                      .Where(u.IsActive == 1)
                    );

                Telecaller = connection.List<TeleCallingRow>(q => q
                      .SelectTableFields()
                      .Select(t.Id)
                      .Select(t.AssignedToUsername)
                       .Select(t.AssignedToIsActive)

                       .Select(t.AssignedToDisplayName)
                       .Where(t.Date >= Request.DateFrom)
                      .Where(t.Date <= Request.DateTo)
                    );

                TeleCallInterest = connection.List<EnquiryRow>(q => q
                    .SelectTableFields()
                    .Select(e.Id)
                    .Select(e.Total)
                    .Select(e.AssignedUsername)
                    .Select(e.AssignedIsActive)
                    .Select(e.OwnerUsername)
                    .Select(e.AssignedDisplayName)
                    .Where(e.Source == "TeleCalling")
                    .Where(e.Date >= Request.DateFrom)
                    .Where(e.Date <= Request.DateTo)
                    );

                Visits = connection.List<VisitRow>(q => q
                     .SelectTableFields()
                     .Select(v.Id)
                     .Select(v.CreatedByDisplayName)
                     .Select(v.CreatedByIsActive)
                      .Where(v.DateNTime >= Request.DateFrom)
                     .Where(v.DateNTime <= Request.DateTo)
                   );

                visitor = connection.List<EnquiryRow>(q => q
                    .SelectTableFields()
                    .Select(e.Id).Select(e.Total)
                    .Select(e.AssignedUsername)
                    .Select(e.AssignedIsActive)
                     .Select(e.AssignedDisplayName)
                    .Select(e.OwnerUsername)
                    .Where(e.Source == "Visit")
                    .Where(e.Date >= Request.DateFrom)
                    .Where(e.Date <= Request.DateTo)
                    );

                other = connection.List<EnquiryRow>(q => q
                    .SelectTableFields()
                    .Select(e.Id).Select(e.Total)
                    .Select(e.AssignedUsername)
                     .Select(e.AssignedDisplayName)
                     .Select(e.AssignedIsActive)
                    .Select(e.OwnerUsername)
                    .Where(e.Source != "Visit")
                    .Where(e.Source != "" +
                    "TeleCalling")
                    .Where(e.Date >= Request.DateFrom)
                    .Where(e.Date <= Request.DateTo)
                    );

                Open = connection.List<EnquiryRow>(q => q
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

                Closed = connection.List<EnquiryRow>(q => q
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

                All = connection.List<EnquiryRow>(q => q
                     .SelectTableFields()
                     .Select(e.Id).Select(e.Total)
                    .Select(e.AssignedUsername)
                     .Select(e.AssignedDisplayName)
                     .Select(e.AssignedIsActive)
                    .Select(e.OwnerUsername)
                     .Where(e.Date >= Request.DateFrom)
                     .Where(e.Date <= Request.DateTo)
                     );

                Won = connection.List<EnquiryRow>(q => q
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

                Lost = connection.List<EnquiryRow>(q => q
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