using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.DNCContact
{
    [ConnectionKey("Default"), Module("DNCContact"), TableName("[dbo].[DNCContacts]")]
    [DisplayName("DNC Contacts"), InstanceName("Dnc Contacts")]
    [ReadPermission("DNCContacts:Read")]
    [InsertPermission("DNCContacts:Insert")]
    [UpdatePermission("DNCContacts:Update")]
    [DeletePermission("DNCContacts:Delete")]
    [LookupScript("DNCContact.DNCContacts", Permission = "?")]
    public sealed class DncContactsRow : Row<DncContactsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
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

        [DisplayName("Dnc Status"), Column("DNCStatus"), Size(50), QuickSearch]
        public String DncStatus
        {
            get => fields.DncStatus[this];
            set => fields.DncStatus[this] = value;
        }

        [DisplayName("Number"), Size(50), QuickSearch]
        public String Number
        {
            get => fields.Number[this];
            set => fields.Number[this] = value;
        }

        [DisplayName("Campaign"), ForeignKey("[dbo].[DemandayCampaignId]", "Id"), LeftJoin("jCampaign"), TextualField("CampaignCampaignId")]
        public Int32? CampaignId
        {
            get => fields.CampaignId[this];
            set => fields.CampaignId[this] = value;
        }

        [DisplayName("Master Account"), ForeignKey("[dbo].[DemandayMasterAccount]", "Id"), LeftJoin("jMasterAccount"), TextualField("MasterAccountAccountNumber")]
        public Int32? MasterAccountId
        {
            get => fields.MasterAccountId[this];
            set => fields.MasterAccountId[this] = value;
        }

        [DisplayName("Campaign Id"), Expression("jCampaign.[CampaignId]"), QuickSearch]
        public String CampaignCampaignId
        {
            get => fields.CampaignCampaignId[this];
            set => fields.CampaignCampaignId[this] = value;
        }

        [DisplayName("Campaign Demanday Master Account Id"), Expression("jCampaign.[DemandayMasterAccountId]")]
        public Int32? CampaignDemandayMasterAccountId
        {
            get => fields.CampaignDemandayMasterAccountId[this];
            set => fields.CampaignDemandayMasterAccountId[this] = value;
        }

        [DisplayName("Master Account Number"), Expression("jMasterAccount.[AccountNumber]"), QuickSearch]
        public String MasterAccountAccountNumber
        {
            get => fields.MasterAccountAccountNumber[this];
            set => fields.MasterAccountAccountNumber[this] = value;
        }

        public DncContactsRow()
            : base()
        {
        }

        public DncContactsRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField FirstName;
            public StringField LastName;
            public StringField Email;
            public StringField DncStatus;
            public StringField Number;
            public Int32Field CampaignId;
            public Int32Field MasterAccountId;

            public StringField CampaignCampaignId;
            public Int32Field CampaignDemandayMasterAccountId;

            public StringField MasterAccountAccountNumber;
        }
    }
}
