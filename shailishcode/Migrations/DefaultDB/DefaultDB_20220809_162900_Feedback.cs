using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220809162900)]
    public class DefaultDB_20220809_162900_Feedback : Migration
    {
        public override void Up()
        {
            Create.Table("FeedbackDetails")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(150).NotNullable()
                .WithColumn("Phone").AsString(30).NotNullable()
                .WithColumn("Service").AsInt32().Nullable()
                .WithColumn("Refer").AsBoolean().NotNullable()
                .WithColumn("Details").AsString(500).NotNullable()
                
                ;
        }

        public override void Down()
        {

        }
    }
}