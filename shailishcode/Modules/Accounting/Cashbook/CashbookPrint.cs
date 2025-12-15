
namespace AdvanceCRM.Accounting
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Administration.Entities;
    using AdvanceCRM.Quotation;
    
    using Serenity.Data;
    using Serenity.Reporting;
    using System;

    [Report("Cashbook.PrintRV")]
    [ReportDesign(MVC.Views.Accounting.Cashbook.CashbookPrint)]
   
    public class CashbookReport : IReport, ICustomizeHtmlToPdf
    {
        private readonly ISqlConnections _connections;

        public CashbookReport(ISqlConnections connections)
        {
            _connections = connections;
        }
        public Int32 Id { get; set; }
        public Boolean Taxable { get; set; }
        public String ModType { get; set; }

        public object GetData()
        {
            var data = new CashbookReportData();

            using (var connection = _connections.NewFor<QuotationRow>())
            {
                data.id = this.Id;
                data.modtype = this.ModType;

                var o = CashbookRow.Fields;
                data.Cash = connection.TryById<CashbookRow>(this.Id, q => q
                     .SelectTableFields()
                     .Select(o.Id)
                     .Select(o.Date)
                     .Select(o.ContactsName)
                     .Select(o.Type)
                     .Select(o.Head)
                     .Select(o.Head1)
                     .Select(o.CashIn)
                     .Select(o.CashOut)
                     .Select(o.Narration)                     
                     );

                var c = CompanyDetailsRow.Fields;
                data.Company = connection.TryById<CompanyDetailsRow>(1, q => q
                     .SelectTableFields()
                     .Select(c.Name)
                     .Select(c.Slogan)
                     .Select(c.Address)
                     .Select(c.Phone)
                     .Select(c.Logo)
                     .Select(c.LogoHeight)
                     .Select(c.LogoWidth)
                     .Select(c.HeaderImage)
                     .Select(c.HeaderHeight)
                     .Select(c.HeaderWidth)
                     .Select(c.FooterImage)
                     .Select(c.FooterHeight)
                     .Select(c.FooterWidth)
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

    public class CashbookReportData
    {
        public Int32 id { get; set; }
        public String modtype { get; set; }
        public CashbookRow Cash { get; set; }
        public CompanyDetailsRow Company { get; set; }
    }
}