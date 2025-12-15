
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
    [Migration(20250108131200)]
    public class DefaultDB_20250108_131200_Ticket : Migration
    {

        public override void Up()
        {

            Alter.Table("Ticket")              
              .AddColumn("Date").AsDateTime().Nullable();

        }

        public override void Down()
        {
        }
    }
}