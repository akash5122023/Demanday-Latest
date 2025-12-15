
namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[WatiConfig]")]
    [DisplayName("Wati Config"), InstanceName("Wati Config")]
    [ReadPermission("Settings:SMS")]
    [ModifyPermission("Settings:SMS")]
    public sealed class WatiConfigRow : Row<WatiConfigRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Url"), Column("URL"), Size(200), QuickSearch,NameProperty]
        public String Url
        {
            get { return Fields.Url[this]; }
            set { Fields.Url[this] = value; }
        }

        [DisplayName("Token"), Size(1000)]
        public String Token
        {
            get { return Fields.Token[this]; }
            set { Fields.Token[this] = value; }
        }

    

        public WatiConfigRow()
            : base(Fields)
        {
        }
        
        public WatiConfigRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Url;
            public StringField Token;
        }
    }
}
