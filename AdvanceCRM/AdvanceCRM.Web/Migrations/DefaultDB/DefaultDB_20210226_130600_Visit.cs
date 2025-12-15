
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
    [Migration(20210226130600)]
    public class DefaultDB_20210226_130600_Visit : Migration
    {

        public override void Up()
        {
            if (Schema.Table("Visit").Exists())
                return;

            // Original Visit table creation was replaced by a later migration.
            // Keep this migration empty so version history remains intact.
        }

        public override void Down()
        {
        }
    }
}