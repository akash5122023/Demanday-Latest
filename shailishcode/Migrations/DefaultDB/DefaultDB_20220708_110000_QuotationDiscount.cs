using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220708110000)]
    public class DefaultDB_20220708_110000_QuotationDiscount : AutoReversingMigration
    {
        public override void Up()
        {


            Alter.Table("MailInboxDetails")
           .AddColumn("messageID").AsString(255).Nullable()
           .AddColumn("IsMoved").AsBoolean().WithDefaultValue(false);

            Alter.Table("Quotation")
                .AddColumn("PerDiscount").AsDouble().Nullable()
                .AddColumn("DiscountAmt").AsDouble().Nullable();





        }
    }
}