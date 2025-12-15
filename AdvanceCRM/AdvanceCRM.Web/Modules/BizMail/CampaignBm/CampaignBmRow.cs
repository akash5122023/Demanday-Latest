
namespace AdvanceCRM.BizMail
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("BizMail"), TableName("[dbo].[CampaignBM]")]
    [DisplayName("Campaign"), InstanceName("Campaign")]
    [ReadPermission("BizMail:Read")]
    [InsertPermission("BizMail:Insert")]
    [UpdatePermission("BizMail:Update")]
    [DeletePermission("BizMail:Delete")]
    public sealed class CampaignBmRow : Row<CampaignBmRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Campaignuid"), Size(200), QuickSearch,ReadOnly(true),NameProperty]
        public String Campaignuid
        {
            get { return Fields.Campaignuid[this]; }
            set { Fields.Campaignuid[this] = value; }
        }

        [DisplayName("Name"), Size(200)]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Type")]
        public Masters.CampaignTypeMaster? Type
        {
            get { return (Masters.CampaignTypeMaster?)Fields.Type[this]; }
            set { Fields.Type[this] = (Int32)value; }
        }

        [DisplayName("From Name"), Size(200)]
        public String FromName
        {
            get { return Fields.FromName[this]; }
            set { Fields.FromName[this] = value; }
        }

        [DisplayName("From Email"), Size(200)]
        public String FromEmail
        {
            get { return Fields.FromEmail[this]; }
            set { Fields.FromEmail[this] = value; }
        }

        [DisplayName("Subject"), Size(200)]
        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }

        [DisplayName("Reply To"), Size(200)]
        public String ReplyTo
        {
            get { return Fields.ReplyTo[this]; }
            set { Fields.ReplyTo[this] = value; }
        }

        [DisplayName("Bm List"), Column("BMListId"), NotNull, ForeignKey("[dbo].[BMList]", "Id"), LeftJoin("jBmList"), TextualField("BmListListId"),LookupInclude]
        [LookupEditor(typeof(BmListRow), InplaceAdd = false)]
        public Int32? BmListId
        {
            get { return Fields.BmListId[this]; }
            set { Fields.BmListId[this] = value; }
        }

        [DisplayName("List ID"), Expression("(SELECT ListId FROM BMList WHERE Id= t0.[BMListId]))")]
        public String ListId
        {
            get { return Fields.ListId[this]; }
            set { Fields.ListId[this] = value; }
        }


        [DisplayName("Send At")]
        public DateTime? SendAt
        {
            get { return Fields.SendAt[this]; }
            set { Fields.SendAt[this] = value; }
        }

        [DisplayName("Bm Template"), Column("BMTemplateId"), ForeignKey("[dbo].[BMTemplate]", "Id"), LeftJoin("jBmTemplate"), TextualField("BmTemplateName")]
        [LookupEditor(typeof(BmTemplateRow), InplaceAdd = false)]
        public Int32? BmTemplateId
        {
            get { return Fields.BmTemplateId[this]; }
            set { Fields.BmTemplateId[this] = value; }
        }

        [DisplayName("Inline Css"), Column("InlineCSS")]
        public Masters.InlinecssMaster? InlineCss
        {
            get { return (Masters.InlinecssMaster?)Fields.InlineCss[this]; }
            set { Fields.InlineCss[this] = (Int32?)value; }
        }

        [DisplayName("Auto Plane Test")]
        public Masters.InlinecssMaster? AutoPlaneTest
        {
            get { return (Masters.InlinecssMaster?)Fields.AutoPlaneTest[this]; }
            set { Fields.AutoPlaneTest[this] = (Int32?)value; }
        }

        [DisplayName("Bm List List Id"), Expression("jBmList.[ListId]"),LookupInclude]
        public String BmListListId
        {
            get { return Fields.BmListListId[this]; }
            set { Fields.BmListListId[this] = value; }
        }

        [DisplayName("Bm List Company Name"), Expression("jBmList.[CompanyName]")]
        public String BmListCompanyName
        {
            get { return Fields.BmListCompanyName[this]; }
            set { Fields.BmListCompanyName[this] = value; }
        }

        [DisplayName("Bm List Name"), Expression("jBmList.[Name]")]
        public String BmListName
        {
            get { return Fields.BmListName[this]; }
            set { Fields.BmListName[this] = value; }
        }

        [DisplayName("Bm List City"), Expression("jBmList.[City]")]
        public String BmListCity
        {
            get { return Fields.BmListCity[this]; }
            set { Fields.BmListCity[this] = value; }
        }

        [DisplayName("Bm List Display Name"), Expression("jBmList.[DisplayName]")]
        public String BmListDisplayName
        {
            get { return Fields.BmListDisplayName[this]; }
            set { Fields.BmListDisplayName[this] = value; }
        }

        [DisplayName("Bm List Description"), Expression("jBmList.[Description]")]
        public String BmListDescription
        {
            get { return Fields.BmListDescription[this]; }
            set { Fields.BmListDescription[this] = value; }
        }

        [DisplayName("Bm List From"), Expression("jBmList.[From]")]
        public String BmListFrom
        {
            get { return Fields.BmListFrom[this]; }
            set { Fields.BmListFrom[this] = value; }
        }

        [DisplayName("Bm List Reply To"), Expression("jBmList.[ReplyTo]")]
        public String BmListReplyTo
        {
            get { return Fields.BmListReplyTo[this]; }
            set { Fields.BmListReplyTo[this] = value; }
        }

        [DisplayName("Bm Template Name"), Expression("jBmTemplate.[Name]")]
        public String BmTemplateName
        {
            get { return Fields.BmTemplateName[this]; }
            set { Fields.BmTemplateName[this] = value; }
        }

        [DisplayName("Bm Template Content"), Expression("jBmTemplate.[Content]")]
        public String BmTemplateContent
        {
            get { return Fields.BmTemplateContent[this]; }
            set { Fields.BmTemplateContent[this] = value; }
        }

        [DisplayName("Bm Template Inline Css"), Expression("jBmTemplate.[InlineCSS]")]
        public Int32? BmTemplateInlineCss
        {
            get { return Fields.BmTemplateInlineCss[this]; }
            set { Fields.BmTemplateInlineCss[this] = value; }
        }

       

       
        public CampaignBmRow()
            : base(Fields)
        {
        }
       
        public CampaignBmRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Campaignuid;
            public StringField Name;
            public Int32Field Type;
            public StringField FromName;
            public StringField FromEmail;
            public StringField Subject;
            public StringField ReplyTo;
            public Int32Field BmListId;
            public DateTimeField SendAt;
            public Int32Field BmTemplateId;
            public Int32Field InlineCss;
            public Int32Field AutoPlaneTest;
            public StringField ListId;

            public StringField BmListListId;
            public StringField BmListCompanyName;
            public StringField BmListName;
            public StringField BmListCity;
            public StringField BmListDisplayName;
            public StringField BmListDescription;
            public StringField BmListFrom;
            public StringField BmListReplyTo;

            public StringField BmTemplateName;
            public StringField BmTemplateContent;
            public Int32Field BmTemplateInlineCss;
        }
    }
}
