using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250620000000)]
    public class DefaultDB_20250620_000000_MakeProductColumnsNullable : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Column("ProductTypeId").OnTable("Products").AsInt32().Nullable();
            Alter.Column("ModelSegmentId").OnTable("Products").AsInt32().Nullable();
            Alter.Column("ModelNameID").OnTable("Products").AsInt32().Nullable();
            Alter.Column("ModelCodeId").OnTable("Products").AsInt32().Nullable();
            Alter.Column("ModelVarientId").OnTable("Products").AsInt32().Nullable();
            Alter.Column("ModelColorId").OnTable("Products").AsInt32().Nullable();
            Alter.Column("SerialNo").OnTable("Products").AsString(200).Nullable();
        }
    }
}
