namespace AdvanceCRM.Masters {
    export enum ContactsStage {
        Enquiry = 1,
        Quotation = 2,
        Sales = 3,
        Purchase = 4,
        CMS = 5
    }
    Serenity.Decorators.registerEnumType(ContactsStage, 'AdvanceCRM.Masters.ContactsStage', 'Masters.ContactsStage');
}
