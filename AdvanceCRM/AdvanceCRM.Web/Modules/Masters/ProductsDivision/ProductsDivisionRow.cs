
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[ProductsDivision]")]
    [DisplayName("Products Division"), InstanceName("Products Division")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.ProductsDivision", Permission = "?")]
    public sealed class ProductsDivisionRow : Row<ProductsDivisionRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Products Division"), Size(200), NotNull, QuickSearch,NameProperty]
        public String ProductsDivision
        {
            get { return Fields.ProductsDivision[this]; }
            set { Fields.ProductsDivision[this] = value; }
        }

      

        public ProductsDivisionRow()
            : base(Fields)
        {
        }
        
        public ProductsDivisionRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField ProductsDivision;
        }
    }
}
