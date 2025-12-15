
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[SulekhaDetails]")]
    [DisplayName("Sulekha Details"), InstanceName("Sulekha Details")]
    [ReadPermission("SulekhaDetails:Inbox")]
    [ModifyPermission("SulekhaDetails:Inbox")]
    public sealed class SulekhaDetailsRow : Row<SulekhaDetailsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("User Name"), Size(255), QuickSearch,NameProperty]
        public String UserName
        {
            get { return Fields.UserName[this]; }
            set { Fields.UserName[this] = value; }
        }

        [DisplayName("Mobile"), Size(50)]
        public String Mobile
        {
            get { return Fields.Mobile[this]; }
            set { Fields.Mobile[this] = value; }
        }

        [DisplayName("Email"), Size(50)]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("City"), Size(255)]
        public String City
        {
            get { return Fields.City[this]; }
            set { Fields.City[this] = value; }
        }

        [DisplayName("Localities")]
        public String Localities
        {
            get { return Fields.Localities[this]; }
            set { Fields.Localities[this] = value; }
        }

        [DisplayName("Date Time")]
        public DateTime? DateTime
        {
            get { return Fields.DateTime[this]; }
            set { Fields.DateTime[this] = value; }
        }

        [DisplayName("Comments")]
        public String Comments
        {
            get { return Fields.Comments[this]; }
            set { Fields.Comments[this] = value; }
        }

        [DisplayName("Keywords"), Size(255)]
        public String Keywords
        {
            get { return Fields.Keywords[this]; }
            set { Fields.Keywords[this] = value; }
        }

        [DisplayName("Feedback"),TextAreaEditor(Rows =3)]
        public String Feedback
        {
            get { return Fields.Feedback[this]; }
            set { Fields.Feedback[this] = value; }
        }

        [DisplayName("Is Moved"), NotNull]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }

      

        public SulekhaDetailsRow()
            : base(Fields)
        {
        }
        
        public SulekhaDetailsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField UserName;
            public StringField Mobile;
            public StringField Email;
            public StringField City;
            public StringField Localities;
            public StringField Comments;
            public StringField Keywords;
            public StringField Feedback;
            public BooleanField IsMoved;
            public DateTimeField DateTime;
        }
    }
}
