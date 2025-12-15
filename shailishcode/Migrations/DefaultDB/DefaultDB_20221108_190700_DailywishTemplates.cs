using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20221108190700)]
    public class DefaultDB_20221108_190700_DailywishTemplates : Migration
    {
        public override void Up()
        {
            Alter.Table("DailyWishesTemplate")
                .AlterColumn("BirthdayEmail").AsString(int.MaxValue).NotNullable()
                .AlterColumn("MarriageEmail").AsString(int.MaxValue).NotNullable()
                .AlterColumn("DOFAnniversaryEmail").AsString(int.MaxValue).NotNullable()
                ;
        }

        public override void Down()
        {

        }
    }
}