using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20180601162900)]
    public class DefaultDB_20180601_162900_Complaint : Migration
    {
        public override void Up()
        {
            Create.Table("Ticket")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(150).NotNullable()
                .WithColumn("Phone").AsString(30).NotNullable()
                .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_Ticket_ProductsId", "dbo", "Products", "Id")
                .WithColumn("ComplaintDetails").AsString(500).NotNullable()
                .WithColumn("Priority").AsInt32().NotNullable()
                .WithColumn("AssignedId").AsInt32().Nullable().ForeignKey("FK_Complaint_CUserId", "dbo", "Users", "UserId")
            ;
        }

        public override void Down()
        {

        }
    }
}