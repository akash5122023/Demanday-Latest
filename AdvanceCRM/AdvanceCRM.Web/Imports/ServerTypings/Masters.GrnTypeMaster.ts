namespace AdvanceCRM.Masters {
    export enum GrnTypeMaster {
        WithPO = 1,
        WithoutPO = 2
    }
    Serenity.Decorators.registerEnumType(GrnTypeMaster, 'AdvanceCRM.Masters.GrnTypeMaster', 'Masters.GrnType');
}
