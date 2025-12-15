
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[WoocommerceDetails]")]
    [DisplayName("Woocommerce Details"), InstanceName("Woocommerce Details")]
    [ReadPermission("Woocommerce:Inbox")]
    [ModifyPermission("Woocommerce:Inbox")]
    public sealed class WoocommerceDetailsRow : Row<WoocommerceDetailsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Feedback"), Size(500)]
        public String Feedback
        {
            get { return Fields.Feedback[this]; }
            set { Fields.Feedback[this] = value; }
        }

        [DisplayName("First Name"), Size(255), QuickSearch,NameProperty]
        public String FirstName
        {
            get { return Fields.FirstName[this]; }
            set { Fields.FirstName[this] = value; }
        }

        [DisplayName("Last Name"), Size(255)]
        public String LastName
        {
            get { return Fields.LastName[this]; }
            set { Fields.LastName[this] = value; }
        }

        [DisplayName("Company"), Size(255)]
        public String Company
        {
            get { return Fields.Company[this]; }
            set { Fields.Company[this] = value; }
        }

        [DisplayName("Email"), Size(50)]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("Phone"), Size(20)]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Address"), Size(255)]
        public String Address
        {
            get { return Fields.Address[this]; }
            set { Fields.Address[this] = value; }
        }

        [DisplayName("City"), Size(255)]
        public String City
        {
            get { return Fields.City[this]; }
            set { Fields.City[this] = value; }
        }

        [DisplayName("State"), Size(255)]
        public String State
        {
            get { return Fields.State[this]; }
            set { Fields.State[this] = value; }
        }

        [DisplayName("Country"), Size(255)]
        public String Country
        {
            get { return Fields.Country[this]; }
            set { Fields.Country[this] = value; }
        }

        [DisplayName("Created Date"), NotNull]
        public DateTime? CreatedDate
        {
            get { return Fields.CreatedDate[this]; }
            set { Fields.CreatedDate[this] = value; }
        }

        [DisplayName("Is Moved"), NotNull]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }
        [DisplayName("WCId"), Column("WooID"), Size(255)]
        public String WooId
        {
            get { return Fields.WooId[this]; }
            set { Fields.WooId[this] = value; }
        }

      

        public WoocommerceDetailsRow()
            : base(Fields)
        {
        }
        public WoocommerceDetailsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField WooId;
            public StringField FirstName;
            public StringField LastName;
            public StringField Company;
            public StringField Email;
            public StringField Phone;
            public StringField Address;
            public StringField City;
            public StringField State;
            public StringField Country;
            public DateTimeField CreatedDate;
            public BooleanField IsMoved;
            public StringField Feedback;
        }
    }
}
