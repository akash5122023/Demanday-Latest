
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[Tax]")]
    [DisplayName("Tax"), InstanceName("Tax")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.Tax", Permission = "?")]
    public sealed class TaxRow : Row<TaxRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Name"), Size(100), NotNull, QuickSearch, LookupInclude, Unique,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Type"), Size(100), NotNull, LookupInclude]
        public String Type
        {
            get { return Fields.Type[this]; }
            set { Fields.Type[this] = value; }
        }

        [DisplayName("Percentage"), NotNull, LookupInclude]
        public Double? Percentage
        {
            get { return Fields.Percentage[this]; }
            set { Fields.Percentage[this] = value; }
        }

      
        public TaxRow()
            : base(Fields)
        {
        }
        
        public TaxRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public StringField Type;
            public DoubleField Percentage;
        }
    }
}
