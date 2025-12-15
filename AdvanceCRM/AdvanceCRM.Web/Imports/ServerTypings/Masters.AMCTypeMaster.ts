namespace AdvanceCRM.Masters {
    export enum AMCTypeMaster {
        Quantity = 1,
        Visit = 2,
        QtyAndVisit = 3
    }
    Serenity.Decorators.registerEnumType(AMCTypeMaster, 'AdvanceCRM.Masters.AMCTypeMaster', 'Masters.AMCType');
}
