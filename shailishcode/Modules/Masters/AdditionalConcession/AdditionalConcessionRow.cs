
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[AdditionalConcession]")]
    [DisplayName("Additional Concession"), InstanceName("Additional Concession")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.AdditionalConcession", Permission = "?")]
    public sealed class AdditionalConcessionRow : Row<AdditionalConcessionRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Name"), NotNull, QuickSearch, LookupInclude,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Percentage"), LookupInclude]
        public Double? Percentage
        {
            get { return Fields.Percentage[this]; }
            set { Fields.Percentage[this] = value; }
        }
        [DisplayName("Amount")]
        public Double? Amount
        {
            get { return Fields.Amount[this]; }
            set { Fields.Amount[this] = value; }
        }

      

        public AdditionalConcessionRow()
            : base(Fields)
        {
        }
        
        public AdditionalConcessionRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public DoubleField Percentage;
            public DoubleField Amount;
        }
    }
}
