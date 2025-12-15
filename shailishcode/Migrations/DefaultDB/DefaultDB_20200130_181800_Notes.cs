using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200130181800)]
    public class DefaultDB_20200130_181800_Notes : AutoReversingMigration
    {

        public override void Up()
        {
            Create.Table("Notes").WithColumn("NoteID").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("EntityType").AsString(100).NotNullable()
                .WithColumn("EntityID").AsInt64().NotNullable()
                .WithColumn("Text").AsString(int.MaxValue).NotNullable()
                .WithColumn("InsertUserId").AsInt32().NotNullable()
                .WithColumn("InsertDate").AsDateTime().NotNullable();
        }
    }
}