namespace AdvanceCRM.EBBCheck {
    export enum EbbStatus {
        Clear = 1,
        Bounce = 2,
        FiftyFifty = 3,
        Waiting = 4
    }
    Serenity.Decorators.registerEnumType(EbbStatus, 'AdvanceCRM.EBBCheck.EbbStatus', 'EBBCheck.EbbStatus');
}
