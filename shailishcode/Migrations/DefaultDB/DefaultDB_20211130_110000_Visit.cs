using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20211130110000)]
    public class DefaultDB_20211130_110000_Visit : Migration
    {
        public override void Up()
        {
            if (Schema.Table("Visit").Column("CreatedBy").Exists())
            {
               Delete.Column("CreatedBy").FromTable("Visit");
            }


            Alter.Table("Visit")
                .AddColumn("CreatedBy").AsInt32().NotNullable().WithDefaultValue(1).ForeignKey("FK_VisitUserId_UserId", "dbo", "Users", "UserId");
        }

        public override void Down()
        {

        }
    }
}