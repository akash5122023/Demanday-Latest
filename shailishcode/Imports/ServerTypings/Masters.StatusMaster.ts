namespace AdvanceCRM.Masters {
    export enum StatusMaster {
        Open = 1,
        Closed = 2,
        Pending = 3,
        Expired = 4
    }
    Serenity.Decorators.registerEnumType(StatusMaster, 'AdvanceCRM.Masters.StatusMaster', 'Masters.Status');
}
