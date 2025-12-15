namespace AdvanceCRM.Settings.Forms
{
    using AdvanceCRM.Settings;
    using Serenity.ComponentModel;

    [FormScript("Settings.ProductPlanModule")]
    [BasedOnRow(typeof(ProductPlanModuleRow), CheckNames = true)]
    public class ProductPlanModuleForm
    {
        [HalfWidth, LookupEditor(typeof(ProductPlanRow))]
        public int? PlanId { get; set; }

        [HalfWidth, LookupEditor(typeof(ProductModuleRow), FilterField = "IsActive", FilterValue = "1")]
        public int? ModuleId { get; set; }
    }
}
