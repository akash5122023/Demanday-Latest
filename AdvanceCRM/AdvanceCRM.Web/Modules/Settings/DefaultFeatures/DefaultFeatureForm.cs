namespace AdvanceCRM.Settings.Forms
{
    using Serenity.ComponentModel;
    using System.ComponentModel;

    [FormScript("Settings.DefaultFeature")]
    [BasedOnRow(typeof(DefaultFeatureRow), CheckNames = true)]
    public class DefaultFeatureForm
    {
        [Required]
        public string Name { get; set; }
    }
}
