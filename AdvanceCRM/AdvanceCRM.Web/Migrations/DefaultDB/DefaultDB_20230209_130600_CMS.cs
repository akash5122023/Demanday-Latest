
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
    [Migration(20230209130600)]
    public class DefaultDB_20230209_130600_CMS : Migration
    {

        public override void Up()
        {
            Alter.Table("CMS").AlterColumn("ContactsId").AsInt32().Nullable()
         ;
        

    }

        public override void Down()
        {
        }
    }
}