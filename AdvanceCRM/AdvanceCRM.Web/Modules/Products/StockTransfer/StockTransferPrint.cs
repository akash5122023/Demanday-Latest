
namespace AdvanceCRM.Products
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using System;
    using System.Collections.Generic;
    using Serenity.Services;
    using Serenity.Extensions.DependencyInjection;

    [Report("StockTransfer.PrintStockTransfer")]
    [ReportDesign(MVC.Views.Products.StockTransfer.StockTransferPrint)]
    public class StockTransferReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;


        public StockTransferReport(

            ISqlConnections connections,
            IRequestContext context
          )
        {
            this._connections = connections;
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public StockTransferReport()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }
        public Int32 Id { get; set; }
        public Boolean Taxable { get; set; }
        public String ModType { get; set; }

        public object GetData()
        {
            var data = new StockTransferReportData();

            using (var connection = _connections.NewFor<StockTransferRow>())
            {
                data.id = this.Id;
                data.modtype = this.ModType;


                var o = StockTransferRow.Fields;

                data.ST = connection.TryById<StockTransferRow>(this.Id, q => q
                     .SelectTableFields()
                     .Select(o.Id)
                     .Select(o.Date)
                     .Select(o.FromBranchBranch)
                     .Select(o.ToBranchBranch)
                     .Select(o.FromBranchAddress)
                     .Select(o.ToBranchAddress)
                     .Select(o.FromBranchPhone)
                     .Select(o.FromBranchStateId)
                     .Select(o.ToBranchPhone)
                     .Select(o.ToBranchStateId)
                     );

                var od = StockTransferProductsRow.Fields;
                data.STP = connection.List<StockTransferProductsRow>(q => q
                    .SelectTableFields()
                    .Select(od.ProductsCode)
                    .Select(od.ProductsHsn)
                    .Select(od.ProductsName)
                    .Select(od.ProductsDescription)
                    .Select(od.Quantity)
                    .Select(od.TaxType1)
                    .Select(od.Percentage1)
                    .Select(od.Tax1Amount)
                    .Select(od.TaxType2)
                    .Select(od.Percentage2)
                    .Select(od.Tax2Amount)
                    .Select(od.LineTotal)
                    .Select(od.GrandTotal)
                    .Select(od.ProductsTechSpecs)
                    .Where(od.StockTransferId == this.Id));


                var r = Administration.UserRow.Fields;
                data.Representative = connection.TryFirst<Administration.UserRow>(r.UserId == data.ST.RepresentativeId.Value)
                    ?? new Administration.UserRow();

                var c = CompanyDetailsRow.Fields;

                var userDef = Context?.User?.ToUserDefinition() as UserDefinition ??
                               Serenity.Authorization.UserDefinition as UserDefinition;

                if (userDef != null)
                    data.Company = connection.TryById<CompanyDetailsRow>(userDef.CompanyId, q => q
                     .SelectTableFields()
                     .Select(c.Name)
                     .Select(c.Slogan)
                     .Select(c.Address)
                     .Select(c.Phone)
                     .Select(c.StateId)
                     .Select(c.Logo)
                     .Select(c.LogoHeight)
                     .Select(c.LogoWidth)
                     .Select(c.HeaderImage)
                     .Select(c.HeaderHeight)
                     .Select(c.HeaderWidth)
                     .Select(c.FooterImage)
                     .Select(c.FooterHeight)
                     .Select(c.FooterWidth)
                     .Select(c.HeaderImage)
                     .Select(c.HeaderHeight)
                     .Select(c.HeaderWidth)
                     .Select(c.FooterImage)
                     .Select(c.FooterHeight)
                     .Select(c.FooterWidth)
                     .Select(c.GSTIN)
                     .Select(c.PANNo)
                     );
            }

            return data;
        }

        public void Customize(IHtmlToPdfOptions options)
        {
            // you may customize HTML to PDF converter (WKHTML) parameters here, e.g. 
            // options.MarginsAll = "2cm";
        }
    }

    public class StockTransferReportData
    {
        public Int32 id { get; set; }
        public String modtype { get; set; }
        public StockTransferRow ST { get; set; }
        public List<StockTransferProductsRow> STP { get; set; }
        public Administration.UserRow Representative { get; set; }
        public ContactsRow Contacts { get; set; }
        public CompanyDetailsRow Company { get; set; }
    }
}