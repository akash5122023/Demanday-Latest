using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace AdvanceCRM.TeleMarketingEmailTeam
{
    [ConnectionKey("Default"), Module("TeleMarketingEmailTeam"), TableName("[dbo].[TeleMarketingEmailTeam]")]
    [DisplayName("TM Email Team"), InstanceName("TM Email Team")]
    [ReadPermission("TeleMarketingEmailTeam:Read")]
    [InsertPermission("TeleMarketingEmailTeam:Insert")]
    [UpdatePermission("TeleMarketingEmailTeam:Update")]
    [DeletePermission("TeleMarketingEmailTeam:Delete")]
    public sealed class TeleMarketingEmailTeamRow : Row<TeleMarketingEmailTeamRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Master Account"), ForeignKey("[dbo].[DemandayMasterAccount]", "Id"), LeftJoin("jMasterAccount"), TextualField("MasterAccountNumber")]
        public Int32? MasterAccountId
        {
            get => fields.MasterAccountId[this];
            set => fields.MasterAccountId[this] = value;
        }

        [DisplayName("Campaign"), ForeignKey("[dbo].[DemandayCampaignId]", "Id"), LeftJoin("jCampaign"), TextualField("CampaignCode")]
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

        // Changing this mirrors the new value onto DemandayTeleMarketingQualilty.EmailTeamStatus
        // – see TeleMarketingEmailTeamSaveHandler.
        [DisplayName("Status")]
        public TeleMarketingEmailTeamStatus? Status
        {
            get => (TeleMarketingEmailTeamStatus?)fields.Status[this];
            set => fields.Status[this] = (Int32?)value;
        }

        // "Created By" – carried over from TM Team Leader on move, otherwise auto-stamped
        // with the creating user (see TeleMarketingEmailTeamSaveHandler).
        [DisplayName("Created By"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jOwner"), TextualField("OwnerUsername"), ReadOnly(true)]
        [Administration.UserEditor]
        public Int32? OwnerId
        {
            get => fields.OwnerId[this];
            set => fields.OwnerId[this] = value;
        }

        // The TM Quality row this record was created from (Move-to-Quality stamps it).
        // It is the only link used to reflect Status back into TM Quality.
        [DisplayName("TM Quality Id"), ForeignKey("[dbo].[DemandayTeleMarketingQualilty]", "Id"), LeftJoin("jQuality"), ReadOnly(true)]
        public Int32? DemandayTeleMarketingQualiltyId
        {
            get => fields.DemandayTeleMarketingQualiltyId[this];
            set => fields.DemandayTeleMarketingQualiltyId[this] = value;
        }

        [DisplayName("Owner Username"), Expression("jOwner.[Username]")]
        public String OwnerUsername
        {
            get => fields.OwnerUsername[this];
            set => fields.OwnerUsername[this] = value;
        }

        [DisplayName("Owner Display Name"), Expression("jOwner.[DisplayName]")]
        public String OwnerDisplayName
        {
            get => fields.OwnerDisplayName[this];
            set => fields.OwnerDisplayName[this] = value;
        }

        [DisplayName("Master Account Number"), Expression("jMasterAccount.[AccountNumber]")]
        public String MasterAccountNumber
        {
            get => fields.MasterAccountNumber[this];
            set => fields.MasterAccountNumber[this] = value;
        }

        [DisplayName("Campaign Id"), Expression("jCampaign.[CampaignId]")]
        public String CampaignCode
        {
            get => fields.CampaignCode[this];
            set => fields.CampaignCode[this] = value;
        }

        public TeleMarketingEmailTeamRow()
            : base()
        {
        }

        public TeleMarketingEmailTeamRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field MasterAccountId;
            public Int32Field CampaignId;
            public StringField FirstName;
            public StringField LastName;
            public StringField Email;
            public Int32Field Status;
            public Int32Field OwnerId;
            public Int32Field DemandayTeleMarketingQualiltyId;

            public StringField MasterAccountNumber;
            public StringField CampaignCode;
            public StringField OwnerUsername;
            public StringField OwnerDisplayName;
        }
    }
}
