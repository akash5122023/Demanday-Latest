namespace AdvanceCRM.Settings.Columns
{
    using Serenity.ComponentModel;
    using System.ComponentModel;

    [ColumnsScript("Settings.DefaultFeature")]
    [BasedOnRow(typeof(DefaultFeatureRow), CheckNames = true)]
    public class DefaultFeatureColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public int Id { get; set; }

        [EditLink]
        public string Name { get; set; }
    }
}
