using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250510122600)]
    public class DefaultDB_20250510_122600_AddCompanyIdPurchaseandPO : AutoReversingMigration
    {

        public override void Up()
        {

            Alter.Table("Purchase")
                    .AddColumn("CompanyId").AsInt32().Nullable().WithDefaultValue(1)
                    .ForeignKey("FK_Purchase_CompanyId", "dbo", "CompanyDetails", "Id");


            Alter.Table("PurchaseOrder")
                     .AddColumn("CompanyId").AsInt32().Nullable().WithDefaultValue(1)
                    .ForeignKey("FK_PurchaseOrder_CompanyId", "dbo", "CompanyDetails", "Id");




        }
    }
}