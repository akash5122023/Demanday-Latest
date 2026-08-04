using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Masters
{
    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[DemandayCampaignId]")]
    [DisplayName("CampaignId"), InstanceName("CampaignId")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.DemandayCampaignId", Permission = "?")]
    // A Campaign ID is only meaningful inside its Master Account — two accounts may legitimately
    // run a campaign with the same number, but one account must never hold it twice, or every
    // campaign-wise sheet (and the lookups above them) would show two identical entries.
    // Backed by UX_DemandayCampaignId_Account_CampaignId so an import cannot slip past this either.
    [UniqueConstraint(nameof(DemandayMasterAccountId), nameof(CampaignId),
        ErrorMessage = "This Campaign ID already exists under the selected Master Account!")]
    public sealed class DemandayCampaignIdRow : Row<DemandayCampaignIdRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Campaign Id"), Size(15), QuickSearch, NameProperty]
        public String CampaignId
        {
            get => fields.CampaignId[this];
            set => fields.CampaignId[this] = value;
        }

        [DisplayName("Demanday Master Account"), ForeignKey("[dbo].[DemandayMasterAccount]", "Id"), LeftJoin("jDemandayMasterAccount"), TextualField("DemandayMasterAccountAccountNumber")]
        [LookupInclude]
        public Int32? DemandayMasterAccountId
        {
            get => fields.DemandayMasterAccountId[this];
            set => fields.DemandayMasterAccountId[this] = value;
        }

        [DisplayName("Demanday Account Number"), Expression("jDemandayMasterAccount.[AccountNumber]")]
        public String DemandayMasterAccountAccountNumber
        {
            get => fields.DemandayMasterAccountAccountNumber[this];
            set => fields.DemandayMasterAccountAccountNumber[this] = value;
        }

        public DemandayCampaignIdRow()
            : base()
        {
        }

        public DemandayCampaignIdRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField CampaignId;
            public Int32Field DemandayMasterAccountId;

            public StringField DemandayMasterAccountAccountNumber;
        }
    }
}
