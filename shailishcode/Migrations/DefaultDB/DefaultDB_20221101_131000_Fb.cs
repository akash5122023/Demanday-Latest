
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
    [Migration(20221101131000)]
    public class DefaultDB_20221101_131000_Fb : Migration
    {

        public override void Up()
        {
           
            Update.Table("CompanyDetails").Set(new
            {
                Name = "Your Company Name"                
            }).AllRows();


        }

        public override void Down()
        {
        }
    }
}