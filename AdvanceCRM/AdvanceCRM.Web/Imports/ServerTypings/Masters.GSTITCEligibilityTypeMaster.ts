namespace AdvanceCRM.Masters {
    export enum GSTITCEligibilityTypeMaster {
        Inputs = 1,
        Capitalgoods = 2,
        Inputservices = 3,
        Ineligible = 4
    }
    Serenity.Decorators.registerEnumType(GSTITCEligibilityTypeMaster, 'AdvanceCRM.Masters.GSTITCEligibilityTypeMaster', 'Masters.GSTITCEligibilityType');
}
