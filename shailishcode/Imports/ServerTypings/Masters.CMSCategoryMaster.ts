namespace AdvanceCRM.Masters {
    export enum CMSCategoryMaster {
        Chargeable = 1,
        NonChargeable = 2
    }
    Serenity.Decorators.registerEnumType(CMSCategoryMaster, 'AdvanceCRM.Masters.CMSCategoryMaster', 'Masters.CMSCategory');
}
