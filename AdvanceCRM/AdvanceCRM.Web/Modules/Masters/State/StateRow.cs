
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[State]")]
    [DisplayName("State"), InstanceName("State")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.State", Permission = "?")]
    public sealed class StateRow : Row<StateRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("State"), Size(200), NotNull, QuickSearch,NameProperty]
        public String State
        {
            get { return Fields.State[this]; }
            set { Fields.State[this] = value; }
        }

      

        public StateRow()
            : base(Fields)
        {
        }
        
        public StateRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField State;
        }
    }
}
