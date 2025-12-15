namespace AdvanceCRM.Masters {
    export enum TypeMaster {
        Domestic = 1,
        International = 2
    }
    Serenity.Decorators.registerEnumType(TypeMaster, 'AdvanceCRM.Masters.TypeMaster', 'Masters.Type');
}
