
namespace AdvanceCRM.Administration
{
    using AdvanceCRM.Masters;
    using AdvanceCRM.Scripts;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Administration"), TableName("[dbo].[Branch]")]
    [DisplayName("Branch"), InstanceName("Branch")]
    [ReadPermission(PermissionKeys.General)]
    [ModifyPermission(PermissionKeys.General)]
    [LookupScript("Administration.Branch", Permission = "?")]
    public sealed class BranchRow : Row<BranchRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Branch"), Size(200), NotNull, QuickSearch,NameProperty]
        public String Branch
        {
            get { return Fields.Branch[this]; }
            set { Fields.Branch[this] = value; }
        }

        [DisplayName("Phone"), Size(50)]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Email"), Size(200)]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("Address"), Size(800)]
        public String Address
        {
            get { return Fields.Address[this]; }
            set { Fields.Address[this] = value; }
        }

        [DisplayName("City"), ForeignKey("[dbo].[City]", "Id"), LeftJoin("jCity"), TextualField("City")]
        [LookupEditor(typeof(CityRow), InplaceAdd = true)]
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

        [DisplayName("Company"), ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), NotNull, LookupInclude]
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
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

       
       

        public BranchRow()
            : base(Fields)
        {
        }
        public BranchRow(RowFields fields)
          : base(fields)
        {
        }
        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Branch;
            public StringField Phone;
            public StringField Email;
            public StringField Address;
            public Int32Field CityId;
            public Int32Field StateId;
            public StringField Pin;
            public Int32Field Country;
            public Int32Field CompanyId;

            public StringField City;
            public Int32Field CityStateId;

            public StringField State;
        }
    }
}
