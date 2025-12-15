
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[Category]")]
    [DisplayName("Category"), InstanceName("Category")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.Category", Permission = "?")]
    public sealed class CategoryRow : Row<CategoryRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Category"), Size(100), NotNull, QuickSearch, Unique, LookupInclude,NameProperty]
        public String Category
        {
            get { return Fields.Category[this]; }
            set { Fields.Category[this] = value; }
        }

        [DisplayName("Type"), DefaultValue("1"), NotNull, LookupInclude]
        public Int32? Type
        {
            get { return Fields.Type[this]; }
            set { Fields.Type[this] = value; }
        }

     
        public CategoryRow()
            : base(Fields)
        {
        }
        
        public CategoryRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Category;
            public Int32Field Type;
        }
    }
}
