namespace AdvanceCRM.Reports {
    export enum StockReportType {
        Current = 1,
        AllBranchwise = 2,
        Branchwise = 3,
        Divisionwise = 4,
        Groupwise = 5,
        Reorder = 6,
        AllBranchDivisionWise = 7,
        AllBranchProductWise = 8
    }
    Serenity.Decorators.registerEnumType(StockReportType, 'AdvanceCRM.Reports.StockReportType', 'Reports.StockReportType');
}
