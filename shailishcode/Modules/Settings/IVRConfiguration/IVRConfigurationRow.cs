
namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[IVRConfiguration]")]
    [DisplayName("IVR Configuration"), InstanceName("IVR Configuration")]
    [ReadPermission("Settings:IVR")]
    [ModifyPermission("Settings:IVR")]
    [LookupScript("Settings.IVR", Permission = "?")]

    public sealed class IVRConfigurationRow : Row<IVRConfigurationRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("IVR Number"), Column("IVRNumber"), Size(500), QuickSearch, LookupInclude,NameProperty]
        public String IVRNumber
        {
            get { return Fields.IVRNumber[this]; }
            set { Fields.IVRNumber[this] = value; }
        }

        [DisplayName("Api Key/ Key"), Column("APIKey"), Size(150)]
        public String ApiKey
        {
            get { return Fields.ApiKey[this]; }
            set { Fields.ApiKey[this] = value; }
        }

        [DisplayName("Plan"), Size(20), Placeholder("Basic or Advance or Premium or Enterprise")]
        public String Plan
        {
            get { return Fields.Plan[this]; }
            set { Fields.Plan[this] = value; }
        }

        [DisplayName("IVR Type"), Column("IVRType"), NotNull, DefaultValue(1)]
        public Masters.IVRTypeMaster? IVRType
        {
            get { return (Masters.IVRTypeMaster?)Fields.IVRType[this]; }
            set { Fields.IVRType[this] = (Int32?)value; }
        }

        [DisplayName("App Id/RFID"), Column("AppID"), Size(1024), NotNull]
        public String AppId
        {
            get { return Fields.AppId[this]; }
            set { Fields.AppId[this] = value; }
        }

        [DisplayName("App Secret"), Size(1024), NotNull]
        public String AppSecret
        {
            get { return Fields.AppSecret[this]; }
            set { Fields.AppSecret[this] = value; }
        }

        [KnowlarityAgentsEditor, NotMapped]
        [DisplayName("Agents"), MasterDetailRelation(foreignKey: "KnowlarityId", IncludeColumns = "Name")]
        public List<KnowlarityAgentsRow> Agents
        {
            get { return Fields.Agents[this]; }
            set { Fields.Agents[this] = value; }
        }

        [DisplayName("Auto Email")]
        public Boolean? AutoEmail
        {
            get { return Fields.AutoEmail[this]; }
            set { Fields.AutoEmail[this] = value; }
        }

        [DisplayName("Auto Sms"), Column("AutoSMS")]
        public Boolean? AutoSms
        {
            get { return Fields.AutoSms[this]; }
            set { Fields.AutoSms[this] = value; }
        }

        [DisplayName("Sender"), Size(250)]
        public String Sender
        {
            get { return Fields.Sender[this]; }
            set { Fields.Sender[this] = value; }
        }

        [DisplayName("Subject"), Size(250)]
        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }

        [DisplayName("Email Template"), Size(2000)]
        public String EmailTemplate
        {
            get { return Fields.EmailTemplate[this]; }
            set { Fields.EmailTemplate[this] = value; }
        }

        [DisplayName("Attachment"), Size(1000)]
        public String Attachment
        {
            get { return Fields.Attachment[this]; }
            set { Fields.Attachment[this] = value; }
        }

        [DisplayName("Sms Template"), Column("SMSTemplate"), Size(1000)]
        public String SmsTemplate
        {
            get { return Fields.SmsTemplate[this]; }
            set { Fields.SmsTemplate[this] = value; }
        }

        [DisplayName("Template Id"), Column("TemplateID"), Size(20)]
        public String TemplateId
        {
            get { return Fields.TemplateId[this]; }
            set { Fields.TemplateId[this] = value; }
        }

        [DisplayName("Host"), Size(200)]
        public String Host
        {
            get { return Fields.Host[this]; }
            set { Fields.Host[this] = value; }
        }

        [DisplayName("Port")]
        public Int32? Port
        {
            get { return Fields.Port[this]; }
            set { Fields.Port[this] = value; }
        }

        [DisplayName("Ssl"), Column("SSL")]
        public Boolean? Ssl
        {
            get { return Fields.Ssl[this]; }
            set { Fields.Ssl[this] = value; }
        }

        [DisplayName("Email Id"), Size(200)]
        public String EmailId
        {
            get { return Fields.EmailId[this]; }
            set { Fields.EmailId[this] = value; }
        }

        [DisplayName("Email Password"), Size(200)]
        public String EmailPassword
        {
            get { return Fields.EmailPassword[this]; }
            set { Fields.EmailPassword[this] = value; }
        }

        [DisplayName("Username"), Size(100)]
        public String Username
        {
            get { return Fields.Username[this]; }
            set { Fields.Username[this] = value; }
        }

        [DisplayName("Password"), Size(100)]
        public String Password
        {
            get { return Fields.Password[this]; }
            set { Fields.Password[this] = value; }
        }

        [DisplayName("Post Url"), Column("PostURL")]
        public String PostUrl
        {
            get { return Fields.PostUrl[this]; }
            set { Fields.PostUrl[this] = value; }
        }

        [DisplayName("WhatsApp Template"), Column("WATemplate"), TextAreaEditor(Rows = 4)]
        public String WaTemplate
        {
            get { return Fields.WaTemplate[this]; }
            set { Fields.WaTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Template Id"), Column("WATemplateId"), Size(100)]
        public String WaTemplateId
        {
            get { return Fields.WaTemplateId[this]; }
            set { Fields.WaTemplateId[this] = value; }
        }

        [DisplayName("CLI Number"), Column("CLINumber"), Size(100)]
        public String CliNumber
        {
            get { return Fields.CliNumber[this]; }
            set { Fields.CliNumber[this] = value; }
        }
        [DisplayName("Token Id"), Column("Token_Id")]
        public String Token_Id
        {
            get { return Fields.Token_Id[this]; }
            set { Fields.Token_Id[this] = value; }
        }
        [DisplayName("User Type"), Column("userType")]
        public String userType
        {
            get { return Fields.userType[this]; }
            set { Fields.userType[this] = value; }
        }
        [DisplayName("Number"), Column("number")]
        public String Number
        {
            get { return Fields.Number[this]; }
            set { Fields.Number[this] = value; }
        }
        [DisplayName("Round Robin"), Column("roundRobin")]
        public Boolean? RoundRobin
        {
            get { return Fields.RoundRobin[this]; }
            set { Fields.RoundRobin[this] = value; }
        }
        [DisplayName("Auto Refresh"), Column("autoRefresh")]
        public Boolean? AutoRefresh
        {
            get { return Fields.AutoRefresh[this]; }
            set { Fields.AutoRefresh[this] = value; }
        }
        [DisplayName("Auto Refresh Time"), Column("autoRefreshTime")]
        public Int32? AutoRefreshTime
        {
            get { return Fields.AutoRefreshTime[this]; }
            set { Fields.AutoRefreshTime[this] = value; }
        }

        
        public IVRConfigurationRow()
            : base(Fields)
        {
        }
        
        public IVRConfigurationRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField IVRNumber;
            public StringField ApiKey;
            public StringField Plan;
            public Int32Field IVRType;
            public StringField AppId;
            public StringField AppSecret;
            public BooleanField AutoEmail;
            public BooleanField AutoSms;
            public StringField Sender;
            public StringField Subject;
            public StringField EmailTemplate;
            public StringField Attachment;
            public StringField SmsTemplate;
            public StringField TemplateId;
            public StringField Host;
            public Int32Field Port;
            public BooleanField Ssl;
            public StringField EmailId;
            public StringField EmailPassword;
            public StringField CliNumber;

            public StringField Username;
            public StringField Password;
            public StringField PostUrl;

            public StringField WaTemplate;
            public StringField WaTemplateId;
            public StringField Token_Id;
            public StringField userType;
            public StringField Number;

            public BooleanField RoundRobin;
            public BooleanField AutoRefresh;
            public Int32Field AutoRefreshTime;

            public readonly RowListField<KnowlarityAgentsRow> Agents;
        }
    }
}
