
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[WatiContacts]")]
    [DisplayName("Wati Contacts"), InstanceName("Wati Contacts")]
    [ReadPermission("WatiContacts:Inbox")]
    [ModifyPermission("WatiContacts:Inbox")]
    public sealed class WatiContactsRow : Row<WatiContactsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Waid"), Column("WAID"), Size(50), QuickSearch,NameProperty]
        public String Waid
        {
            get { return Fields.Waid[this]; }
            set { Fields.Waid[this] = value; }
        }

        [DisplayName("Firt Name"), Size(50)]
        public String FirtName
        {
            get { return Fields.FirtName[this]; }
            set { Fields.FirtName[this] = value; }
        }

        [DisplayName("Full Name"), Size(50)]
        public String FullName
        {
            get { return Fields.FullName[this]; }
            set { Fields.FullName[this] = value; }
        }

        [DisplayName("Phone"), Size(50)]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Source"), Size(50)]
        public String Source
        {
            get { return Fields.Source[this]; }
            set { Fields.Source[this] = value; }
        }

        [DisplayName("Status"), Size(50)]
        public String Status
        {
            get { return Fields.Status[this]; }
            set { Fields.Status[this] = value; }
        }

        [DisplayName("Created")]
        public DateTime? Created
        {
            get { return Fields.Created[this]; }
            set { Fields.Created[this] = value; }
        }

        [DisplayName("Is Moved"), NotNull]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }

       

        public WatiContactsRow()
            : base(Fields)
        {
        }
        public WatiContactsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Waid;
            public StringField FirtName;
            public StringField FullName;
            public StringField Phone;
            public StringField Source;
            public StringField Status;
            public DateTimeField Created;
            public BooleanField IsMoved;
        }
    }
}
