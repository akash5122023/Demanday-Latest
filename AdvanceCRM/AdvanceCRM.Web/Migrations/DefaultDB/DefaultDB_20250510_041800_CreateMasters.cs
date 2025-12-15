using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250510041800)]
    public class DefaultDB_20250510_041800_CreateMasters : AutoReversingMigration
    {

        public override void Up()
        {

            Create.Table("Days").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Title").AsString(50).Nullable()
                .WithColumn("Heading").AsString(100).Nullable()
                .WithColumn("Description").AsString(500).Nullable()
                .WithColumn("FileAttachments").AsString(1000).Nullable();

        }
    }
}