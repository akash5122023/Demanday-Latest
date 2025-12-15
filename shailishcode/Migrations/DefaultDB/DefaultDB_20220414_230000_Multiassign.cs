using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220414230000)]
    public class DefaultDB_20220414_230000_Multiassign : Migration
    {
        public override void Up()
        {
           

            Alter.Table("Quotation")
                .AddColumn("ProjectId").AsInt32().Nullable().ForeignKey("FK_Quotation_ProjectId", "dbo", "MultiProjects", "Id");


        }

        public override void Down()
        {

        }
    }
}