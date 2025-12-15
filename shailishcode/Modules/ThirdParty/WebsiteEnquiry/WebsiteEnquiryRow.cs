
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[WebsiteEnquiryDetails]")]
    [DisplayName("Website Enquiry"), InstanceName("Website Enquiry")]
    [ReadPermission("WebsiteEnquiry:Inbox")]
    [ModifyPermission("WebsiteEnquiry:Inbox")]
    public sealed class WebsiteEnquiryRow : Row<WebsiteEnquiryRow.RowFields>, IIdRow, INameRow
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

        //[DisplayName("City")]
        [DisplayName("Additional Info")]
        public String Address
        {
            get { return Fields.Address[this]; }
            set { Fields.Address[this] = value; }
        }

        [DisplayName("Date Time"), Size(100)]
        [DateTimeEditor]
        public DateTime? DateTime
        {
            get { return Fields.DateTime[this]; }
            set { Fields.DateTime[this] = value; }
        }

        //[DisplayName("Scheme")]
        [DisplayName("Company")]
        public String Requirement
        {
            get { return Fields.Requirement[this]; }
            set { Fields.Requirement[this] = value; }
        }

        [DisplayName("Is Moved"), NotNull]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }

       

        public WebsiteEnquiryRow()
            : base(Fields)
        {
        }
        

        public WebsiteEnquiryRow(RowFields fields)
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
            public DateTimeField DateTime;
            public StringField Requirement;
            public StringField Feedback;
            public BooleanField IsMoved;
        }
    }
}
