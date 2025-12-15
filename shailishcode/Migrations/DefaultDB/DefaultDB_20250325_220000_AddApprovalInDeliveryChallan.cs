
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
    [Migration(20250325220000)]
    public class DefaultDB_20250325_220000_AddApprovalInDeliveryChallan : Migration
    {
        public override void Up()
        {
            Alter.Table("Challan")
                .AddColumn("ApprovedBy").AsInt32().Nullable().ForeignKey("FK_ChallanApprovedBy_UserId", "dbo", "Users", "UserId");

        }

        public override void Down()
        {

        }
    }
}