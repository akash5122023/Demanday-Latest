using FluentMigrator;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{

    [Migration(20210321120000)]
    public class DefaultDB_20210321_120000_visit : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Visit")
                .AlterColumn("DateNTime").AsDateTime().NotNullable()
                
                 ;
            
        }
    }
}