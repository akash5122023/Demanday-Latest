namespace AdvanceCRM.Settings.Columns
{
    using Serenity.ComponentModel;

    [ColumnsScript("Settings.ProductPlanModule")]
    [BasedOnRow(typeof(ProductPlanModuleRow), CheckNames = true)]
    public class ProductPlanModuleColumns
    {
        [EditLink, Width(150)]
        public string PlanName { get; set; }

        [Width(200)]
        public string ModuleDisplayName { get; set; }
    }
}
