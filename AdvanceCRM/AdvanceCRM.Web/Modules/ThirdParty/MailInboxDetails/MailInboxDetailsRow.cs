
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[MailInboxDetails]")]
    [DisplayName("Mail Inbox Details"), InstanceName("Mail Inbox Details")]
    [ReadPermission("MailInbox:Inbox")]
    [ModifyPermission("MailInbox:Inbox")]
    public sealed class MailInboxDetailsRow : Row<MailInboxDetailsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Subject"), Size(255), QuickSearch, LookupInclude,NameProperty]
        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }

        [DisplayName("Phone"), Size(20)]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Message Id"), Size(255), LookupInclude]
        public String ToName
        {
            get { return Fields.ToName[this]; }
            set { Fields.ToName[this] = value; }
        }

        [DisplayName("To"), Size(255)]
        public String To
        {
            get { return Fields.To[this]; }
            set { Fields.To[this] = value; }
        }

        [DisplayName("To Address"), Size(255), LookupInclude]
        public String ToAddress
        {
            get { return Fields.ToAddress[this]; }
            set { Fields.ToAddress[this] = value; }
        }

        [DisplayName("From Name"), Size(255),Required]
        public String FromName
        {
            get { return Fields.FromName[this]; }
            set { Fields.FromName[this] = value; }
        }

        [DisplayName("From"), Size(255)]
        public String From
        {
            get { return Fields.From[this]; }
            set { Fields.From[this] = value; }
        }

        [DisplayName("From Address"), Size(255), LookupInclude]
        public String FromAddress
        {
            get { return Fields.FromAddress[this]; }
            set { Fields.FromAddress[this] = value; }
        }

        [DisplayName("Created Date"), NotNull,LookupInclude]
        public DateTime? CreatedDate
        {
            get { return Fields.CreatedDate[this]; }
            set { Fields.CreatedDate[this] = value; }
        }

        [DisplayName("Content"), Size(4000),TextAreaEditor(Rows=5), LookupInclude,Required]
        public String Content
        {
            get { return Fields.Content[this]; }
            set { Fields.Content[this] = value; }
        }

        [DisplayName("Attachment"), Size(2000)]
        public String Attachment
        {
            get { return Fields.Attachment[this]; }
            set { Fields.Attachment[this] = value; }
        }
        [DisplayName("MessagehjyId"), Column("messageID"), Size(255)]
        public String MessageId
        {
            get { return Fields.MessageId[this]; }
            set { Fields.MessageId[this] = value; }
        }

        [DisplayName("Is Moved"), NotNull]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }
       


       
        public MailInboxDetailsRow()
            : base(Fields)
        {
        }
       
        public MailInboxDetailsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Subject;
            public StringField Phone;
            public StringField ToName;
            public StringField To;
            public StringField ToAddress;
            public StringField FromName;
            public StringField From;
            public StringField FromAddress;
            public DateTimeField CreatedDate;
            public StringField Content;
            public StringField Attachment;
            public StringField MessageId;
            public BooleanField IsMoved;
            
        }
    }
}
