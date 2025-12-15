
namespace AdvanceCRM.BizMail
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("BizMail"), TableName("[dbo].[BMSubscribers]")]
    [DisplayName("Bm Subscribers"), InstanceName("Bm Subscribers")]
    [ReadPermission("BizMail:Read")]
    [InsertPermission("BizMail:Insert")]
    [UpdatePermission("BizMail:Update")]
    [DeletePermission("BizMail:Delete")]
    public sealed class BmSubscribersRow : Row<BmSubscribersRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Subscriber Id"), Size(200), QuickSearch,NameProperty]
        public String SubscriberId
        {
            get { return Fields.SubscriberId[this]; }
            set { Fields.SubscriberId[this] = value; }
        }

        [DisplayName("Email"), Size(200)]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("First Name"), Size(200)]
        public String FirstName
        {
            get { return Fields.FirstName[this]; }
            set { Fields.FirstName[this] = value; }
        }

        [DisplayName("Last Name"), Size(200)]
        public String LastName
        {
            get { return Fields.LastName[this]; }
            set { Fields.LastName[this] = value; }
        }

        [DisplayName("Status"), Size(200)]
        public String Status
        {
            get { return Fields.Status[this]; }
            set { Fields.Status[this] = value; }
        }

        [DisplayName("Source"), Size(200)]
        public String Source
        {
            get { return Fields.Source[this]; }
            set { Fields.Source[this] = value; }
        }

        [DisplayName("Ip Address"), Column("IPAddress"), Size(200)]
        public String IpAddress
        {
            get { return Fields.IpAddress[this]; }
            set { Fields.IpAddress[this] = value; }
        }

        [DisplayName("Date Added"), Size(200)]
        public String DateAdded
        {
            get { return Fields.DateAdded[this]; }
            set { Fields.DateAdded[this] = value; }
        }

        [DisplayName("List Name"), Size(200)]
        public String ListName
        {
            get { return Fields.ListName[this]; }
            set { Fields.ListName[this] = value; }
        }

        [DisplayName("List Id"), Column("ListID"), Size(20)]
        public String ListId
        {
            get { return Fields.ListId[this]; }
            set { Fields.ListId[this] = value; }
        }

        [DisplayName("Phone"), Size(20)]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Is Moved"), NotNull]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }

       
        public BmSubscribersRow()
            : base(Fields)
        {
        }
        public BmSubscribersRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField SubscriberId;
            public StringField Email;
            public StringField FirstName;
            public StringField LastName;
            public StringField Status;
            public StringField Source;
            public StringField IpAddress;
            public StringField DateAdded;
            public StringField ListName;
            public StringField ListId;
            public StringField Phone;
            public BooleanField IsMoved;
        }
    }
}
