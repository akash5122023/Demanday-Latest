using FluentMigrator;
using System;
using System.Data;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251101090000)]
    public class DefaultDB_20251101_090000_ProductModules : Migration
    {
        public override void Up()
        {
            Create.Table("ProductModules")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("DisplayName").AsString(150).NotNullable()
                .WithColumn("Description").AsString(500).Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("SortOrder").AsInt32().Nullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("CreatedBy").AsString(50).Nullable()
                .WithColumn("ModifiedOn").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString(50).Nullable();

            Create.Index("IX_ProductModules_Name")
                .OnTable("ProductModules")
                .OnColumn("Name").Ascending().WithOptions().Unique();

            Create.Table("ProductPlanModules")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("PlanId").AsInt32().NotNullable()
                .WithColumn("ModuleId").AsInt32().NotNullable();

            Create.ForeignKey("FK_ProductPlanModules_ProductPlans")
                .FromTable("ProductPlanModules").ForeignColumn("PlanId")
                .ToTable("ProductPlans").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);

            Create.ForeignKey("FK_ProductPlanModules_ProductModules")
                .FromTable("ProductPlanModules").ForeignColumn("ModuleId")
                .ToTable("ProductModules").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);

            Create.Index("UX_ProductPlanModules_Plan_Module")
                .OnTable("ProductPlanModules")
                .OnColumn("PlanId").Ascending()
                .OnColumn("ModuleId").Ascending()
                .WithOptions().Unique();

            var modules = new (string Name, string Display, int Sort)[]
            {
                ("MultiCompany", "Multi Company", 1),
                ("MultiLocation", "Multi Location", 2),
                ("Proforma", "Proforma", 3),
                ("Sales", "Sales", 4),
                ("Challan", "Challan", 5),
                ("CEO", "CEO Dashboard", 6),
                ("PurchaseOrder", "Purchase Order", 7),
                ("Purchase", "Purchase", 8),
                ("Cashbook", "Cashbook", 9),
                ("ExpenseManagement", "Expense Management", 10),
                ("CMS", "Content Management (CMS)", 11),
                ("AMC", "AMC", 12),
                ("TeleCalling", "Tele Calling", 13),
                ("Ticket", "Ticketing", 14),
                ("DMS", "Document Management (DMS)", 15),
                ("ChannelManagement", "Channel Management", 16),
                ("MailChimp", "MailChimp", 17),
                ("IVR", "IVR", 18),
                ("IndiaMART", "IndiaMART", 19),
                ("JustDial", "JustDial", 20),
                ("TradeIndia", "TradeIndia", 21),
                ("Facebook", "Facebook Leads", 22)
            };

            foreach (var module in modules)
            {
                Insert.IntoTable("ProductModules").Row(new
                {
                    Name = module.Name,
                    DisplayName = module.Display,
                    Description = (string)null,
                    SortOrder = module.Sort,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = "system"
                });
            }

            Execute.Sql(@"
                INSERT INTO ProductPlanModules (PlanId, ModuleId)
                SELECT p.Id, m.Id
                FROM ProductPlans p
                JOIN ProductModules m ON m.Name IN ('MultiCompany','MultiLocation','Proforma','Sales','Challan','Purchase')
                WHERE p.Name = 'Starter ERP';

                INSERT INTO ProductPlanModules (PlanId, ModuleId)
                SELECT p.Id, m.Id
                FROM ProductPlans p
                JOIN ProductModules m ON m.Name IN ('MultiCompany','MultiLocation','Proforma','Sales','Challan','Purchase','PurchaseOrder','Cashbook','ExpenseManagement','CMS','AMC')
                WHERE p.Name = 'ERP + Basic Accounting';

                INSERT INTO ProductPlanModules (PlanId, ModuleId)
                SELECT p.Id, m.Id
                FROM ProductPlans p
                JOIN ProductModules m ON m.IsActive = 1
                WHERE p.Name = 'BizPlus All-in-One';
            ");
        }

        public override void Down()
        {
            Delete.Table("ProductPlanModules");
            Delete.Table("ProductModules");
        }
    }
}
