namespace AdvanceCRM.Modules.ThirdParty.KnowlarityDetails {
    export enum IvrCallDuration {
        MissedCalls = 0,
        AnsweredCalls = 1,
        CustomerMissed = 2,
        AgentMissed = 3,
        TodaysCalls = 4
    }
    Serenity.Decorators.registerEnumType(IvrCallDuration, 'AdvanceCRM.Modules.ThirdParty.KnowlarityDetails.IvrCallDuration', 'ThirdParty.CallDurationState');
}
