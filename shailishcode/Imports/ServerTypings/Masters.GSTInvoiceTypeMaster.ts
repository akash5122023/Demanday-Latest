namespace AdvanceCRM.Masters {
    export enum GSTInvoiceTypeMaster {
        Regular = 1,
        SEZsupplieswithpayment = 2,
        SEZsupplieswithoutpayment = 3,
        DeemedExp = 4
    }
    Serenity.Decorators.registerEnumType(GSTInvoiceTypeMaster, 'AdvanceCRM.Masters.GSTInvoiceTypeMaster', 'Masters.GSTInvoiceType');
}
