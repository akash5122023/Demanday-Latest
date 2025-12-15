using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20230716183100)]
    public class DefaultDB_20230716_183100_sales : Migration
    {
        public override void Up()
        {
            Alter.Table("Sales")
                .AddColumn("Subject").AsString(1000).Nullable()
                .AddColumn("Reference").AsString(1000).Nullable()
                .AddColumn("MessageId").AsInt32().Nullable().ForeignKey("FK_SMessage_MessageId","dbo","MessageMaster","Id");
        }

        public override void Down()
        {

        }
    }
}