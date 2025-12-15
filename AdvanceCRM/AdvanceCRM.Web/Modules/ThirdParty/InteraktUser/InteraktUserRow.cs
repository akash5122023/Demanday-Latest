
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[InteraktUser]")]
    [DisplayName("Interakt User"), InstanceName("Interakt User")]
    [ReadPermission("Interakt:Inbox")]
    [ModifyPermission("Interakt:Inbox")]
    public sealed class InteraktUserRow : Row<InteraktUserRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Interakt Id"), Column("InteraktID"), Size(100), QuickSearch,NameProperty]
        public String InteraktId
        {
            get { return Fields.InteraktId[this]; }
            set { Fields.InteraktId[this] = value; }
        }

        [DisplayName("Created")]
        public DateTime? Created
        {
            get { return Fields.Created[this]; }
            set { Fields.Created[this] = value; }
        }

        [DisplayName("Modified")]
        public DateTime? Modified
        {
            get { return Fields.Modified[this]; }
            set { Fields.Modified[this] = value; }
        }

        [DisplayName("Phone"), Size(50)]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Country Code"), Size(50)]
        public String CountryCode
        {
            get { return Fields.CountryCode[this]; }
            set { Fields.CountryCode[this] = value; }
        }

        [DisplayName("User Id"), Size(100)]
        public String UserId
        {
            get { return Fields.UserId[this]; }
            set { Fields.UserId[this] = value; }
        }

        [DisplayName("Full Name"), Size(100)]
        public String FullName
        {
            get { return Fields.FullName[this]; }
            set { Fields.FullName[this] = value; }
        }

        [DisplayName("Email"), Size(100)]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("WhatsApp Opted In"), Column("WPOptedIn")]
        public Boolean? WpOptedIn
        {
            get { return Fields.WpOptedIn[this]; }
            set { Fields.WpOptedIn[this] = value; }
        }

        [DisplayName("Is Moved"), NotNull]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }

       

        public InteraktUserRow()
            : base(Fields)
        {
        }
        
        public InteraktUserRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField InteraktId;
            public DateTimeField Created;
            public DateTimeField Modified;
            public StringField Phone;
            public StringField CountryCode;
            public StringField UserId;
            public StringField FullName;
            public StringField Email;
            public BooleanField WpOptedIn;
            public BooleanField IsMoved;
        }
    }
}
