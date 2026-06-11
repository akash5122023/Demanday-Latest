using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260902000000)]
    public class DefaultDB_20260902_000000_DemandayJobFields : Migration
    {
        public override void Up()
        {
            if (Schema.Table("DemandayMIS").Exists())
            {
                if (!Schema.Table("DemandayMIS").Column("Domain").Exists())
                    Alter.Table("DemandayMIS").AddColumn("Domain").AsString(100).Nullable();
                if (!Schema.Table("DemandayMIS").Column("Job_Level").Exists())
                    Alter.Table("DemandayMIS").AddColumn("Job_Level").AsString(100).Nullable();
                if (!Schema.Table("DemandayMIS").Column("Job_Function_Role").Exists())
                    Alter.Table("DemandayMIS").AddColumn("Job_Function_Role").AsString(200).Nullable();
            }

            if (Schema.Table("DemandayTeleMarketingMIS").Exists())
            {
                if (!Schema.Table("DemandayTeleMarketingMIS").Column("Domain").Exists())
                    Alter.Table("DemandayTeleMarketingMIS").AddColumn("Domain").AsString(100).Nullable();
                if (!Schema.Table("DemandayTeleMarketingMIS").Column("Job_Level").Exists())
                    Alter.Table("DemandayTeleMarketingMIS").AddColumn("Job_Level").AsString(100).Nullable();
                if (!Schema.Table("DemandayTeleMarketingMIS").Column("Job_Function_Role").Exists())
                    Alter.Table("DemandayTeleMarketingMIS").AddColumn("Job_Function_Role").AsString(200).Nullable();
            }
        }

        public override void Down()
        {
        }
    }
}
