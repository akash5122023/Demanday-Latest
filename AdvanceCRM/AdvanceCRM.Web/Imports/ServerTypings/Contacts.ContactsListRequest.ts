namespace AdvanceCRM.Contacts {
    export interface ContactsListRequest extends Serenity.ListRequest {
        SubContactsId?: number;
        Stage?: Masters.ContactsStage;
    }
}
