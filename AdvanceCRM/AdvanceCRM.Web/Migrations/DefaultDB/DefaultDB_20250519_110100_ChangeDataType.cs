using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250519110100)]
    public class DefaultDB_20250519_110100_ChangeDataType : AutoReversingMigration
    {

        public override void Up()
        {

            Alter.Column("Amount").OnTable("Itinerary").AsDouble().Nullable();



        }
    }
}