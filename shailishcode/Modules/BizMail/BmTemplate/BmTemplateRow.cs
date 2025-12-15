
namespace AdvanceCRM.BizMail
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("BizMail"), TableName("[dbo].[BMTemplate]")]
    [DisplayName("Bm Template"), InstanceName("Bm Template")]
    [ReadPermission("BizMail:Read")]
    [InsertPermission("BizMail:Insert")]
    [UpdatePermission("BizMail:Update")]
    [DeletePermission("BizMail:Delete")]
    [LookupScript("BizMail.BMTemplate", Permission = "?")]
    public sealed class BmTemplateRow : Row<BmTemplateRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Name"), Size(200), QuickSearch,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Content"),NotNull,TextAreaEditor(Rows =5)]
        public String Content
        {
            get { return Fields.Content[this]; }
            set { Fields.Content[this] = value; }
        }

        [DisplayName("Inline Css"), Column("InlineCSS")]
        public Int32? InlineCss
        {
            get { return Fields.InlineCss[this]; }
            set { Fields.InlineCss[this] = value; }
        }
        [DisplayName("Template Uid"), Column("TemplateUID"), Size(200),LookupInclude]
        public String TemplateUid
        {
            get { return Fields.TemplateUid[this]; }
            set { Fields.TemplateUid[this] = value; }
        }


        public BmTemplateRow()
            : base(Fields)
        {
        }
        
        public BmTemplateRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public StringField Content;
            public Int32Field InlineCss;
            public StringField TemplateUid;
        }
    }
}
