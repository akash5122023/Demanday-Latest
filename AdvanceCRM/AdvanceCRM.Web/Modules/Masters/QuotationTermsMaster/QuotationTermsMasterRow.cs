
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[QuotationTermsMaster]")]
    [DisplayName("Quotation Terms Master"), InstanceName("Quotation Terms Master")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.QuotationTermsMaster", Permission = "?")]
    public sealed class QuotationTermsMasterRow : Row<QuotationTermsMasterRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Terms"), Size(1000), NotNull, QuickSearch, LookupInclude, TextAreaEditor(Rows = 4), Unique,NameProperty]
        public String Terms
        {
            get { return Fields.Terms[this]; }
            set { Fields.Terms[this] = value; }
        }

        

        public QuotationTermsMasterRow()
            : base(Fields)
        {
        }
        public QuotationTermsMasterRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Terms;
        }
    }
}
