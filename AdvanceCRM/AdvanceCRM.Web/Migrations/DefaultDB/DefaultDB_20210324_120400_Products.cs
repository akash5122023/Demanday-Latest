using FluentMigrator;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{

    [Migration(20210324120400)]
    public class DefaultDB_20210324_120400_Products : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("EnquiryProducts")
                .AddColumn("Capacity").AsString().Nullable()             
                 ;

            Alter.Table("InvoiceProducts")
               .AddColumn("Capacity").AsString().Nullable()
                ;

            Alter.Table("QuotationProducts")
           .AddColumn("Capacity").AsString().Nullable()
            ;
        }
    }
}