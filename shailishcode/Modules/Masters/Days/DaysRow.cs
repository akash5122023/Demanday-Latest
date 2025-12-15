
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[Days]")]
    [DisplayName("Days"), InstanceName("Days")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.Days", Permission = "?")]
    public sealed class DaysRow : Row<DaysRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Title"), Size(50), QuickSearch, LookupInclude,NameProperty]
        public String Title
        {
            get { return Fields.Title[this]; }
            set { Fields.Title[this] = value; }
        }

        [DisplayName("Heading"), Size(100)]
        public String Heading
        {
            get { return Fields.Heading[this]; }
            set { Fields.Heading[this] = value; }
        }

        [DisplayName("Description"), Size(500)]
        public String Description
        {
            get { return Fields.Description[this]; }
            set { Fields.Description[this] = value; }
        }

        [DisplayName("File Attachments"), Size(1000)]
        [ImageUploadEditor(FilenameFormat = "Days/~", CopyToHistory = true, DisableDefaultBehavior = true)]
        public String FileAttachments
        {
            get { return Fields.FileAttachments[this]; }
            set { Fields.FileAttachments[this] = value; }
        }

       

        public DaysRow()
            : base(Fields)
        {
        }
        
        public DaysRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Title;
            public StringField Heading;
            public StringField Description;
            public StringField FileAttachments;
        }
    }
}
