namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[DefaultFeatures]")]
    [DisplayName("Default Feature"), InstanceName("Default Feature")]
    [ReadPermission("Settings:ProductPlans")]
    [ModifyPermission("Settings:ProductPlans")]
    [LookupScript]
    public sealed class DefaultFeatureRow : Row<DefaultFeatureRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => Fields.Id[this];
            set => Fields.Id[this] = value;
        }

        [DisplayName("Name"), Size(150), NotNull, QuickSearch, NameProperty]
        public String Name
        {
            get => Fields.Name[this];
            set => Fields.Name[this] = value;
        }

        public DefaultFeatureRow()
            : base(Fields)
        {
        }

        public DefaultFeatureRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
        }
    }
}
