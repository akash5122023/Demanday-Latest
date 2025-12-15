
using AdvanceCRM.Administration;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20221017130600)]
    public class DefaultDB_20221017_130600_templates : Migration
    {

        public override void Up()
        {
            Alter.Table("EnquiryTemplate") 
                .AddColumn("WATemplate").AsString(1000).Nullable();
            Alter.Table("QuotationTemplate")
                .AddColumn("WATemplate").AsString(1000).Nullable();
            Alter.Table("InvoiceTemplate")
                .AddColumn("WATemplate").AsString(1000).Nullable();
           
        }

        public override void Down()
        {
        }
    }
}