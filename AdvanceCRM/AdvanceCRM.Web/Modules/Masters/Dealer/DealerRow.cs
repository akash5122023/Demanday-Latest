
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[Dealer]")]
    [DisplayName("Dealer"), InstanceName("Dealer")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.Dealer", Permission = "?")]
    public sealed class DealerRow : Row<DealerRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Dealer Name"), Size(150), NotNull, QuickSearch,NameProperty]
        public String DealerName
        {
            get { return Fields.DealerName[this]; }
            set { Fields.DealerName[this] = value; }
        }

        [DisplayName("Phone"), Size(100),LookupInclude]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Email"), Size(100), LookupInclude]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("Address"), Size(500), LookupInclude]
        public String Address
        {
            get { return Fields.Address[this]; }
            set { Fields.Address[this] = value; }
        }

        [DisplayName("City"), ForeignKey("[dbo].[City]", "Id"), LeftJoin("jCity"), TextualField("City")]
        [LookupEditor(typeof(CityRow),InplaceAdd =true, CascadeFrom = "StateId", CascadeValue = "StateId")]
        public Int32? CityId
        {
            get { return Fields.CityId[this]; }
            set { Fields.CityId[this] = value; }
        }

        [DisplayName("State"), ForeignKey("[dbo].[State]", "Id"), LeftJoin("jState"), TextualField("State")]
        [LookupEditor(typeof(StateRow), InplaceAdd = true)]
        public Int32? StateId
        {
            get { return Fields.StateId[this]; }
            set { Fields.StateId[this] = value; }
        }

        [DisplayName("Pin"), Size(50)]
        public String Pin
        {
            get { return Fields.Pin[this]; }
            set { Fields.Pin[this] = value; }
        }

        [DisplayName("Country")]
        public Masters.CountryMaster? Country
        {
            get { return (Masters.CountryMaster?)Fields.Country[this]; }
            set { Fields.Country[this] = (Int32?)value; }
        }

        [DisplayName("Additional Info"), Size(200)]
        public String AdditionalInfo
        {
            get { return Fields.AdditionalInfo[this]; }
            set { Fields.AdditionalInfo[this] = value; }
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

        [DisplayName("State"), Expression("jState.[State]")]
        public String State
        {
            get { return Fields.State[this]; }
            set { Fields.State[this] = value; }
        }

      
        public DealerRow()
            : base(Fields)
        {
        }
        
        public DealerRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField DealerName;
            public StringField Phone;
            public StringField Email;
            public StringField Address;
            public Int32Field CityId;
            public Int32Field StateId;
            public StringField Pin;
            public Int32Field Country;
            public StringField AdditionalInfo;

            public StringField City;
            public Int32Field CityStateId;

            public StringField State;
        }
    }
}
