
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[Transportation]")]
    [DisplayName("Transportation"), InstanceName("Transportation")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.Transportation", Permission = "?")]
    public sealed class TransportationRow : Row<TransportationRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Name"), Size(100), NotNull, QuickSearch, Unique,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Address"), Size(1000), NotNull, TextAreaEditor(Rows = 4)]
        public String Address
        {
            get { return Fields.Address[this]; }
            set { Fields.Address[this] = value; }
        }

        [DisplayName("Phone"), Size(50), NotNull]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Email"), Size(200), NotNull, EmailEditor]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("Contact Person"), Size(100)]
        public String ContactPerson
        {
            get { return Fields.ContactPerson[this]; }
            set { Fields.ContactPerson[this] = value; }
        }

        [DisplayName("Contact Person Phone"), Size(100)]
        public String ContactPersonPhone
        {
            get { return Fields.ContactPersonPhone[this]; }
            set { Fields.ContactPersonPhone[this] = value; }
        }

        [DisplayName("GSTIN"), Column("GSTIN"), Size(100)]
        public String GSTIN
        {
            get { return Fields.GSTIN[this]; }
            set { Fields.GSTIN[this] = value; }
        }

        

        public TransportationRow()
            : base(Fields)
        {
        }
        public TransportationRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public StringField Address;
            public StringField Phone;
            public StringField Email;
            public StringField ContactPerson;
            public StringField ContactPersonPhone;
            public StringField GSTIN;
        }
    }
}
