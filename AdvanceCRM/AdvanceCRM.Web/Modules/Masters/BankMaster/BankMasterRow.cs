
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[BankMaster]")]
    [DisplayName("Bank Master"), InstanceName("Bank Master")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.BankMaster", Permission = "?")]
    public sealed class BankMasterRow : Row<BankMasterRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Bank Name"), Size(100), NotNull, QuickSearch,NameProperty]
        public String BankName
        {
            get { return Fields.BankName[this]; }
            set { Fields.BankName[this] = value; }
        }

        [DisplayName("Account Number"), Size(100), NotNull]
        public String AccountNumber
        {
            get { return Fields.AccountNumber[this]; }
            set { Fields.AccountNumber[this] = value; }
        }

        [DisplayName("IFSC"), Column("IFSC"), Size(100)]
        public String IFSC
        {
            get { return Fields.IFSC[this]; }
            set { Fields.IFSC[this] = value; }
        }

        [DisplayName("Type"), Size(100)]
        public String Type
        {
            get { return Fields.Type[this]; }
            set { Fields.Type[this] = value; }
        }

        [DisplayName("Branch"), Size(100)]
        public String Branch
        {
            get { return Fields.Branch[this]; }
            set { Fields.Branch[this] = value; }
        }

        [DisplayName("Additional Info"), Size(400), TextAreaEditor(Rows = 4)]
        public String AdditionalInfo
        {
            get { return Fields.AdditionalInfo[this]; }
            set { Fields.AdditionalInfo[this] = value; }
        }

       

        public BankMasterRow()
            : base(Fields)
        {
        }
        public BankMasterRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField BankName;
            public StringField AccountNumber;
            public StringField IFSC;
            public StringField Type;
            public StringField Branch;
            public StringField AdditionalInfo;
        }
    }
}
