using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    /// <summary>
    /// Country on a Demanday Verification record. Nullable: existing records have none.
    /// </summary>
    [Migration(20261111000000)]
    public class DefaultDB_20261111_000000_DemandayVerificationCountry : Migration
    {
        public override void Up()
        {
            Alter.Table("DemandayVerification")
                .AddColumn("Country").AsString(100).Nullable();
        }

        public override void Down()
        {
            Delete.Column("Country").FromTable("DemandayVerification");
        }
    }
}
