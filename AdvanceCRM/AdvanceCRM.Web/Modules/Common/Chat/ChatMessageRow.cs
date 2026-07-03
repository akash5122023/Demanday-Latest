using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace AdvanceCRM.Common
{
    [ConnectionKey("Default"), Module("Common"), TableName("[dbo].[ChatMessage]")]
    [DisplayName("Chat Message"), InstanceName("Chat Message")]
    public sealed class ChatMessageRow : Row<ChatMessageRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Sender"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jSender"), TextualField("SenderName")]
        public Int32? SenderId
        {
            get => fields.SenderId[this];
            set => fields.SenderId[this] = value;
        }

        [DisplayName("Receiver"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jReceiver"), TextualField("ReceiverName")]
        public Int32? ReceiverId
        {
            get => fields.ReceiverId[this];
            set => fields.ReceiverId[this] = value;
        }

        [DisplayName("Message")]
        public String Message
        {
            get => fields.Message[this];
            set => fields.Message[this] = value;
        }

        [DisplayName("Sent Date")]
        public DateTime? SentDate
        {
            get => fields.SentDate[this];
            set => fields.SentDate[this] = value;
        }

        [DisplayName("Attachment Path"), Size(500)]
        public String AttachmentPath
        {
            get => fields.AttachmentPath[this];
            set => fields.AttachmentPath[this] = value;
        }

        [DisplayName("Attachment Name"), Size(255)]
        public String AttachmentName
        {
            get => fields.AttachmentName[this];
            set => fields.AttachmentName[this] = value;
        }

        [DisplayName("Attachment Type"), Size(20)]
        public String AttachmentType
        {
            get => fields.AttachmentType[this];
            set => fields.AttachmentType[this] = value;
        }

        [DisplayName("Is Read"), NotNull]
        public Boolean? IsRead
        {
            get => fields.IsRead[this];
            set => fields.IsRead[this] = value;
        }

        [DisplayName("Is Delivered"), NotNull]
        public Boolean? IsDelivered
        {
            get => fields.IsDelivered[this];
            set => fields.IsDelivered[this] = value;
        }

        [DisplayName("Sender Name"), Expression("jSender.[DisplayName]")]
        public String SenderName
        {
            get => fields.SenderName[this];
            set => fields.SenderName[this] = value;
        }

        [DisplayName("Receiver Name"), Expression("jReceiver.[DisplayName]")]
        public String ReceiverName
        {
            get => fields.ReceiverName[this];
            set => fields.ReceiverName[this] = value;
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field SenderId;
            public Int32Field ReceiverId;
            public StringField Message;
            public DateTimeField SentDate;
            public StringField AttachmentPath;
            public StringField AttachmentName;
            public StringField AttachmentType;
            public BooleanField IsRead;
            public BooleanField IsDelivered;
            public StringField SenderName;
            public StringField ReceiverName;
        }
    }
}
