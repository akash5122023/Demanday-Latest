using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250519032400)]
    public class DefaultDB_20250519_032400_AddCompanyDetailsTravels : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("CompanyDetails")
               .AddColumn("Travels").AsBoolean().Nullable()
                
               ;



        }
    }
}