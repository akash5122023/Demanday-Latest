namespace AdvanceCRM.Masters {
    export enum EnquiryTypeMaster {
        Hot = 1,
        Warm = 2,
        Cold = 3
    }
    Serenity.Decorators.registerEnumType(EnquiryTypeMaster, 'AdvanceCRM.Masters.EnquiryTypeMaster', 'Masters.EnquiryType');
}
