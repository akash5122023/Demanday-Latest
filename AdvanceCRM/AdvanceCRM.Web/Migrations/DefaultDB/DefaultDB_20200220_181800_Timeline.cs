using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200220181800)]
    public class DefaultDB_20200220_181800_Timeline : AutoReversingMigration
    {

        public override void Up()
        {
            Create.Table("Timeline").WithColumn("TimelineID").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("EntityType").AsString(100).NotNullable()
                .WithColumn("EntityID").AsInt64().NotNullable()
                .WithColumn("Text").AsString(int.MaxValue).NotNullable()
                .WithColumn("InsertUserId").AsInt32().NotNullable()
                .WithColumn("InsertDate").AsDateTime().NotNullable();
        }
    }
}