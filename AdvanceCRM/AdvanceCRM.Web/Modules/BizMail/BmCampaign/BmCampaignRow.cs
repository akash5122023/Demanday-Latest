
namespace AdvanceCRM.BizMail
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("BizMail"), TableName("[dbo].[BMCampaign]")]
    [DisplayName("Bm Campaign"), InstanceName("Bm Campaign")]
    [ReadPermission("BizMail:Read")]
    [InsertPermission("BizMail:Insert")]
    [UpdatePermission("BizMail:Update")]
    [DeletePermission("BizMail:Delete")]
    public sealed class BmCampaignRow : Row<BmCampaignRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Campaign Id"), Size(200), QuickSearch,NameProperty]
        public String CampaignId
        {
            get { return Fields.CampaignId[this]; }
            set { Fields.CampaignId[this] = value; }
        }

        [DisplayName("Name"), Size(200)]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Status"), Size(200)]
        public String Status
        {
            get { return Fields.Status[this]; }
            set { Fields.Status[this] = value; }
        }

       

        public BmCampaignRow()
            : base(Fields)
        {
        }
        

        public BmCampaignRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField CampaignId;
            public StringField Name;
            public StringField Status;
        }
    }
}
