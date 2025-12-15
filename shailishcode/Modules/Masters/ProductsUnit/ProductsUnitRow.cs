
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[ProductsUnit]")]
    [DisplayName("Products Unit"), InstanceName("Products Unit")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.ProductsUnit", Permission = "?")]
    public sealed class ProductsUnitRow : Row<ProductsUnitRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Unit"), Size(200), NotNull, QuickSearch, Unique,NameProperty]
        public String ProductsUnit
        {
            get { return Fields.ProductsUnit[this]; }
            set { Fields.ProductsUnit[this] = value; }
        }

       
        public ProductsUnitRow()
            : base(Fields)
        {
        }
        
        public ProductsUnitRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField ProductsUnit;
        }
    }
}
