namespace AdvanceCRM.Settings {
    export enum PlanBadgeLabel {
        None = 0,
        Regular = 1,
        Popular = 2,
        BestValue = 3,
        Featured = 4,
        LimitedTime = 5
    }
    Serenity.Decorators.registerEnumType(PlanBadgeLabel, 'AdvanceCRM.Settings.PlanBadgeLabel', 'Settings.PlanBadgeLabel');
}
