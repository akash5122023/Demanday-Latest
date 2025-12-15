namespace AdvanceCRM.Masters {
    export enum AddInfoTypeMaster {
        Enquiry = 1,
        Quotation = 2,
        Contact = 3
    }
    Serenity.Decorators.registerEnumType(AddInfoTypeMaster, 'AdvanceCRM.Masters.AddInfoTypeMaster', 'Masters.AddInfoType');
}
