
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[ProductsGroup]")]
    [DisplayName("Products Group"), InstanceName("Products Group")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.ProductsGroup", Permission = "?")]
    public sealed class ProductsGroupRow : Row<ProductsGroupRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Products Group"), Size(200), NotNull, QuickSearch, Unique,NameProperty]
        public String ProductsGroup
        {
            get { return Fields.ProductsGroup[this]; }
            set { Fields.ProductsGroup[this] = value; }
        }

      

        public ProductsGroupRow()
            : base(Fields)
        {
        }
        
        public ProductsGroupRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField ProductsGroup;
        }
    }
}
