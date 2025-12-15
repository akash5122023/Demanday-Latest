namespace AdvanceCRM.Settings.Forms
{
    using AdvanceCRM.Settings;
    using Serenity.ComponentModel;
    using System.Collections.Generic;
    using System.ComponentModel;

    [FormScript("Settings.ProductPlan")]
    [BasedOnRow(typeof(ProductPlanRow), CheckNames = true)]
    public class ProductPlanForm
    {
        [Category("Plan Details")]
        [HalfWidth]
        public string Name { get; set; }

        [HalfWidth]
        [DecimalEditor(MinValue = "0", Decimals = 2)]
        public decimal PricePerUser { get; set; }

        [HalfWidth]
        public int TrialDays { get; set; }

        [HalfWidth]
        public int UserLimit { get; set; }

        [HalfWidth]
        [DisplayName("Non Operational Users")]
        [Hint("Maximum number of non-operational users allowed in this plan.")]
        public int NonOperationalUsers { get; set; }

        [HalfWidth]
        [Placeholder("e.g. INR")]
        public string Currency { get; set; }

        [HalfWidth]
        public bool IsActive { get; set; }

        [HalfWidth]
        public int? SortOrder { get; set; }

        [HalfWidth]
        [EnumEditor]
        [DisplayName("Badge Label")]
        public PlanBadgeLabel? BadgeLabel { get; set; }

        [HalfWidth]
        [DisplayName("Highlight Badge")]
        public bool BadgeHighlight { get; set; }

        [Category("Modules")]
        [LookupEditor(typeof(ProductModuleRow), Multiple = true)]
        public List<int> ModuleList { get; set; }

        [Category("Default Features")]
        [LookupEditor(typeof(DefaultFeatureRow), Multiple = true)]
        public List<int> FeatureList { get; set; }
    }
}
