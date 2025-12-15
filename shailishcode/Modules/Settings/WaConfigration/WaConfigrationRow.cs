
namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[WAConfigration]")]
    [DisplayName("Wa Configration"), InstanceName("Wa Configration")]
    [ReadPermission("Settings:WA")]
    [ModifyPermission("Settings:WA")]
    public sealed class WaConfigrationRow : Row<WaConfigrationRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Mobile"), Size(50), NotNull, QuickSearch,NameProperty]
        public String Mobile
        {
            get { return Fields.Mobile[this]; }
            set { Fields.Mobile[this] = value; }
        }

        [DisplayName("Api Key"), Column("Api-Key"), Size(100)]
        public String ApiKey
        {
            get { return Fields.ApiKey[this]; }
            set { Fields.ApiKey[this] = value; }
        }

        [DisplayName("Message Api"), Column("MessageAPI"), Size(2048), NotNull]
        public String MessageApi
        {
            get { return Fields.MessageApi[this]; }
            set { Fields.MessageApi[this] = value; }
        }

        [DisplayName("Media Api"), Column("MediaAPI"), Size(2048), NotNull]
        public String MediaApi
        {
            get { return Fields.MediaApi[this]; }
            set { Fields.MediaApi[this] = value; }
        }

        [DisplayName("Success Response"), Size(2048), NotNull]
        public String SuccessResponse
        {
            get { return Fields.SuccessResponse[this]; }
            set { Fields.SuccessResponse[this] = value; }
        }


        public WaConfigrationRow()
            : base(Fields)
        {
        }
        public WaConfigrationRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Mobile;
            public StringField ApiKey;
            public StringField MessageApi;
            public StringField MediaApi;
            public StringField SuccessResponse;
        }
    }
}
