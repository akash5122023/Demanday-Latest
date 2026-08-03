using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20261106000000)]
    public class DefaultDB_20260803_140000_DNCContactsSrNo : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("DNCContacts").Column("SrNo").Exists())
            {
                Alter.Table("DNCContacts").AddColumn("SrNo").AsInt32().Nullable();
                Execute.Sql(
                    "CREATE UNIQUE INDEX [UX_DNCContacts_SrNo] ON [dbo].[DNCContacts] ([SrNo]) " +
                    "WHERE [SrNo] IS NOT NULL");
            }
        }

        public override void Down()
        {
            Execute.Sql("DROP INDEX IF EXISTS [UX_DNCContacts_SrNo] ON [dbo].[DNCContacts]");
            if (Schema.Table("DNCContacts").Column("SrNo").Exists())
            {
                Delete.Column("SrNo").FromTable("DNCContacts");
            }
        }
    }
}
