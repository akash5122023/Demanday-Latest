using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20221105190700)]
    public class DefaultDB_20221105_190700_DailywishTemplates : Migration
    {
        public override void Up()
        {
            Alter.Table("DailyWishesTemplate")
                .AlterColumn("BirthdayEmail").AsString(2500).NotNullable()
                .AlterColumn("MarriageEmail").AsString(2500).NotNullable()
                .AlterColumn("DOFAnniversaryEmail").AsString(2500).NotNullable()
                ;
        }

        public override void Down()
        {

        }
    }
}