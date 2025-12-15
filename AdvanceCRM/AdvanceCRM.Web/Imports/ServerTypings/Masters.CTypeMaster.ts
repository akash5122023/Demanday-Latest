namespace AdvanceCRM.Masters {
    export enum CTypeMaster {
        Individual = 1,
        Organization = 2
    }
    Serenity.Decorators.registerEnumType(CTypeMaster, 'AdvanceCRM.Masters.CTypeMaster', 'Masters.CType');
}
