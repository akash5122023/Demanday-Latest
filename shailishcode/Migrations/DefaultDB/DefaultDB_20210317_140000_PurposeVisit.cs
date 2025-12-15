using FluentMigrator;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{

    [Migration(20210317140000)]
    public class DefaultDB_20210317_140000_PurposeVisit : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Purpose")
                 .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                 .WithColumn("Purpose").AsString(2000).NotNullable();
        }
    }
}