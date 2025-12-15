using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using Serenity.Services;
using System.ComponentModel;

namespace AdvanceCRM.Administration
{
    [ConnectionKey("Default"), Module("Administration"), TableName("SassApplicationSetting")]
    [DisplayName("SaaS Application Setting"), InstanceName("SaaS Application Setting")]
    [ReadPermission("*"), ModifyPermission("*")]
    public sealed class SassApplicationSettingRow : Row<SassApplicationSettingRow.RowFields>, IIdRow
    {
        [Identity, IdProperty]
        public int? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [Size(100), NotNull, QuickSearch]
        public string Key
        {
            get => fields.Key[this];
            set => fields.Key[this] = value;
        }

        [Size(500)]
        public string Value
        {
            get => fields.Value[this];
            set => fields.Value[this] = value;
        }

        public SassApplicationSettingRow() : base() { }

        public sealed class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Key;
            public StringField Value;
        }
    }
}
