
namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[BizMailConfig]")]
    [DisplayName("Biz Mail Config"), InstanceName("Biz Mail Config")]
    [ReadPermission("Settings:BizMail")]
    [ModifyPermission("Settings:BizMail")]
    public sealed class BizMailConfigRow : Row<BizMailConfigRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Apiurl"), Column("apiurl"), Size(200), QuickSearch,NameProperty]
        public String Apiurl
        {
            get { return Fields.Apiurl[this]; }
            set { Fields.Apiurl[this] = value; }
        }

        [DisplayName("Apikey"), Column("apikey"), Size(200)]
        public String Apikey
        {
            get { return Fields.Apikey[this]; }
            set { Fields.Apikey[this] = value; }
        }

        [DisplayName("From Name"), Size(100)]
        public String FromName
        {
            get { return Fields.FromName[this]; }
            set { Fields.FromName[this] = value; }
        }

        [DisplayName("From Mail"), Size(100)]
        public String FromMail
        {
            get { return Fields.FromMail[this]; }
            set { Fields.FromMail[this] = value; }
        }

        [DisplayName("Reply To Name"), Size(100)]
        public String ReplyToName
        {
            get { return Fields.ReplyToName[this]; }
            set { Fields.ReplyToName[this] = value; }
        }

        [DisplayName("Reply To Mail"), Size(100)]
        public String ReplyToMail
        {
            get { return Fields.ReplyToMail[this]; }
            set { Fields.ReplyToMail[this] = value; }
        }


        

        public BizMailConfigRow()
            : base(Fields)
        {
        }

        public BizMailConfigRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Apiurl;
            public StringField Apikey;

            public StringField FromName;
            public StringField FromMail;
            public StringField ReplyToName;
            public StringField ReplyToMail;
        }
    }
}
