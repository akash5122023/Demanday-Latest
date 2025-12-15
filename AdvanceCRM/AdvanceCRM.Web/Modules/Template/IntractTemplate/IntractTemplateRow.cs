
namespace AdvanceCRM.Template
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Template"), TableName("[dbo].[IntractTemplate]")]
    [DisplayName("Intract Template"), InstanceName("Intract Template")]
    [ReadPermission("Template:IntractTemplate")]
    [ModifyPermission("Template:IntractTemplate")]
    [LookupScript("Template.IntractTemplate", Permission = "?")]
    public sealed class IntractTemplateRow : Row<IntractTemplateRow.RowFields>, IIdRow, INameRow,_Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Created At Utc"), Column("created_at_utc")]
        public String? CreatedAtUtc
        {
            get { return Fields.CreatedAtUtc[this]; }
            set { Fields.CreatedAtUtc[this] = value; }
        }
        [DisplayName("intractid"), Column("intractid"), Size(255), QuickSearch]
        public String IntractId
        {
            get { return Fields.IntractId[this]; }
            set { Fields.IntractId[this] = value; }
        }
        [DisplayName("Name"), Column("name"), Size(255), QuickSearch,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Language"), Column("language"), Size(255)]
        public String Language
        {
            get { return Fields.Language[this]; }
            set { Fields.Language[this] = value; }
        }

        [DisplayName("Category"), Column("category"), Size(255)]
        public String Category
        {
            get { return Fields.Category[this]; }
            set { Fields.Category[this] = value; }
        }

        [DisplayName("Template Category Label"), Column("template_category_label"), Size(255)]
        public String TemplateCategoryLabel
        {
            get { return Fields.TemplateCategoryLabel[this]; }
            set { Fields.TemplateCategoryLabel[this] = value; }
        }

        [DisplayName("Header Format"), Column("header_format"), Size(255)]
        public String HeaderFormat
        {
            get { return Fields.HeaderFormat[this]; }
            set { Fields.HeaderFormat[this] = value; }
        }

        [DisplayName("Header"), Column("header"), Size(255)]
        public String Header
        {
            get { return Fields.Header[this]; }
            set { Fields.Header[this] = value; }
        }

        [DisplayName("Body"), Column("body")]
        public String Body
        {
            get { return Fields.Body[this]; }
            set { Fields.Body[this] = value; }
        }

        [DisplayName("Footer"), Column("footer")]
        public String Footer
        {
            get { return Fields.Footer[this]; }
            set { Fields.Footer[this] = value; }
        }

        [DisplayName("Buttons"), Column("buttons")]
        public String Buttons
        {
            get { return Fields.Buttons[this]; }
            set { Fields.Buttons[this] = value; }
        }

        [DisplayName("Autosubmitted For"), Column("autosubmitted_for")]
        public String AutosubmittedFor
        {
            get { return Fields.AutosubmittedFor[this]; }
            set { Fields.AutosubmittedFor[this] = value; }
        }

        [DisplayName("Display Name"), Column("display_name")]
        public String DisplayName
        {
            get { return Fields.DisplayName[this]; }
            set { Fields.DisplayName[this] = value; }
        }

        [DisplayName("Approval Status"), Column("approval_status"), Size(255)]
        public String ApprovalStatus
        {
            get { return Fields.ApprovalStatus[this]; }
            set { Fields.ApprovalStatus[this] = value; }
        }

        [DisplayName("Wa Template Id"), Column("wa_template_id")]
        public String WaTemplateId
        {
            get { return Fields.WaTemplateId[this]; }
            set { Fields.WaTemplateId[this] = value; }
        }

        [DisplayName("Variable Present"), Column("variable_present")]
        public String VariablePresent
        {
            get { return Fields.VariablePresent[this]; }
            set { Fields.VariablePresent[this] = value; }
        }

        [DisplayName("Header File Url"), Column("header_handle_file_url")]
        public String header_handle_file_url
        {
            get { return Fields.header_handle_file_url[this]; }
            set { Fields.header_handle_file_url[this] = value; }
        }

      

        public IntractTemplateRow()
            : base(Fields)
        {
        }
        

        public IntractTemplateRow(RowFields fieldsW)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField CreatedAtUtc;
            public StringField Name;
            public StringField IntractId;
            public StringField Language;
            public StringField Category;
            public StringField TemplateCategoryLabel;
            public StringField HeaderFormat;
            public StringField Header;
            public StringField Body;
            public StringField Footer;
            public StringField Buttons;
            public StringField AutosubmittedFor;
            public StringField DisplayName;
            public StringField ApprovalStatus;
            public StringField WaTemplateId;
            public StringField VariablePresent;
            public StringField header_handle_file_url;

        }
    }
}
