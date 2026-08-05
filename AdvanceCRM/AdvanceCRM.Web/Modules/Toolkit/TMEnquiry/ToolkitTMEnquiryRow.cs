using AdvanceCRM.Masters;
using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace AdvanceCRM.Toolkit
{
    [ConnectionKey("Default"), Module("Toolkit"), TableName("[dbo].[ToolkitTMEnquiry]")]
    [DisplayName("Toolkit TM Enquiry"), InstanceName("Toolkit TM Enquiry")]
    [ReadPermission("ToolkitTMEnquiry:Read")]
    [InsertPermission("ToolkitTMEnquiry:Insert")]
    [UpdatePermission("ToolkitTMEnquiry:Update")]
    [DeletePermission("ToolkitTMEnquiry:Delete")]
    [LookupScript("Toolkit.ToolkitTMEnquiry", Permission = "ToolkitTMEnquiry:Read")]
    public sealed class ToolkitTMEnquiryRow : Row<ToolkitTMEnquiryRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Sr No"), Identity, IdProperty]
        public Int32? SrNo
        {
            get => fields.SrNo[this];
            set => fields.SrNo[this] = value;
        }

        [DisplayName("Master Account"), ForeignKey("[dbo].[DemandayMasterAccount]", "Id"), LeftJoin("jMasterAccount"), TextualField("MasterAccountAccountNumber")]
        [LookupEditor(typeof(DemandayMasterAccountRow), InplaceAdd = true)]
        public Int32? MasterAccountId
        {
            get => fields.MasterAccountId[this];
            set => fields.MasterAccountId[this] = value;
        }

        [DisplayName("Campaign"), ForeignKey("[dbo].[DemandayCampaignId]", "Id"), LeftJoin("jCampaign"), TextualField("CampaignCampaignId")]
        [LookupEditor(typeof(DemandayCampaignIdRow), InplaceAdd = true)]
        public Int32? CampaignId
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

        [DisplayName("TM Enquiry"), ForeignKey("[dbo].[DemandayTeleMarketingEnquiry]", "Id"), LeftJoin("jTMEnquiry"), TextualField("TMEnquiryFirstName")]
        [LookupEditor(typeof(Demanday.DemandayTeleMarketingEnquiryRow), InplaceAdd = true)]
        public Int32? DemandayTeleMarketingEnquiryId
        {
            get => fields.DemandayTeleMarketingEnquiryId[this];
            set => fields.DemandayTeleMarketingEnquiryId[this] = value;
        }

        [DisplayName("Master Account Number"), Expression("jMasterAccount.[AccountNumber]"), QuickSearch]
        public String MasterAccountAccountNumber
        {
            get => fields.MasterAccountAccountNumber[this];
            set => fields.MasterAccountAccountNumber[this] = value;
        }

        [DisplayName("Campaign Id"), Expression("jCampaign.[CampaignId]"), QuickSearch]
        public String CampaignCampaignId
        {
            get => fields.CampaignCampaignId[this];
            set => fields.CampaignCampaignId[this] = value;
        }

        // Lets the Campaign quick filter cascade from the selected Master Account.
        [DisplayName("Campaign Demanday Master Account Id"), Expression("jCampaign.[DemandayMasterAccountId]")]
        public Int32? CampaignDemandayMasterAccountId
        {
            get => fields.CampaignDemandayMasterAccountId[this];
            set => fields.CampaignDemandayMasterAccountId[this] = value;
        }

        [DisplayName("TM Enquiry First Name"), Expression("jTMEnquiry.[FirstName]"), QuickSearch]
        public String TMEnquiryFirstName
        {
            get => fields.TMEnquiryFirstName[this];
            set => fields.TMEnquiryFirstName[this] = value;
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

        public ToolkitTMEnquiryRow()
            : base()
        {
        }

        public ToolkitTMEnquiryRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field SrNo;
            public Int32Field MasterAccountId;
            public Int32Field CampaignId;
            public StringField FirstName;
            public StringField LastName;
            public StringField Email;
            public StringField CompanyName;
            public DateTimeField Timestamp;
            public Int32Field DemandayTeleMarketingEnquiryId;
            public StringField MasterAccountAccountNumber;
            public StringField CampaignCampaignId;
            public Int32Field CampaignDemandayMasterAccountId;
            public StringField TMEnquiryFirstName;
            public DateTimeField CreatedOn;
            public StringField CreatedBy;
            public DateTimeField UpdatedOn;
            public StringField UpdatedBy;
        }
    }
}
