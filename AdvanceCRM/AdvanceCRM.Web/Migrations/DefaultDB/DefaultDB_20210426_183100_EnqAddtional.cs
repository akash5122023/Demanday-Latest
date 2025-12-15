using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210426_183100)]
    public class DefaultDB_20210426_183100_EnqAddtional : Migration
    {
        public override void Up()
        {
            Alter.Table("Enquiry")          
           .AddColumn("EnquiryNo").AsInt32().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {

        }
    }
}