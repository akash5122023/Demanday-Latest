
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[AccountingHeads]")]
    [DisplayName("Accounting Heads"), InstanceName("Accounting Heads")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.AccountingHeads", Permission = "?")]
    public sealed class AccountingHeadsRow : Row<AccountingHeadsRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, LookupInclude,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Head"), Size(200), NotNull, QuickSearch, LookupInclude, Unique,NameProperty]
        public String Head
        {
            get { return Fields.Head[this]; }
            set { Fields.Head[this] = value; }
        }

        [DisplayName("Type"), NotNull, LookupInclude]
        public Masters.TransactionTypeMaster? Type
        {
            get { return (Masters.TransactionTypeMaster?)Fields.Type[this]; }
            set { Fields.Type[this] = (Int32?)value; }
        }

      

        public AccountingHeadsRow()
            : base(Fields)
        {
        }
        
        public AccountingHeadsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Head;
            public Int32Field Type;

            public RowFields()
                : base()
            {
                LocalTextPrefix = "Masters.AccountingHeads";
            }
        }
    }
}
