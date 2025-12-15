using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210702183100)]
    public class DefaultDB_20210702_183100_Quotation : Migration
    {
        public override void Up()
        {
            Alter.Table("Invoice")
                .AddColumn("MessageId").AsInt32().Nullable().ForeignKey("FK_Message_MessageId","dbo","MessageMaster","Id");
        }

        public override void Down()
        {

        }
    }
}