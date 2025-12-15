
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[FacebookDetails]")]
    [DisplayName("Facebook"), InstanceName("Facebook")]
    [ReadPermission("Facebook:Inbox")]
    [ModifyPermission("Facebook:Inbox")]
    public sealed class FacebookDetailsRow : Row<FacebookDetailsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Name"), Size(255), QuickSearch,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }
        [DisplayName("Feedback"), Size(500)]
        public String Feedback
        {
            get { return Fields.Feedback[this]; }
            set { Fields.Feedback[this] = value; }
        }

        [DisplayName("Phone"), Size(20)]
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

        [DisplayName("Compaign Name"), Size(50)]
        public String CompaignName
        {
            get { return Fields.CompaignName[this]; }
            set { Fields.CompaignName[this] = value; }
        }

        [DisplayName("Ad Set Name"), Size(50)]
        public String AdSetName
        {
            get { return Fields.AdSetName[this]; }
            set { Fields.AdSetName[this] = value; }
        }

        [DisplayName("Created Time"), NotNull]
        public DateTime? CreatedTime
        {
            get { return Fields.CreatedTime[this]; }
            set { Fields.CreatedTime[this] = value; }
        }

        [DisplayName("Is Moved"), NotNull]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }

        [DisplayName("Lead Id"), Size(100)]
        public String LeadId
        {
            get { return Fields.LeadId[this]; }
            set { Fields.LeadId[this] = value; }
        }

        [DisplayName("Campaignid"), Size(100)]
        public String Campaignid
        {
            get { return Fields.Campaignid[this]; }
            set { Fields.Campaignid[this] = value; }
        }
        [DisplayName("Company"), Size(100)]
        public String Company
        {
            get { return Fields.Company[this]; }
            set { Fields.Company[this] = value; }
        }

        [DisplayName("Ad Id"), Size(100)]
        public String AdId
        {
            get { return Fields.AdId[this]; }
            set { Fields.AdId[this] = value; }
        }

        [DisplayName("Ad Name"), Size(100)]
        public String AdName
        {
            get { return Fields.AdName[this]; }
            set { Fields.AdName[this] = value; }
        }

        [DisplayName("Ad Set Id"), Size(100)]
        public String AdSetId
        {
            get { return Fields.AdSetId[this]; }
            set { Fields.AdSetId[this] = value; }
        }
        [DisplayName("Address"), Size(255)]
        public String Address
        {
            get { return Fields.Address[this]; }
            set { Fields.Address[this] = value; }
        }
        [DisplayName("Additional Details"), Size(500)]
        public String AdditionalDetails
        {
            get { return Fields.AdditionalDetails[this]; }
            set { Fields.AdditionalDetails[this] = value; }
        }
       
        public FacebookDetailsRow()
            : base(Fields)
        {
        }
        
        public FacebookDetailsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public StringField Phone; public StringField Address;
            public StringField Email;
            public StringField CompaignName;
            public StringField AdSetName;
            public DateTimeField CreatedTime;
            public BooleanField IsMoved;
            public StringField LeadId;
            public StringField Campaignid;
            public StringField Company;
            public StringField AdId;
            public StringField AdName;
            public StringField AdSetId;
            public StringField AdditionalDetails;
            public StringField Feedback;
        }
    }
}
