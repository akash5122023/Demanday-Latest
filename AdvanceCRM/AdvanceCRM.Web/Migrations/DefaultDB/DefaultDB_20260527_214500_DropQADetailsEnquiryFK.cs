using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260527214500)]
    public class DefaultDB_20260527_214500_DropQADetailsEnquiryFK : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                IF EXISTS (
                    SELECT * 
                    FROM sys.foreign_keys 
                    WHERE name = 'FK_DemandayTeleMarketingEnquiryQADetails_EnquiryId' 
                      AND parent_object_id = OBJECT_ID('dbo.DemandayTeleMarketingEnquiryQADetails')
                )
                BEGIN
                    ALTER TABLE dbo.DemandayTeleMarketingEnquiryQADetails 
                    DROP CONSTRAINT FK_DemandayTeleMarketingEnquiryQADetails_EnquiryId;
                END
            ");
        }

        public override void Down()
        {
            if (Schema.Table("DemandayTeleMarketingEnquiryQADetails").Exists() &&
                Schema.Table("DemandayTeleMarketingEnquiry").Exists())
            {
                Create.ForeignKey("FK_DemandayTeleMarketingEnquiryQADetails_EnquiryId")
                    .FromTable("DemandayTeleMarketingEnquiryQADetails").InSchema("dbo").ForeignColumn("EnquiryId")
                    .ToTable("DemandayTeleMarketingEnquiry").InSchema("dbo").PrimaryColumn("Id");
            }
        }
    }
}
