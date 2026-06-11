using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260903000000)]
    public class DefaultDB_20260903_000000_DemandayTeamLeaderSlot : Migration
    {
        public override void Up()
        {
            if (Schema.Table("DemandayTeamLeader").Exists() &&
                !Schema.Table("DemandayTeamLeader").Column("SLOT").Exists())
            {
                Alter.Table("DemandayTeamLeader").AddColumn("SLOT").AsString(100).Nullable();
            }

            if (Schema.Table("DemandayTeleMarketingTeamLeader").Exists() &&
                !Schema.Table("DemandayTeleMarketingTeamLeader").Column("SLOT").Exists())
            {
                Alter.Table("DemandayTeleMarketingTeamLeader").AddColumn("SLOT").AsString(100).Nullable();
            }
        }

        public override void Down()
        {
        }
    }
}
