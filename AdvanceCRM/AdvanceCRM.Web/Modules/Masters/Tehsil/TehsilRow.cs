
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[Tehsil]")]
    [DisplayName("Tehsil"), InstanceName("Tehsil")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.Tehsil", Permission = "?")]
    public sealed class TehsilRow : Row<TehsilRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Tehsil"), Size(200), NotNull, QuickSearch, LookupInclude,NameProperty]
        public String Tehsil
        {
            get { return Fields.Tehsil[this]; }
            set { Fields.Tehsil[this] = value; }
        }

        [DisplayName("State"), NotNull, ForeignKey("[dbo].[State]", "Id"), LeftJoin("jState"), TextualField("State"), LookupInclude]
        [LookupEditor(typeof(StateRow), InplaceAdd = true)]
        public Int32? StateId
        {
            get { return Fields.StateId[this]; }
            set { Fields.StateId[this] = value; }
        }

        [DisplayName("City/District"), NotNull, ForeignKey("[dbo].[City]", "Id"), LeftJoin("jCity"), TextualField("City"), LookupInclude]
        [LookupEditor(typeof(CityRow), InplaceAdd = true, CascadeFrom = "StateId", CascadeValue = "StateId")]
        public Int32? CityId
        {
            get { return Fields.CityId[this]; }
            set { Fields.CityId[this] = value; }
        }

        [DisplayName("State"), Expression("jState.[State]")]
        public String State
        {
            get { return Fields.State[this]; }
            set { Fields.State[this] = value; }
        }

        [DisplayName("City"), Expression("jCity.[City]")]
        public String City
        {
            get { return Fields.City[this]; }
            set { Fields.City[this] = value; }
        }

        [DisplayName("City State Id"), Expression("jCity.[StateId]")]
        public Int32? CityStateId
        {
            get { return Fields.CityStateId[this]; }
            set { Fields.CityStateId[this] = value; }
        }

      

        public TehsilRow()
            : base(Fields)
        {
        }
        
        public TehsilRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Tehsil;
            public Int32Field StateId;
            public Int32Field CityId;

            public StringField State;

            public StringField City;
            public Int32Field CityStateId;
        }
    }
}
