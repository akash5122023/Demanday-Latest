using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Settings
{
    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[AiConfiguration]")]
    [DisplayName("Ai Configuration"), InstanceName("Ai Configuration")]
    [ReadPermission("Settings:Ai Configuration")]
    [ModifyPermission("Settings:Ai Configuration")]
    public sealed class AiConfigurationRow : Row<AiConfigurationRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Ai Key"), Column("AI_KEY"), Size(255), NotNull, QuickSearch, NameProperty]
        public String AiKey
        {
            get => fields.AiKey[this];
            set => fields.AiKey[this] = value;
        }

        public AiConfigurationRow()
            : base()
        {
        }

        public AiConfigurationRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField AiKey;
        }
    }
}
