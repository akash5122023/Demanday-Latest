using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200706014512)]
    public class DefaultDB_20200706_014512_IncreaseInfoSize : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("Contacts").AlterColumn("AdditionalInfo").AsString(2048).Nullable();
            Alter.Table("Enquiry").AlterColumn("AdditionalInfo").AsString(2048).Nullable();
            Alter.Table("Quotation").AlterColumn("AdditionalInfo").AsString(2048).Nullable();
            Alter.Table("CMS").AlterColumn("AdditionalInfo").AsString(2048).Nullable();
            Alter.Table("Invoice").AlterColumn("AdditionalInfo").AsString(2048).Nullable();
            Alter.Table("AMC").AlterColumn("AdditionalInfo").AsString(2048).Nullable();
            Alter.Table("Purchase").AlterColumn("AdditionalInfo").AsString(2048).Nullable();
            Alter.Table("Sales").AlterColumn("AdditionalInfo").AsString(2048).Nullable();
            Alter.Table("Challan").AlterColumn("AdditionalInfo").AsString(2048).Nullable();
            Alter.Table("StockTransfer").AlterColumn("AdditionalInfo").AsString(2048).Nullable();
            Alter.Table("SalesReturn").AlterColumn("AdditionalInfo").AsString(2048).Nullable();
            Alter.Table("PurchaseReturn").AlterColumn("AdditionalInfo").AsString(2048).Nullable();
            Alter.Table("ExpenseManagement").AlterColumn("AdditionalInfo").AsString(2048).Nullable();
            Alter.Table("Tasks").AlterColumn("Details").AsString(2048).Nullable();
        }
    }
}