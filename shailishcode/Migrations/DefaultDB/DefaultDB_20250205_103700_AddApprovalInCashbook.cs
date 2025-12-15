
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
    [Migration(20250205103700)]
    public class DefaultDB_20250205_103700_AddApprovalInCashbook : Migration
    {
        public override void Up()
        {
            Alter.Table("Cashbook")
                .AddColumn("ApprovedBy").AsInt32().Nullable().ForeignKey("FK_CashbookApprovedBy_UserId", "dbo", "Users", "UserId");

        }

        public override void Down()
        {

        }
    }
}