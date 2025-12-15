using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20201001164200)]
    public class DefaultDB_20201001_164200_IndiaMARTDateTime : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("IndiaMartDetails")
                .AlterColumn("DateTimeRe").AsDateTime();
        }
    }
}