
namespace AdvanceCRM.Administration
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Administration"), TableName("[dbo].[OPTLog]")]
    [DisplayName("OTP Log"), InstanceName("Opt Log")]
    [ReadPermission(PermissionKeys.Logs)]

    public sealed class OptLogRow : Row<OptLogRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Phone"), Size(30), NotNull, QuickSearch,NameProperty]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("OTP"), Column("OPT"), NotNull]
        public Double? Opt
        {
            get { return Fields.Opt[this]; }
            set { Fields.Opt[this] = value; }
        }

        [DisplayName("Validity"), NotNull]
        public DateTime? Validity
        {
            get { return Fields.Validity[this]; }
            set { Fields.Validity[this] = value; }
        }

       
        public OptLogRow()
            : base(Fields)
        {
        }
        
        public OptLogRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Phone;
            public DoubleField Opt;
            public DateTimeField Validity;
        }
    }
}
