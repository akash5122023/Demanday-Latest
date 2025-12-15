namespace AdvanceCRM.Masters {
    export enum MailRuleMaster {
        Status = 1,
        Stage = 2,
        Source = 3,
        Type = 4,
        ClosingType = 5
    }
    Serenity.Decorators.registerEnumType(MailRuleMaster, 'AdvanceCRM.Masters.MailRuleMaster', 'Masters.MailRule');
}
