namespace AdvanceCRM.Masters {
    export enum PriorityMaster {
        Low = 1,
        Medium = 2,
        High = 3,
        Urgent = 4
    }
    Serenity.Decorators.registerEnumType(PriorityMaster, 'AdvanceCRM.Masters.PriorityMaster', 'Masters.Priority');
}
