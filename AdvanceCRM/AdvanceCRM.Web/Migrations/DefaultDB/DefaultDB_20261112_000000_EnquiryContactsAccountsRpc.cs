using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    /// <summary>
    /// Enquiry Contacts becomes an Accounts + RPC Contacts module: three dropdown masters
    /// (Account Type, Status, Disposition) and the new columns that point at them.
    /// </summary>
    [Migration(20261112000000)]
    public class DefaultDB_20261112_000000_EnquiryContactsAccountsRpc : Migration
    {
        private static readonly string[] Masters =
        {
            "ContactAccountTypeMaster", "ContactAccountStatusMaster", "ContactDispositionMaster"
        };

        public override void Up()
        {
            foreach (var t in Masters)
            {
                if (!Schema.Table(t).Exists())
                {
                    Create.Table(t)
                        .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                        .WithColumn("Name").AsString(200).Nullable();
                }
            }

            Seed("ContactAccountStatusMaster", "Aware", "Unaware");
            Seed("ContactDispositionMaster", "Lead", "Lost", "MQL");

            Alter.Table("EnquiryContacts")
                .AddColumn("AccountTypeId").AsInt32().Nullable()
                    .ForeignKey("FK_EnquiryContacts_AccountType", "ContactAccountTypeMaster", "Id")
                .AddColumn("AccountStatusId").AsInt32().Nullable()
                    .ForeignKey("FK_EnquiryContacts_AccountStatus", "ContactAccountStatusMaster", "Id")
                .AddColumn("DispositionId").AsInt32().Nullable()
                    .ForeignKey("FK_EnquiryContacts_Disposition", "ContactDispositionMaster", "Id")
                .AddColumn("Comments").AsString(2000).Nullable()
                .AddColumn("L1Meeting").AsDateTime().Nullable();
        }

        private void Seed(string table, params string[] values)
        {
            foreach (var v in values)
            {
                Execute.Sql(
                    "IF NOT EXISTS (SELECT 1 FROM dbo." + table + " WHERE Name = N'" + v + "') " +
                    "INSERT INTO dbo." + table + " (Name) VALUES (N'" + v + "');");
            }
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_EnquiryContacts_AccountType").OnTable("EnquiryContacts");
            Delete.ForeignKey("FK_EnquiryContacts_AccountStatus").OnTable("EnquiryContacts");
            Delete.ForeignKey("FK_EnquiryContacts_Disposition").OnTable("EnquiryContacts");
            Delete.Column("AccountTypeId").FromTable("EnquiryContacts");
            Delete.Column("AccountStatusId").FromTable("EnquiryContacts");
            Delete.Column("DispositionId").FromTable("EnquiryContacts");
            Delete.Column("Comments").FromTable("EnquiryContacts");
            Delete.Column("L1Meeting").FromTable("EnquiryContacts");
            foreach (var t in Masters)
                Delete.Table(t);
        }
    }
}
