namespace AdvanceCRM.Masters {
    export enum WinPercentageMaster {
        TwentyFivePercent = 1,
        FiftyPercent = 2,
        SeventyFivePercent = 3,
        NintyFivePercent = 4
    }
    Serenity.Decorators.registerEnumType(WinPercentageMaster, 'AdvanceCRM.Masters.WinPercentageMaster', 'Masters.WinPercentage');
}
