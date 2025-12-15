namespace AdvanceCRM.Masters {
    export enum BizMailRulesMaster {
        Contact = 1,
        CMS = 2,
        Facebook = 3,
        IndiaMart = 4,
        InstaMojo = 5,
        IVR = 6,
        JustDial = 7,
        TradeIndia = 8,
        Visit = 9,
        WebEnquiry = 10,
        WooCommerce = 11
    }
    Serenity.Decorators.registerEnumType(BizMailRulesMaster, 'AdvanceCRM.Masters.BizMailRulesMaster', 'Masters.BizMailRules');
}
