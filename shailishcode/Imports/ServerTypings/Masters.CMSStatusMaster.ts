namespace AdvanceCRM.Masters {
    export enum CMSStatusMaster {
        Open = 1,
        Closed = 2,
        Analysis = 3,
        Action = 4,
        Verify = 5
    }
    Serenity.Decorators.registerEnumType(CMSStatusMaster, 'AdvanceCRM.Masters.CMSStatusMaster', 'Masters.CMSStatus');
}
