namespace AdvanceCRM.Masters {
    export enum PrintTemplates {
        Default = 1,
        Template1 = 2,
        Template2 = 3
    }
    Serenity.Decorators.registerEnumType(PrintTemplates, 'AdvanceCRM.Masters.PrintTemplates', 'Masters.PrintTemplates');
}
