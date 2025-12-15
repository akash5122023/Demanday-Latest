namespace AdvanceCRM.Masters {
    export enum NotificationModules {
        Contacts = 1,
        Enquiry = 2,
        Quotation = 3,
        Proforma = 4,
        Sales = 5,
        Challan = 6,
        SalesReturn = 7,
        Purchase = 8,
        PurchaseReturn = 9,
        PurchaseOrder = 10,
        AMC = 11,
        CMS = 12,
        TeleCalling = 13,
        Tasks = 14
    }
    Serenity.Decorators.registerEnumType(NotificationModules, 'AdvanceCRM.Masters.NotificationModules', 'Masters.NotificationModules');
}
