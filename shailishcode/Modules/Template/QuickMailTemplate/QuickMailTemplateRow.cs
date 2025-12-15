
namespace AdvanceCRM.Template
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Template"), TableName("[dbo].[QuickMailTemplate]")]
    [DisplayName("Quick Mail Template"), InstanceName("Quick Mail Template")]
    [ReadPermission("Template:QuickMailTemplate")]
    [ModifyPermission("Template:QuickMailTemplate")]
    [LookupScript("Template.QuickMailTemplate", Permission = "?")]
    public sealed class QuickMailTemplateRow : Row<QuickMailTemplateRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Name"), QuickSearch,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }


        [DisplayName("Subject"), QuickSearch]
        [LookupInclude] // ✅ Include in lookup

        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }

        [DisplayName("Message")]
        [LookupInclude] // ✅ Include in lookup

        public String Message
        {
            get { return Fields.Message[this]; }
            set { Fields.Message[this] = value; }
        }

        [DisplayName("Attachments")]
        [LookupInclude] // ✅ Include in lookup

        public String Attachments
        {
            get { return Fields.Attachments[this]; }
            set { Fields.Attachments[this] = value; }
        }

      
        public QuickMailTemplateRow()
            : base(Fields)
        {
        }
        public QuickMailTemplateRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Subject;
            public StringField Name;
            public StringField Message;
            public StringField Attachments;
        }
    }
}
