namespace AdvanceCRM.Masters {
    export enum ClosingTypeMaster {
        Won = 1,
        Lost = 2,
        Revised = 3
    }
    Serenity.Decorators.registerEnumType(ClosingTypeMaster, 'AdvanceCRM.Masters.ClosingTypeMaster', 'Masters.ClosingType');
}
