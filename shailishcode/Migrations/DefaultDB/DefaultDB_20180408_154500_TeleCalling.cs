using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20180408154500)]
    public class DefaultDB_20180408_154500_TeleCalling : Migration
    {
        public override void Up()
        {

            Create.Table("TeleCalling")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_TeleCalling_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("ProductsId").AsInt32().Nullable().ForeignKey("FK_TeleCalling_ProductsId", "dbo", "Products", "Id")
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Feedback").AsString(2000).Nullable()
                .WithColumn("SourceId").AsInt32().Nullable().ForeignKey("FK_TeleCalling_SourceId", "dbo", "Source", "Id")
                .WithColumn("StageId").AsInt32().Nullable().ForeignKey("FK_TeleCalling_StageId", "dbo", "Stage", "Id")
                .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_TeleCalling_BranchId", "dbo", "Branch", "Id")
                .WithColumn("Representative").AsInt32().Nullable().ForeignKey("FK_TeleCalling_UserId", "dbo", "Users", "UserId")
                .WithColumn("Date").AsDateTime().Nullable()
                .WithColumn("AppointmentDate").AsDateTime().Nullable()
                .WithColumn("Details").AsString(200).Nullable()
                .WithColumn("AssignedTo").AsInt32().Nullable().ForeignKey("FK_TeleCallingAssignedTo_UserId", "dbo", "Users", "UserId")
                ;

            Create.Table("TeleCallingFollowups")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("FollowupNote").AsString(500).NotNullable()
                .WithColumn("Details").AsString(500)
                .WithColumn("FollowupDate").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("TeleCallingId").AsInt32().NotNullable().ForeignKey("FK_Followups_TeleCallingId", "dbo", "TeleCalling", "Id")
                .WithColumn("RepresentativeId").AsInt32().Nullable().ForeignKey("FK_TeleCallingFollowups_UserId", "dbo", "Users", "UserId")
                ;
        }

        public override void Down()
        {

        }
    }
}