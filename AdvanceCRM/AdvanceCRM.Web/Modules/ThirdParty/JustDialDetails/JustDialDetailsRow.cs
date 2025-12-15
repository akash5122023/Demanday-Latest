
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[JustDialDetails]")]
    [DisplayName("Just Dial Details"), InstanceName("Just Dial Details")]
    [ReadPermission("JustDial:Inbox")]
    [ModifyPermission("JustDial:Inbox")]
    public sealed class JustDialDetailsRow : Row<JustDialDetailsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), PrimaryKey,IdProperty]
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

        [DisplayName("Lead Id"), NotNull]
        public String LeadId
        {
            get { return Fields.LeadId[this]; }
            set { Fields.LeadId[this] = value; }
        }

        [DisplayName("Lead Type"), Size(255), QuickSearch]
        public String LeadType
        {
            get { return Fields.LeadType[this]; }
            set { Fields.LeadType[this] = value; }
        }

        [DisplayName("Prefix"), Size(255), NotNull]
        public String Prefix
        {
            get { return Fields.Prefix[this]; }
            set { Fields.Prefix[this] = value; }
        }

        [DisplayName("Name"), Size(255), QuickSearch,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Mobile"), Size(50), QuickSearch, LookupInclude]
        public String Mobile
        {
            get { return Fields.Mobile[this]; }
            set { Fields.Mobile[this] = value; }
        }

        [DisplayName("Landline"), Size(50), LookupInclude]
        public String Landline
        {
            get { return Fields.Landline[this]; }
            set { Fields.Landline[this] = value; }
        }

        [DisplayName("Email"), Size(50),LookupInclude]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("Date Time"), NotNull]
        public DateTime? DateTime
        {
            get { return Fields.DateTime[this]; }
            set { Fields.DateTime[this] = value; }
        }

        [DisplayName("Category"), Size(255)]
        public String Category
        {
            get { return Fields.Category[this]; }
            set { Fields.Category[this] = value; }
        }

        [DisplayName("City"), Size(255), QuickSearch]
        public String City
        {
            get { return Fields.City[this]; }
            set { Fields.City[this] = value; }
        }

        [DisplayName("Area"), Size(255), QuickSearch]
        public String Area
        {
            get { return Fields.Area[this]; }
            set { Fields.Area[this] = value; }
        }

        [DisplayName("Branch Area"), Size(255)]
        public String BranchArea
        {
            get { return Fields.BranchArea[this]; }
            set { Fields.BranchArea[this] = value; }
        }

        [DisplayName("DCN Mobile"), Column("DCNMobile"), NotNull, LookupInclude]
        public Boolean? DcnMobile
        {
            get { return Fields.DcnMobile[this]; }
            set { Fields.DcnMobile[this] = value; }
        }

        [DisplayName("DCN Phone"), Column("DCNPhone"), NotNull, LookupInclude]
        public Boolean? DcnPhone
        {
            get { return Fields.DcnPhone[this]; }
            set { Fields.DcnPhone[this] = value; }
        }

        [DisplayName("Company"), Size(255)]
        public String Company
        {
            get { return Fields.Company[this]; }
            set { Fields.Company[this] = value; }
        }

        [DisplayName("Pin"), Size(50)]
        public String Pin
        {
            get { return Fields.Pin[this]; }
            set { Fields.Pin[this] = value; }
        }

        [DisplayName("Branh Pin"), Size(50)]
        public String BranhPin
        {
            get { return Fields.BranhPin[this]; }
            set { Fields.BranhPin[this] = value; }
        }

        [DisplayName("Parent Id"), Size(255)]
        public String ParentId
        {
            get { return Fields.ParentId[this]; }
            set { Fields.ParentId[this] = value; }
        }

        [DisplayName("Moved"), NotNull]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }

       

        public JustDialDetailsRow()
            : base(Fields)
        {
        }
        
        public JustDialDetailsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField LeadId;
            public StringField LeadType;
            public StringField Prefix;
            public StringField Name;
            public StringField Feedback;
            public StringField Mobile;
            public StringField Landline;
            public StringField Email;
            public DateTimeField DateTime;
            public StringField Category;
            public StringField City;
            public StringField Area;
            public StringField BranchArea;
            public BooleanField DcnMobile;
            public BooleanField DcnPhone;
            public StringField Company;
            public StringField Pin;
            public StringField BranhPin;
            public StringField ParentId;
            public BooleanField IsMoved;
        }
    }
}
