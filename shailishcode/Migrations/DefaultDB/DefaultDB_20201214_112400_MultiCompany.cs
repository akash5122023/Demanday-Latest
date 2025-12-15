
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
    [Migration(20201214112400)]
    public class DefaultDB_20201214_112400_MultiCompany : AutoReversingMigration
    {

        public override void Up()
        {
            if (!Schema.Table("Users").Column("CompanyId").Exists())
                Alter.Table("Users").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_User_CompanyId", "dbo", "CompanyDetails", "Id");
            if (!Schema.Table("Roles").Column("CompanyId").Exists())
                Alter.Table("Roles").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_Roles_CompanyId", "dbo", "CompanyDetails", "Id");
            if (!Schema.Table("Branch").Column("CompanyId").Exists())
                Alter.Table("Branch").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_Branch_CompanyId", "dbo", "CompanyDetails", "Id");

            if (!Schema.Table("Cashbook").Column("CompanyId").Exists())
                Alter.Table("Cashbook").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_Cashbook_CompanyId", "dbo", "CompanyDetails", "Id");
            if (!Schema.Table("Products").Column("CompanyId").Exists())
                Alter.Table("Products").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_Products_CompanyId", "dbo", "CompanyDetails", "Id");
            if (!Schema.Table("StockTransfer").Column("CompanyId").Exists())
                Alter.Table("StockTransfer").AddColumn("CompanyId").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_StockTransfer_CompanyId", "dbo", "CompanyDetails", "Id");

        }
    }
}