
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
    [Migration(20220126130600)]
    public class DefaultDB_20220126_130600_DMS : Migration
    {

        public override void Up()
        {

            Alter.Table("DMS")
             .AddColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_DMSUserId_UserId", "dbo", "Users", "UserId");

        }

        public override void Down()
        {
        }
    }
}