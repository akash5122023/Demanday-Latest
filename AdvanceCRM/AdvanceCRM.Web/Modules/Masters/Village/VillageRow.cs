
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[Village]")]
    [DisplayName("Village"), InstanceName("Village")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.Village", Permission = "?")]
    public sealed class VillageRow : Row<VillageRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Village"), Size(200), NotNull, QuickSearch, LookupInclude,NameProperty]
        public String Village
        {
            get { return Fields.Village[this]; }
            set { Fields.Village[this] = value; }
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

        [DisplayName("Tehsil"), NotNull, ForeignKey("[dbo].[Tehsil]", "Id"), LeftJoin("jTehsil"), TextualField("Tehsil"), LookupInclude]
        [LookupEditor(typeof(TehsilRow), InplaceAdd = true, CascadeFrom = "CityId", CascadeValue = "CityId")]
        public Int32? TehsilId
        {
            get { return Fields.TehsilId[this]; }
            set { Fields.TehsilId[this] = value; }
        }

        [DisplayName("PIN"), Size(50), QuickSearch, LookupInclude]
        public String PIN
        {
            get { return Fields.PIN[this]; }
            set { Fields.PIN[this] = value; }
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

        [DisplayName("Tehsil"), Expression("jTehsil.[Tehsil]")]
        public String Tehsil
        {
            get { return Fields.Tehsil[this]; }
            set { Fields.Tehsil[this] = value; }
        }

        [DisplayName("Tehsil State Id"), Expression("jTehsil.[StateId]")]
        public Int32? TehsilStateId
        {
            get { return Fields.TehsilStateId[this]; }
            set { Fields.TehsilStateId[this] = value; }
        }

        [DisplayName("Tehsil City Id"), Expression("jTehsil.[CityId]")]
        public Int32? TehsilCityId
        {
            get { return Fields.TehsilCityId[this]; }
            set { Fields.TehsilCityId[this] = value; }
        }

       

        public VillageRow()
            : base(Fields)
        {
        }
        public VillageRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Village;
            public Int32Field StateId;
            public Int32Field CityId;
            public Int32Field TehsilId;
            public StringField PIN;

            public StringField State;

            public StringField City;
            public Int32Field CityStateId;

            public StringField Tehsil;
            public Int32Field TehsilStateId;
            public Int32Field TehsilCityId;
        }
    }
}
