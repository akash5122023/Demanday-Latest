
namespace AdvanceCRM.Products
{
    using AdvanceCRM.Administration;
    using AdvanceCRM;

    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using AdvanceCRM.Itinerary;

    [Report("Itinerary.ItineraryPrint")]
    [ReportDesign(MVC.Views.Products.Itinerary.ItineraryPrint)]
    public class ItineraryReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;

        public ItineraryReport(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ItineraryReport()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }

        public Int32 Id { get; set; }
        public String ModType { get; set; }

        public object GetData()
        {
            var data = new ItineraryReportData();

            using (var connection = _connections.NewFor<ItineraryRow>())
            {
                data.id = this.Id;
                data.modtype = this.ModType;


                var o = ItineraryRow.Fields;

                data.Itinerary = connection.TryById<ItineraryRow>(this.Id, q => q
                     .SelectTableFields()
                     .Select(o.Id)
                     .Select(o.Headline)
                     .Select(o.Date)
                     .Select(o.DaysId)
                     .Select(o.From)
                     .Select(o.To)
                     .Select(o.Destination)
                     .Select(o.HotelName)
                     .Select(o.Nights)
                     .Select(o.Adults)
                     .Select(o.Childrens)
                     .Select(o.MealPlan)
                     .Select(o.Amount)
                     .Select(o.TermsAndConditions)

                     );




                var it = ItineraryTermRow.Fields;
                data.ItineraryTerm = connection.List<ItineraryTermRow>(q => q
                    .SelectTableFields()
                    .Select(it.DaysId)
                    .Where(it.ItineraryId == this.Id));


                var c = CompanyDetailsRow.Fields;

                var userDef = Context?.User?.ToUserDefinition() as UserDefinition;
                if (userDef != null)
                {
                    data.Company = connection.TryById<CompanyDetailsRow>(userDef.CompanyId, q => q
                         .SelectTableFields()
                         //.Select(c.Name)
                         //.Select(c.Slogan)
                         //.Select(c.Address)
                         //.Select(c.Phone)
                         //.Select(c.StateId)
                         .Select(c.Logo)
                         .Select(c.LogoHeight)
                         .Select(c.LogoWidth)
                         .Select(c.ItineraryHeaderImage)
                         .Select(c.ItineraryHeaderHeight)
                         .Select(c.ItineraryHeaderWidth)
                         .Select(c.ItineraryFooterImage)
                         .Select(c.ItineraryFooterHeight)
                         .Select(c.ItineraryFooterWidth)
                         .Select(c.ItineraryHeaderContent)
                         .Select(c.ItineraryFooterContent)
                         //.Select(c.GSTIN)
                         //.Select(c.PANNo)
                         .Select(c.CompanyDetails)
                         //.Select(c.ItineraryTemplate)
                         //.Select(c.InvoiceSuffix)
                         //.Select(c.InvoicePrefix)
                         //.Select(c.InvoiceTaxColumnIncluded)
                         );
                }


            }

            return data;
        }

        public void Customize(IHtmlToPdfOptions options)
        {
            // you may customize HTML to PDF converter (WKHTML) parameters here, e.g. 
            // options.MarginsAll = "2cm";
        }
    }

        [Report("Itinerary.PrintItinerary")]
        [ReportDesign(MVC.Views.Products.Itinerary.ItineraryPrint)]
        public class InvoiceReport : IReport, ICustomizeHtmlToPdf
        {
            private readonly ISqlConnections _connections;
            private readonly IRequestContext Context;

            public InvoiceReport(ISqlConnections connections, IRequestContext context)
            {
                _connections = connections;
                Context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public InvoiceReport()
                : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
            {
            }

            public Int32 Id { get; set; }
            public String ModType { get; set; }

            public object GetData()
            {
                var data = new ItineraryReportData();

                using (var connection = _connections.NewFor<ItineraryRow>())
                {
                    data.id = this.Id;
                    data.modtype = this.ModType;


                    var o = ItineraryRow.Fields;

                    data.Itinerary = connection.TryById<ItineraryRow>(this.Id, q => q
                         .SelectTableFields()
                         .Select(o.Id)
                         .Select(o.Headline)
                     .Select(o.Date)
                     .Select(o.DaysId)
                     .Select(o.From)
                     .Select(o.To)
                     .Select(o.Destination)
                     .Select(o.HotelName)
                     .Select(o.Nights)
                     .Select(o.Adults)
                     .Select(o.Childrens)
                     .Select(o.MealPlan)
                     .Select(o.Amount)
                     .Select(o.TermsAndConditions)

                         );




                    var it = ItineraryTermRow.Fields;
                    data.ItineraryTerm = connection.List<ItineraryTermRow>(q => q
                        .SelectTableFields()
                        .Select(it.DaysId)
                        .Select(it.DaysTitle)
                        .Select(it.DaysDescription)
                        .Select(it.DaysHeading)
                        .Select(it.DaysFileAttachments)
                        .Where(it.ItineraryId == this.Id));


                var c = CompanyDetailsRow.Fields;

                var userDef = Context?.User?.ToUserDefinition() as UserDefinition;
                if (userDef != null)
                {
                    data.Company = connection.TryById<CompanyDetailsRow>(userDef.CompanyId, q => q
                         .SelectTableFields()
                         .Select(c.Name)
                         //.Select(c.Slogan)
                         .Select(c.Address)
                         //.Select(c.Phone)
                         //.Select(c.StateId)
                         .Select(c.Logo)
                         .Select(c.LogoHeight)
                         .Select(c.LogoWidth)
                         .Select(c.ItineraryHeaderImage)
                         .Select(c.ItineraryHeaderHeight)
                         .Select(c.ItineraryHeaderWidth)
                         .Select(c.ItineraryFooterImage)
                         .Select(c.ItineraryFooterHeight)
                         .Select(c.ItineraryFooterWidth)
                         .Select(c.ItineraryHeaderContent)
                         .Select(c.ItineraryFooterContent)
                         //.Select(c.GSTIN)
                         //.Select(c.PANNo)
                         //.Select(c.CompanyDetails)
                         //.Select(c.ItineraryTemplate)
                         //.Select(c.InvoiceSuffix)
                         //.Select(c.InvoicePrefix)
                         //.Select(c.InvoiceTaxColumnIncluded)
                         );
                }



            }

                return data;
            }

            public void Customize(IHtmlToPdfOptions options)
            {
                // you may customize HTML to PDF converter (WKHTML) parameters here, e.g. 
                // options.MarginsAll = "2cm";
            }
        }



        public class ItineraryReportData
        {
            public Boolean taxable { get; set; }
            public Int32 id { get; set; }
            public Int32 PONO { get; set; }
            public String modtype { get; set; }
            //public String Name { get; set; }
            public ItineraryRow Itinerary { get; set; }
            //public List<InvoiceProductsRow> InvoiceProducts { get; set; }
            public Administration.UserRow Representative { get; set; }
            //public ContactsRow Contacts { get; set; }
            public CompanyDetailsRow Company { get; set; }
            //public PurchaseOrderRow PurchaseOrder { get; set; }
            //public Template.InvoiceTemplateRow Template { get; set; }
            public List<ItineraryTermRow> ItineraryTerm { get; set; }
            // public List<InvoiceChargesRow> InvoiceCharges { get; set; }
            //public List<InvoiceConcessionRow> InvoiceConcession { get; set; }
        }
    }
