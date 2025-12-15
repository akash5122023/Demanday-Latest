
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
    [Migration(20240523130600)]
    public class DefaultDB_20240523_130600_User : Migration
    {

        public override void Up()
        {

            Alter.Table("IVRConfiguration")
              .AddColumn("CLINumber").AsString(100).Nullable();

            Create.Table("Teams")
                 .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                 .WithColumn("Team").AsString(100).NotNullable()
                .WithColumn("UserId").AsInt32().Nullable().ForeignKey("FK_TeamUserId_UserId", "dbo", "Users", "UserId");


            Alter.Table("Users")
                .AddColumn("TeamsId").AsInt32().Nullable().ForeignKey("FK_TeamsId_TeamsId", "dbo", "Teams", "Id");
           

        }

        public override void Down()
        {
        }
    }
}