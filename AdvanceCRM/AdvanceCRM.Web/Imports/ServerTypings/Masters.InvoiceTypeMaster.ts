namespace AdvanceCRM.Masters {
    export enum InvoiceTypeMaster {
        Cash = 1,
        Credit = 2,
        Card = 3,
        AppTransfer = 4
    }
    Serenity.Decorators.registerEnumType(InvoiceTypeMaster, 'AdvanceCRM.Masters.InvoiceTypeMaster', 'Masters.InvoiceType');
}
