
namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[KnowlarityAgents]")]
    [DisplayName("Agents"), InstanceName("Agents")]
    [ReadPermission("Settings:IVR")]
    [ModifyPermission("Settings:IVR")]
    [LookupScript("Settings.KnowlarityAgents", Permission = "?")]
    public sealed class KnowlarityAgentsRow : Row<KnowlarityAgentsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Knowlarity"), NotNull, ForeignKey("[dbo].[IVRConfiguration]", "Id"), LeftJoin("jKnowlarity"), TextualField("KnowlarityIvrNumber")]
        public Int32? KnowlarityId
        {
            get { return Fields.KnowlarityId[this]; }
            set { Fields.KnowlarityId[this] = value; }
        }

        [DisplayName("Name"), Size(2000), NotNull, QuickSearch,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Number"), Size(2000), NotNull, Unique]
        public String Number
        {
            get { return Fields.Number[this]; }
            set { Fields.Number[this] = value; }
        }

        [DisplayName("Knowlarity Ivr Number"), Expression("jKnowlarity.[IVRNumber]")]
        public String KnowlarityIvrNumber
        {
            get { return Fields.KnowlarityIvrNumber[this]; }
            set { Fields.KnowlarityIvrNumber[this] = value; }
        }

        [DisplayName("Knowlarity Api Key"), Expression("jKnowlarity.[APIKey]")]
        public String KnowlarityApiKey
        {
            get { return Fields.KnowlarityApiKey[this]; }
            set { Fields.KnowlarityApiKey[this] = value; }
        }

        [DisplayName("Knowlarity Plan"), Expression("jKnowlarity.[Plan]")]
        public String KnowlarityPlan
        {
            get { return Fields.KnowlarityPlan[this]; }
            set { Fields.KnowlarityPlan[this] = value; }
        }

        [DisplayName("Knowlarity Ivr Type"), Expression("jKnowlarity.[IVRType]")]
        public Int32? KnowlarityIvrType
        {
            get { return Fields.KnowlarityIvrType[this]; }
            set { Fields.KnowlarityIvrType[this] = value; }
        }

        [DisplayName("Knowlarity App Id"), Expression("jKnowlarity.[AppID]")]
        public String KnowlarityAppId
        {
            get { return Fields.KnowlarityAppId[this]; }
            set { Fields.KnowlarityAppId[this] = value; }
        }

        [DisplayName("Knowlarity App Secret"), Expression("jKnowlarity.[AppSecret]")]
        public String KnowlarityAppSecret
        {
            get { return Fields.KnowlarityAppSecret[this]; }
            set { Fields.KnowlarityAppSecret[this] = value; }
        }

        public KnowlarityAgentsRow()
            : base(Fields)
        {
        }
        public KnowlarityAgentsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field KnowlarityId;
            public StringField Name;
            public StringField Number;

            public StringField KnowlarityIvrNumber;
            public StringField KnowlarityApiKey;
            public StringField KnowlarityPlan;
            public Int32Field KnowlarityIvrType;
            public StringField KnowlarityAppId;
            public StringField KnowlarityAppSecret;
        }
    }
}
