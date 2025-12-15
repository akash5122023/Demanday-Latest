using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250404165600)]
    public class DefaultDB_20250404_165600_QuotationProducts : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("QuotationProducts")
                 .AddColumn("ImageAttachment").AsString(500).Nullable()
                 .AddColumn("FileAttachment").AsString(1000).Nullable()
              ;

            Alter.Table("Products")
                 .AddColumn("ImageAttachment").AsString(500).Nullable()
                 .AddColumn("FileAttachment").AsString(1000).Nullable()
               ;
        }
    }
}