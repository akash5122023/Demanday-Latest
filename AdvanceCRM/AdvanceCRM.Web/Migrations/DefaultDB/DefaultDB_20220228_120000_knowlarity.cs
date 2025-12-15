using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220228120000)]
    public class DefaultDB_20220228_120000_knowlarity : Migration
    {
        public override void Up()
        {
            Alter.Table("KnowlarityDetails")
                .AddColumn("CMIUID").AsString(30).Nullable()
                 .AddColumn("BilledSec").AsString(20).Nullable()
                .AddColumn("Rate").AsString(20).Nullable()
                .AddColumn("Record").AsString(20).Nullable()
                .AddColumn("From").AsString(20).Nullable()
                .AddColumn("To").AsString(20).Nullable()
                .AddColumn("Type").AsString(20).Nullable()
            ;
        }

        public override void Down()
        {

        }
    }
}