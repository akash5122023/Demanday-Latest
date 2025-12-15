
namespace AdvanceCRM.BizMail
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("BizMail"), TableName("[dbo].[BMList]")]
    [DisplayName("Bm List"), InstanceName("Bm List")]
    [ReadPermission("BizMail:Read")]
    [InsertPermission("BizMail:Insert")]
    [UpdatePermission("BizMail:Update")]
    [DeletePermission("BizMail:Delete")]
    [LookupScript("BizMail.BMList", Permission = "?")]
    public sealed class BmListRow : Row<BmListRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("List Id"), Size(200), QuickSearch,LookupInclude]
        public String ListId
        {
            get { return Fields.ListId[this]; }
            set { Fields.ListId[this] = value; }
        }

        [DisplayName("Company Name"), Size(200)]
        public String CompanyName
        {
            get { return Fields.CompanyName[this]; }
            set { Fields.CompanyName[this] = value; }
        }

        [DisplayName("Name"), Size(200),NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("City"), Size(200)]
        public String City
        {
            get { return Fields.City[this]; }
            set { Fields.City[this] = value; }
        }

        [DisplayName("Display Name"), Size(200)]
        public String DisplayName
        {
            get { return Fields.DisplayName[this]; }
            set { Fields.DisplayName[this] = value; }
        }

        [DisplayName("Description"), Size(200)]
        public String Description
        {
            get { return Fields.Description[this]; }
            set { Fields.Description[this] = value; }
        }

        [DisplayName("From"), Size(200)]
        public String From
        {
            get { return Fields.From[this]; }
            set { Fields.From[this] = value; }
        }

        [DisplayName("Reply To"), Size(200)]
        public String ReplyTo
        {
            get { return Fields.ReplyTo[this]; }
            set { Fields.ReplyTo[this] = value; }
        }

       

        public BmListRow()
            : base(Fields)
        {
        }
        
        public BmListRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField ListId;
            public StringField CompanyName;
            public StringField Name;
            public StringField City;
            public StringField DisplayName;
            public StringField Description;
            public StringField From;
            public StringField ReplyTo;
        }
    }
}
