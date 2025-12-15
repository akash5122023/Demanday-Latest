namespace AdvanceCRM.Masters {
    export enum GenderMaster {
        Male = 1,
        Female = 2,
        Transgender = 3
    }
    Serenity.Decorators.registerEnumType(GenderMaster, 'AdvanceCRM.Masters.GenderMaster', 'Masters.Gender');
}
