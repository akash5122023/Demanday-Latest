using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210629_183100)]
    public class DefaultDB_20210629_183100_AddInfoType : Migration
    {
        public override void Up()
        {
            Alter.Table("AdditionalInfo")
            .AddColumn("Type").AsInt32().Nullable();

            Alter.Table("CompanyDetails")
                 .AddColumn("Addinfo2").AsBoolean().Nullable()
                .AddColumn("MultiAddInfo").AsBoolean().Nullable();
        }

        public override void Down()
        {

        }
    }
}