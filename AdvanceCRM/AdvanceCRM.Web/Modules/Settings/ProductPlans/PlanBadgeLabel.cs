namespace AdvanceCRM.Settings
{
    using Serenity.ComponentModel;
    using System.ComponentModel;

    [EnumKey("Settings.PlanBadgeLabel")]
    public enum PlanBadgeLabel
    {
        [Description("None")]
        None = 0,

        [Description("Regular")]
        Regular = 1,

        [Description("Popular")]
        Popular = 2,

        [Description("Best Value")]
        BestValue = 3,

        [Description("Featured")]
        Featured = 4,

        [Description("Limited Time")]
        LimitedTime = 5
    }
}
