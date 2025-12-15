namespace AdvanceCRM.Masters {
    export enum TaskPeriodMaster {
        Monthly = 1,
        Quaterly = 2,
        HalfYear = 3,
        Yearly = 4
    }
    Serenity.Decorators.registerEnumType(TaskPeriodMaster, 'AdvanceCRM.Masters.TaskPeriodMaster', 'Masters.TaskPeriod');
}
