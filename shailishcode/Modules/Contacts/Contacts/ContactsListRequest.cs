using Serenity.Services;

namespace AdvanceCRM.Contacts
{
    public class ContactsListRequest : ListRequest
    {
        public int? SubContactsId { get; set; }
        public Masters.ContactsStage? Stage { get; set; }
    }
}