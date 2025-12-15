namespace AdvanceCRM.Masters {
    export enum ModulesTypeMaster {
        Enquiry = 1,
        Quotation = 2,
        Proforma = 3,
        Sales = 4,
        Contacts = 5,
        CMS = 6,
        AMC = 7
    }
    Serenity.Decorators.registerEnumType(ModulesTypeMaster, 'AdvanceCRM.Masters.ModulesTypeMaster', 'Masters.ModulesType');
}
