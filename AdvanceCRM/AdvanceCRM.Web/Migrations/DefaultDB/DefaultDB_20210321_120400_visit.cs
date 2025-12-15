using FluentMigrator;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{

    [Migration(20210321120400)]
    public class DefaultDB_20210321_120400_visit : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Visit")
                .AlterColumn("CompanyName").AsString().NotNullable()
                .AlterColumn("Attachments").AsString(2000).Nullable()
                 ;
            
        }
    }
}