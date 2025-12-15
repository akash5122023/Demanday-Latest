using FluentMigrator;
using System.Collections.Generic;
using System.Linq;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251009143000)]
    public class DefaultDB_20251009_143000_Inventory : Migration
    {
        public override void Up()
        {
            // Step 1: Create the Inventory table structure based on Products
            Execute.Sql(@"
                SELECT *
                INTO Inventory
                FROM Products
                WHERE 1 = 0;
            ");

            // Step 2: Add primary key constraint to the Id column
            Execute.Sql(@"
                ALTER TABLE Inventory
                ADD CONSTRAINT PK_Inventory PRIMARY KEY (Id);
            ");
            // Enable IDENTITY_INSERT for Inventory
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
            // Disable IDENTITY_INSERT for Inventory
            Execute.Sql("SET IDENTITY_INSERT Inventory OFF;");
        }
        public override void Down()
        {
            Delete.Table("Inventory");
        }
    }
}
