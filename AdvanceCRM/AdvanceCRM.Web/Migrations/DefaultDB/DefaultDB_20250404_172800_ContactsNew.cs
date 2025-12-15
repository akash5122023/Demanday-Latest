using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250404172800)]
    public class DefaultDB_20250404_172800_ContactsNew : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("Contacts")
                  .AddColumn("PassportNumber").AsString(50).Nullable()
                  .AddColumn("FirstName").AsString(100).Nullable()
                  .AddColumn("LastName").AsString(100).Nullable()
                  .AddColumn("ExpiryDate").AsDateTime().Nullable()

                   .AddColumn("AadharNo").AsString(50).Nullable();
        }
    }
}