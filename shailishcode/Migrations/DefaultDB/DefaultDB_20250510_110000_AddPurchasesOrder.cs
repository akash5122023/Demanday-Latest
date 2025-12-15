using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250510110000)]
    public class DefaultDB_20250510_110000_AddPurchaseOrder : AutoReversingMigration
    {

        public override void Up()
        {

            Alter.Table("Purchase")
                .AddColumn("QuotationNo").AsInt32().Nullable();


            Alter.Table("PurchaseOrder")
                    .AddColumn("QuotationNo").AsInt32().Nullable();
                   
           

        }
    }
}