namespace AdvanceCRM.Masters {
    export enum CampaignTypeMaster {
        Regular = 1,
        Autoresponder = 2
    }
    Serenity.Decorators.registerEnumType(CampaignTypeMaster, 'AdvanceCRM.Masters.CampaignTypeMaster', 'Masters.CampaignType');
}
