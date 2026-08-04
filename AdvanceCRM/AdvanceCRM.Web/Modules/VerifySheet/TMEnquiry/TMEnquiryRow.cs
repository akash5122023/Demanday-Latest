using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace AdvanceCRM.VerifySheet
{
    [ConnectionKey("Default"), Module("VerifySheet"), TableName("[dbo].[TMEnquiry]")]
    [DisplayName("TM Enquiry"), InstanceName("TM Enquiry")]
    [ReadPermission("TMEnquiry:Read")]
    [InsertPermission("TMEnquiry:Insert")]
    [UpdatePermission("TMEnquiry:Update")]
    [DeletePermission("TMEnquiry:Delete")]
    [LookupScript("VerifySheet.TMEnquiry", Permission = "TMEnquiry:Insert")]
    public sealed class TMEnquiryRow : Row<TMEnquiryRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Master Account"), ForeignKey("[dbo].[DemandayMasterAccount]", "Id"), LookupInclude]
        public Int32? MasterAccountId
        {
            get => fields.MasterAccountId[this];
            set => fields.MasterAccountId[this] = value;
        }

        [DisplayName("Campaign Id"), Size(50), QuickSearch]
        public String CampaignId
        {
            get => fields.CampaignId[this];
            set => fields.CampaignId[this] = value;
        }

        [DisplayName("First Name"), Size(100), QuickSearch, NameProperty]
        public String FirstName
        {
            get => fields.FirstName[this];
            set => fields.FirstName[this] = value;
        }

        [DisplayName("Last Name"), Size(100), QuickSearch]
        public String LastName
        {
            get => fields.LastName[this];
            set => fields.LastName[this] = value;
        }

        [DisplayName("Email"), Size(200), QuickSearch]
        public String Email
        {
            get => fields.Email[this];
            set => fields.Email[this] = value;
        }

        [DisplayName("Company Name"), Size(200), QuickSearch]
        public String CompanyName
        {
            get => fields.CompanyName[this];
            set => fields.CompanyName[this] = value;
        }

        [DisplayName("Timestamp")]
        public DateTime? Timestamp
        {
            get => fields.Timestamp[this];
            set => fields.Timestamp[this] = value;
        }

        [DisplayName("TM Enquiry Id"), ForeignKey("[dbo].[DemandayTeleMarketingEnquiry]", "Id")]
        public Int32? DemandayTeleMarketingEnquiryId
        {
            get => fields.DemandayTeleMarketingEnquiryId[this];
            set => fields.DemandayTeleMarketingEnquiryId[this] = value;
        }

        [DisplayName("Created On")]
        public DateTime? CreatedOn
        {
            get => fields.CreatedOn[this];
            set => fields.CreatedOn[this] = value;
        }

        [DisplayName("Created By"), Size(int.MaxValue)]
        public String CreatedBy
        {
            get => fields.CreatedBy[this];
            set => fields.CreatedBy[this] = value;
        }

        [DisplayName("Updated On")]
        public DateTime? UpdatedOn
        {
            get => fields.UpdatedOn[this];
            set => fields.UpdatedOn[this] = value;
        }

        [DisplayName("Updated By"), Size(int.MaxValue)]
        public String UpdatedBy
        {
            get => fields.UpdatedBy[this];
            set => fields.UpdatedBy[this] = value;
        }

        public TMEnquiryRow()
            : base()
        {
        }

        public TMEnquiryRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field MasterAccountId;
            public StringField CampaignId;
            public StringField FirstName;
            public StringField LastName;
            public StringField Email;
            public StringField CompanyName;
            public DateTimeField Timestamp;
            public Int32Field DemandayTeleMarketingEnquiryId;
            public DateTimeField CreatedOn;
            public StringField CreatedBy;
            public DateTimeField UpdatedOn;
            public StringField UpdatedBy;
        }
    }
}
