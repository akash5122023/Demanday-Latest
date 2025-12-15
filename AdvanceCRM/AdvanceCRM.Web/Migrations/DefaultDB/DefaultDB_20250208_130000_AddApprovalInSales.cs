
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
    [Migration(20250208130000)]
    public class DefaultDB_20250208_130000_AddApprovalInSales : Migration
    {
        public override void Up()
        {
            Alter.Table("Sales")
                .AddColumn("ApprovedBy").AsInt32().Nullable().ForeignKey("FK_SalesApprovedBy_UserId", "dbo", "Users", "UserId");

        }

        public override void Down()
        {

        }
    }
}