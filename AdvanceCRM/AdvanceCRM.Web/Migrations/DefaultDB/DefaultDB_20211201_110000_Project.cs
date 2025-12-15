using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20211201110000)]
    public class DefaultDB_20211201_110000_Project : Migration
    {
        public override void Up()
        {
           Create.Table("Project")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Project").AsString(200).NotNullable();

            Alter.Table("Tasks")
                .AddColumn("ProjectId").AsInt32().Nullable().ForeignKey("FK_TProject_ProjectId", "dbo", "Project", "Id");

            Alter.Table("CMS")
               .AddColumn("ProjectId").AsInt32().Nullable().ForeignKey("FK_CMSProject_ProjectId", "dbo", "Project", "Id");
        }

        public override void Down()
        {

        }
    }
}