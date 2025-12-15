namespace AdvanceCRM.Settings.Forms
{
    using Serenity.ComponentModel;
    using System.ComponentModel;

    [FormScript("Settings.ProductModule")]
    [BasedOnRow(typeof(ProductModuleRow), CheckNames = true)]
    public class ProductModuleForm
    {
        [Category("Module Details")]
        [HalfWidth]
        public string Name { get; set; }

        [HalfWidth]
        public string DisplayName { get; set; }

        [TextAreaEditor(Rows = 3)]
        public string Description { get; set; }

        [Category("Configuration")]
        [HalfWidth, DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [Category("Pricing")]
        [HalfWidth, DecimalEditor(MinValue = "0", Decimals = 2)]
        public decimal? Price { get; set; }

        [HalfWidth]
        public string Currency { get; set; }
    }
}
