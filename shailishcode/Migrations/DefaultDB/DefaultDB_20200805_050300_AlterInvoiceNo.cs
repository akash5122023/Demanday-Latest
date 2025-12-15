using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200805050300)]
    public class DefaultDB_20200805_050300_AlterInvoiceNo : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("SalesReturn")
                .AlterColumn("InvoiceNo").AsString(100).Nullable().WithDefaultValue("");

            Alter.Table("PurchaseReturn")
                .AlterColumn("InvoiceNo").AsString(100).Nullable().WithDefaultValue("");
        }
    }
}