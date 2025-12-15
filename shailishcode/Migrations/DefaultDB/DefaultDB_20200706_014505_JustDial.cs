using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200706014505)]
    public class DefaultDB_20200706_014505_JustDial : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("IndiaMartDetails").AddColumn("IsMoved").AsBoolean().WithDefaultValue(false).Nullable();

            Create.Table("JustDialDetails").WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("LeadId").AsString(255).Unique()
                .WithColumn("LeadType").AsString(255).Nullable()
                .WithColumn("Prefix").AsString().Nullable()
                .WithColumn("Name").AsString(255).Nullable()
                .WithColumn("Mobile").AsString(50).Nullable()
                .WithColumn("Landline").AsString(50).Nullable()
                .WithColumn("Email").AsString(50).Nullable()
                .WithColumn("DateTime").AsDateTime()
                .WithColumn("Category").AsString(255).Nullable()
                .WithColumn("City").AsString(255).Nullable()
                .WithColumn("Area").AsString(255).Nullable()
                .WithColumn("BranchArea").AsString(255).Nullable()
                .WithColumn("DCNMobile").AsBoolean().Nullable()
                .WithColumn("DCNPhone").AsBoolean().Nullable()
                .WithColumn("Company").AsString(255).Nullable()
                .WithColumn("Pin").AsString(50).Nullable()
                .WithColumn("BranhPin").AsString(50).Nullable()
                .WithColumn("ParentId").AsString(255).Nullable()
                .WithColumn("IsMoved").AsBoolean().WithDefaultValue(false);

            Create.Table("JustDial")
               .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("Username").AsString(20).Nullable()
               .WithColumn("Password").AsString(20).Nullable()
               ;

            Insert.IntoTable("JustDial").Row(new
            {
                Username = "justdial",
                Password = "justdialapi"
            });
        }
    }
}