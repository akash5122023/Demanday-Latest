using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220708120000)]
    public class DefaultDB_20220708_120000_QuotationDiscount : AutoReversingMigration
    {
        public override void Up()
        {

            Alter.Table("Quotation")
                .AddColumn("DisGrandTotal").AsDouble().Nullable();





        }
    }
}