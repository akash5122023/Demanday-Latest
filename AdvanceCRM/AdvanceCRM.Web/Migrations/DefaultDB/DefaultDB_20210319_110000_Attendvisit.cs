using FluentMigrator;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{

    [Migration(20210319110000)]
    public class DefaultDB_20210319_110000_Attendvisit : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Visit")
                 .AlterColumn("DateNTime").AsDate().NotNullable()
                 .AlterColumn("MobileNo").AsString().Nullable()
                 ;
            Alter.Table("Attendance")
                .AlterColumn("DateNTime").AsDate().NotNullable()
                .AlterColumn("PunchIn").AsTime()
                .AlterColumn("PunchOut").AsTime();
        }
    }
}