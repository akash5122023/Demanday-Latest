namespace AdvanceCRM.Masters {
    export enum ReligionMaster {
        Hindu = 1,
        Muslim = 2,
        Christian = 3,
        Sikh = 4,
        Buddha = 5,
        Other = 6
    }
    Serenity.Decorators.registerEnumType(ReligionMaster, 'AdvanceCRM.Masters.ReligionMaster', 'Masters.Religion');
}
