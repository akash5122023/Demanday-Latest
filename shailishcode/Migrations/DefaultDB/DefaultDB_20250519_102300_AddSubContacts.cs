using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250519102300)]
    public class DefaultDB_20250519_102300_AddSubContacts : AutoReversingMigration
    {

        public override void Up()
        {

            Alter.Table("SubContacts")
                    .AddColumn("AadharNo").AsString(50).Nullable()
                    .AddColumn("PANNo").AsString(50).Nullable()
                    .AddColumn("FileAttachments").AsString(1000).Nullable()
                   ;
         




        }
    }
}