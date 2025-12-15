namespace AdvanceCRM.Masters {
    export enum YearInPrefix {
        None = 1,
        Year = 2,
        FinacialYear = 3
    }
    Serenity.Decorators.registerEnumType(YearInPrefix, 'AdvanceCRM.Masters.YearInPrefix', 'Masters.YearInPrefix');
}
