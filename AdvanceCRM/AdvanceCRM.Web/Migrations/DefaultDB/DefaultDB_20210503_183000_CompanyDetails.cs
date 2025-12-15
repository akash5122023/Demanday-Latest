using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210503183000)]
    public class DefaultDB_20210503_183000_CompanyDetails : Migration
    {
        public override void Up()
        {
            Alter.Table("CompanyDetails")
                .AddColumn("InvoiceHeaderContent").AsString(int.MaxValue).Nullable()
                .AddColumn("InvoiceFooterContent").AsString(int.MaxValue).Nullable()
                .AddColumn("InvoiceHeaderImage").AsString(500).Nullable()
                .AddColumn("InvoiceHeaderHeight").AsInt32().Nullable()
                .AddColumn("InvoiceHeaderWidth").AsInt32().Nullable()
                .AddColumn("InvoiceFooterImage").AsString(500).Nullable()
                .AddColumn("InvoiceFooterHeight").AsInt32().Nullable()
                .AddColumn("InvoiceFooterWidth").AsInt32().Nullable()

                .AddColumn("DCHeaderContent").AsString(int.MaxValue).Nullable()
                .AddColumn("DCFooterContent").AsString(int.MaxValue).Nullable()
                .AddColumn("DCHeaderImage").AsString(500).Nullable()
                .AddColumn("DCHeaderHeight").AsInt32().Nullable()
                .AddColumn("DCHeaderWidth").AsInt32().Nullable()
                .AddColumn("DCFooterImage").AsString(500).Nullable()
                .AddColumn("DCFooterHeight").AsInt32().Nullable()
                .AddColumn("DCFooterWidth").AsInt32().Nullable();
        }

        public override void Down()
        {

        }
    }
}