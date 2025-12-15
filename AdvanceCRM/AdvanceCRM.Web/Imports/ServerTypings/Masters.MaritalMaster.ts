namespace AdvanceCRM.Masters {
    export enum MaritalMaster {
        Single = 1,
        Married = 2,
        Divorced = 3,
        Widowed = 4
    }
    Serenity.Decorators.registerEnumType(MaritalMaster, 'AdvanceCRM.Masters.MaritalMaster', 'Masters.Marital');
}
