
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[City]")]
    [DisplayName("City"), InstanceName("City")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.City", Permission = "?")]
    public sealed class CityRow : Row<CityRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("City/District"), Size(200), NotNull, QuickSearch, LookupInclude,NameProperty]
        public String City
        {
            get { return Fields.City[this]; }
            set { Fields.City[this] = value; }
        }

        [DisplayName("State"), NotNull, ForeignKey("[dbo].[State]", "Id"), LeftJoin("jState"), TextualField("State"), LookupInclude]
        [LookupEditor(typeof(StateRow), InplaceAdd = true)]
        public Int32? StateId
        {
            get { return Fields.StateId[this]; }
            set { Fields.StateId[this] = value; }
        }

        [DisplayName("State"), Expression("jState.[State]"), QuickSearch, LookupInclude]
        public String State
        {
            get { return Fields.State[this]; }
            set { Fields.State[this] = value; }
        }

       

        public CityRow()
            : base(Fields)
        {
        }
        
        public CityRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField City;
            public Int32Field StateId;

            public StringField State;
        }
    }
}
