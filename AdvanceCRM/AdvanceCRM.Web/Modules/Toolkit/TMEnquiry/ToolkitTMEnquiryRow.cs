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

        // The row is created when a TM Enquiry is moved to Team Leader; that move deletes the
        // enquiry, so the link points at the Team Leader record it became.
        [DisplayName("Team Leader"), ForeignKey("[dbo].[DemandayTeleMarketingTeamLeader]", "Id"), LeftJoin("jTeamLeader"), TextualField("TeamLeaderFirstName")]
        [LookupEditor(typeof(Demanday.DemandayTeleMarketingTeamLeaderRow))]
        public Int32? TeamLeaderId
        {
            get => fields.TeamLeaderId[this];
            set => fields.TeamLeaderId[this] = value;
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

        [DisplayName("Team Leader First Name"), Expression("jTeamLeader.[FirstName]"), QuickSearch]
        public String TeamLeaderFirstName
        {
            get => fields.TeamLeaderFirstName[this];
            set => fields.TeamLeaderFirstName[this] = value;
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
            public Int32Field TeamLeaderId;
            public StringField MasterAccountAccountNumber;
            public StringField CampaignCampaignId;
            public Int32Field CampaignDemandayMasterAccountId;
            public StringField TeamLeaderFirstName;
            public DateTimeField CreatedOn;
            public StringField CreatedBy;
            public DateTimeField UpdatedOn;
            public StringField UpdatedBy;
        }
    }
}
