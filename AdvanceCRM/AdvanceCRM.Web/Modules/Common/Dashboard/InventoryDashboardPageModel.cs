using System.Collections.Generic;

namespace AdvanceCRM.Common
{
    public class InventoryDashboardPageModel
    {
        public bool InventoryDataAvailable { get; set; } = true;
        public List<InventoryItem> LowStockItems { get; set; } = new();
        public List<InventoryItem> OverstockItems { get; set; } = new();
        public List<InventoryItem> ProductPerformance { get; set; } = new();
        public double TotalInventoryValue { get; set; }
        public Dictionary<string, List<InventoryItem>> StockByBranch { get; set; } = new();

        public class InventoryItem
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public double Quantity { get; set; }
            public double MinimumStock { get; set; }
            public double MaximumStock { get; set; }
            public double PurchasePrice { get; set; }
            public double TotalValue { get; set; }
            public string Warehouse { get; set; }
        }
    }
}
