using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220414210000)]
    public class DefaultDB_20220414_210000_Multiassign : Migration
    {
        public override void Up()
        {
            Create.Table("MultiProjects")
               .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("ProjectId").AsInt32().NotNullable().ForeignKey("FK_MultiProjects_ProjectId", "dbo", "Project", "Id")
               .WithColumn("SubContactsId").AsInt32().NotNullable().ForeignKey("FK_MultiProjects_SubContactsId", "dbo", "SubContacts", "Id")
               ;

            Alter.Table("Enquiry")
                .AddColumn("ProjectId").AsInt32().Nullable().ForeignKey("FK_Enquiry_ProjectId", "dbo", "Project", "Id");


        }

        public override void Down()
        {

        }
    }
}