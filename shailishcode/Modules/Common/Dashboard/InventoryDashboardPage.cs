namespace AdvanceCRM.Common.Pages
{
    using AdvanceCRM.Common;
    using AdvanceCRM.Products;
    using AdvanceCRM.Web.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Data.SqlClient;
    using Serenity;
    using Serenity.Data;
    using System;
    using System.Data;
    using System.Linq;

    [Route("InventoryDashboard")]
    public class InventoryDashboardController : Controller
    {
        private readonly ISqlConnections _connections;

        public InventoryDashboardController(ISqlConnections connections)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        [Authorize, HttpGet, Route("~/InventoryDashboard")]
        public ActionResult Index()
        {
            var cachedModel = LocalCache.GetLocalStoreOnly("InventoryDashboardPageModel", TimeSpan.FromMinutes(5),
                InventoryRow.Fields.GenerationKey, () =>
                {
                    var model = new InventoryDashboardPageModel();
                    var o = InventoryRow.Fields;

                    using (var connection = _connections.NewFor<InventoryRow>())
                    {
                        if (!InventoryTableExists(connection))
                        {
                            model.InventoryDataAvailable = false;
                            return model;
                        }

                        try
                        {
                            var inventoryItems = connection.List<InventoryRow>(q => q
                                .SelectTableFields()
                                .Select(o.Branch)
                                .Where(new Criteria("1=1")));

                            // 🔹 Low Stock Items
                            model.LowStockItems = inventoryItems
                                .Where(x => (x.Quantity ?? 0) < (x.MinimumStock ?? 0))
                                .Select(x => new InventoryDashboardPageModel.InventoryItem
                                {
                                    Name = x.Name,
                                    Code = x.Code,
                                    Quantity = x.Quantity ?? 0,
                                    MinimumStock = x.MinimumStock ?? 0,
                                    MaximumStock = x.MaximumStock ?? 0,
                                    PurchasePrice = x.PurchasePrice ?? 0,
                                    TotalValue = (x.Quantity ?? 0) * (x.PurchasePrice ?? 0),
                                    Warehouse = x.Branch
                                }).ToList();

                            // 🔹 Overstock Items
                            model.OverstockItems = inventoryItems
                                .Where(x => (x.Quantity ?? 0) > (x.MaximumStock ?? 0))
                                .Select(x => new InventoryDashboardPageModel.InventoryItem
                                {
                                    Name = x.Name,
                                    Code = x.Code,
                                    Quantity = x.Quantity ?? 0,
                                    MinimumStock = x.MinimumStock ?? 0,
                                    MaximumStock = x.MaximumStock ?? 0,
                                    PurchasePrice = x.PurchasePrice ?? 0,
                                    TotalValue = (x.Quantity ?? 0) * (x.PurchasePrice ?? 0),
                                    Warehouse = x.Branch
                                }).ToList();

                            // 🔹 Total Inventory Value
                            model.TotalInventoryValue = inventoryItems.Sum(x => (x.Quantity ?? 0) * (x.PurchasePrice ?? 0));

                            // 🔹 Stock by Branch
                            model.StockByBranch = inventoryItems
                                .GroupBy(x => x.Branch ?? "Unknown Warehouse")
                                .ToDictionary(
                                    g => g.Key,
                                    g => g.Select(x => new InventoryDashboardPageModel.InventoryItem
                                    {
                                        Name = x.Name,
                                        Code = x.Code,
                                        Quantity = x.Quantity ?? 0,
                                        MinimumStock = x.MinimumStock ?? 0,
                                        MaximumStock = x.MaximumStock ?? 0,
                                        PurchasePrice = x.PurchasePrice ?? 0,
                                        TotalValue = (x.Quantity ?? 0) * (x.PurchasePrice ?? 0)
                                    }).ToList()
                                );
                        }
                        catch (SqlException ex) when (IsMissingInventoryTable(ex))
                        {
                            model.InventoryDataAvailable = false;
                            return model;
                        }
                    }

                    return model;
                });

            return View(MVC.Views.Common.Dashboard.InventoryDashboardIndex, cachedModel);
        }

        private static bool InventoryTableExists(IDbConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM sys.tables WHERE name = @name AND schema_id = SCHEMA_ID(@schema)";

                var nameParameter = command.CreateParameter();
                nameParameter.ParameterName = "@name";
                nameParameter.Value = "Inventory";
                command.Parameters.Add(nameParameter);

                var schemaParameter = command.CreateParameter();
                schemaParameter.ParameterName = "@schema";
                schemaParameter.Value = "dbo";
                command.Parameters.Add(schemaParameter);

                var result = command.ExecuteScalar();
                return Convert.ToInt32(result) > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        private static bool IsMissingInventoryTable(SqlException exception)
        {
            return exception?.Number == 208;
        }
    }
}
