
namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[BizWAConfig]")]
    [DisplayName("Biz WA Config"), InstanceName("Biz WA Config")]
    [ReadPermission("Settings:BizWA")]
    [ModifyPermission("Settings:BizWA")]
    public sealed class BizWaConfigRow : Row<BizWaConfigRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Whats App No"), Size(100), QuickSearch,NameProperty]
        public String WhatsAppNo
        {
            get { return Fields.WhatsAppNo[this]; }
            set { Fields.WhatsAppNo[this] = value; }
        }

        [DisplayName("Phone No Id"), Column("PhoneNoID"), Size(100)]
        public String PhoneNoId
        {
            get { return Fields.PhoneNoId[this]; }
            set { Fields.PhoneNoId[this] = value; }
        }

        [DisplayName("Wbaid"), Column("WBAID"), Size(100)]
        public String Wbaid
        {
            get { return Fields.Wbaid[this]; }
            set { Fields.Wbaid[this] = value; }
        }

        [DisplayName("Access Token"),TextAreaEditor(Rows =5)]
        public String Accesstoken
        {
            get { return Fields.Accesstoken[this]; }
            set { Fields.Accesstoken[this] = value; }
        }

        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

       

        public BizWaConfigRow()
            : base(Fields)
        {
        }
        

        public BizWaConfigRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField WhatsAppNo;
            public StringField PhoneNoId;
            public StringField Wbaid;
            public StringField Accesstoken;
        }
    }
}
