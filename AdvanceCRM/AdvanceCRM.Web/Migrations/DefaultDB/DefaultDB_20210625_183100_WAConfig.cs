using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210625183100)]
    public class DefaultDB_20210625_183100_WAConfig : Migration
    {
        public override void Up()
        {
            Create.Table("WAConfigration")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()                
                .WithColumn("Mobile").AsString(50).NotNullable()              
                .WithColumn("Api-Key").AsString(100).Nullable()
                .WithColumn("MessageAPI").AsString(2048).NotNullable().WithDefaultValue("SMS_Url")
                .WithColumn("MediaAPI").AsString(2048).NotNullable().WithDefaultValue("Media_Url")
                .WithColumn("SuccessResponse").AsString(2048).NotNullable().WithDefaultValue("Success");
             ;

            //Insert.IntoTable("SMSConfiguration").Row(new
            //{
            //    MessageAPI = "Username",
            //    MediaAPI = "Password",
            //    Mobile = "MobileNumber"
            //});
        }

        public override void Down()
        {

        }
    }
}