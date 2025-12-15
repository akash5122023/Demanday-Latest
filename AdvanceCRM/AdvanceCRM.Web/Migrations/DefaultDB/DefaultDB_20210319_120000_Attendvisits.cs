using FluentMigrator;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{

    [Migration(20210319120000)]
    public class DefaultDB_20210319_120000_Attendvisits : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Visit")
                 .AddColumn("CreatedBy").AsInt32().NotNullable().WithDefaultValue(1)
                 ;
            Alter.Table("Attendance")
                
                .AlterColumn("PunchIn").AsDateTime()
                .AlterColumn("PunchOut").AsDateTime();
        }
    }
}