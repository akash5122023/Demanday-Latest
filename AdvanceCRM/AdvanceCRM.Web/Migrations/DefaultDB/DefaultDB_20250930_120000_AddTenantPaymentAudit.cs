using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250930120000)]
    public class DefaultDB_20250930_120000_AddTenantPaymentAudit : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Tenants")
                .AddColumn("PurchaseAmount").AsDecimal(18, 2).Nullable()
                .AddColumn("PurchaseCurrency").AsString(16).Nullable()
                .AddColumn("PurchasedUsers").AsInt32().Nullable()
                .AddColumn("PaymentOrderId").AsString(100).Nullable()
                .AddColumn("PaymentId").AsString(100).Nullable();
        }
    }
}
