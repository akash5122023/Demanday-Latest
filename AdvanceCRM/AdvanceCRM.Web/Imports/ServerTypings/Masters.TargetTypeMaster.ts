namespace AdvanceCRM.Masters {
    export enum TargetTypeMaster {
        Enquiry = 1,
        Quotation = 2,
        Sales = 3,
        Calling = 4
    }
    Serenity.Decorators.registerEnumType(TargetTypeMaster, 'AdvanceCRM.Masters.TargetTypeMaster', 'Masters.TargetType');
}
