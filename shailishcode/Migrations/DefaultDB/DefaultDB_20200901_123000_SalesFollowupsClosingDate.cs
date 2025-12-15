using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200901123000)]
    public class DefaultDB_20200901_123000_SalesFollowupsClosingDate : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("SalesFollowups").AddColumn("ClosingDate").AsDate().Nullable();
        }
    }
}