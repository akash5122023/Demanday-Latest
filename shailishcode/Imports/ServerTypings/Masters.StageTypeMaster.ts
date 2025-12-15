namespace AdvanceCRM.Masters {
    export enum StageTypeMaster {
        Enquiry = 1,
        Quotation = 2,
        Invoice = 3,
        Service = 4,
        Manufacturing = 5
    }
    Serenity.Decorators.registerEnumType(StageTypeMaster, 'AdvanceCRM.Masters.StageTypeMaster', 'Masters.StageType');
}
