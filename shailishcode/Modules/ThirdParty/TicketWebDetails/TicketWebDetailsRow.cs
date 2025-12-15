
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[TicketWebDetails]")]
    [DisplayName("Ticket Web Details"), InstanceName("Ticket Web Details")]
    [ReadPermission("WebsiteEnquiry:Inbox")]
    [ModifyPermission("WebsiteEnquiry:Inbox")]
    public sealed class TicketWebDetailsRow : Row<TicketWebDetailsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Name"), Size(255), NotNull, QuickSearch,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Phone"), Size(255)]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Email"), Size(255)]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("Address"), TextAreaEditor(Rows = 3)]
        public String Address
        {
            get { return Fields.Address[this]; }
            set { Fields.Address[this] = value; }
        }

        [DisplayName("Product Name"), Size(500)]
        public String ProductName
        {
            get { return Fields.ProductName[this]; }
            set { Fields.ProductName[this] = value; }
        }

        [DisplayName("Requirement")]
        public String Requirement
        {
            get { return Fields.Requirement[this]; }
            set { Fields.Requirement[this] = value; }
        }

        [DisplayName("Date Time"), NotNull]
        public DateTime? DateTime
        {
            get { return Fields.DateTime[this]; }
            set { Fields.DateTime[this] = value; }
        }

        [DisplayName("Purchase Date"), NotNull]
        public DateTime? PurchaseDate
        {
            get { return Fields.PurchaseDate[this]; }
            set { Fields.PurchaseDate[this] = value; }
        }

        [DisplayName("Complaint Details"),TextAreaEditor(Rows =3)]
        public String ComplaintDetails
        {
            get { return Fields.ComplaintDetails[this]; }
            set { Fields.ComplaintDetails[this] = value; }
        }

        [DisplayName("Attachment")]
        [MultipleImageUploadEditor(FilenameFormat = "Ticket/~", CopyToHistory = true, AllowNonImage = true)]
        public String Attachment
        {
            get { return Fields.Attachment[this]; }
            set { Fields.Attachment[this] = value; }
        }

        [DisplayName("Is Moved"), NotNull]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }

       

        public TicketWebDetailsRow()
            : base(Fields)
        {
        }
        public TicketWebDetailsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public StringField Phone;
            public StringField Email;
            public StringField Address;
            public StringField ProductName;
            public StringField Requirement;
            public DateTimeField DateTime;
            public DateTimeField PurchaseDate;
            public StringField ComplaintDetails;
            public StringField Attachment;
            public BooleanField IsMoved;
        }
    }
}
