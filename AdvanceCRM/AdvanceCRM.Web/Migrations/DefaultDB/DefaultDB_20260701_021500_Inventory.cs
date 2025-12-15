using FluentMigrator;
using System.Collections.Generic;
using System.Linq;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701021500)]
    public class DefaultDB_20260701_021500_Inventory : Migration
    {
        public override void Up()
        {
            if (Schema.Table("Inventory").Exists())
                return;

            if (!Schema.Table("Products").Exists())
                return;

            Execute.Sql(@"
                SELECT *
                INTO Inventory
                FROM Products
                WHERE 1 = 0;
            ");

            Execute.Sql(@"
                ALTER TABLE Inventory
                ADD CONSTRAINT PK_Inventory PRIMARY KEY (Id);
            ");

            Execute.Sql("SET IDENTITY_INSERT Inventory ON;");

            var columns = new List<string>
            {
                "Id",
                "Name",
                "Code",
                "DivisionId",
                "GroupId",
                "SellingPrice",
                "MRP",
                "Description",
                "TaxId1",
                "TaxId2",
                "Image",
                "TechSpecs",
                "HSN",
                "ChannelCustomerPrice",
                "ResellerPrice",
                "WholesalerPrice",
                "DealerPrice",
                "DistributorPrice",
                "StockiestPrice",
                "NationalDistributorPrice",
                "MinimumStock",
                "MaximumStock",
                "RawMaterial",
                "PurchasePrice",
                "OpeningStock",
                "UnitId"
            };

            var optionalColumns = new[] { "CompanyId", "BranchId", "Quantity", "TrackInventory" };

            foreach (var column in optionalColumns)
            {
                if (Schema.Table("Products").Column(column).Exists())
                    columns.Add(column);
            }

            var columnList = string.Join(", ", columns.Select(x => $"[{x}]"));

            Execute.Sql($@"
                INSERT INTO Inventory ( {columnList} )
                SELECT {columnList}
                FROM Products
            ");

            Execute.Sql("SET IDENTITY_INSERT Inventory OFF;");
        }

        public override void Down()
        {
            if (Schema.Table("Inventory").Exists())
                Delete.Table("Inventory");
        }
    }
}
