namespace AdvanceCRM.Reports {
    export enum LeadsReportType {
        Customerwise = 1,
        Divisionwise = 2,
        Statistics = 3,
        LostReasons = 4,
        Mediawise = 5,
        Productwise = 6,
        Representativewise = 7,
        Detailed = 8,
        TeamWise = 9
    }
    Serenity.Decorators.registerEnumType(LeadsReportType, 'AdvanceCRM.Reports.LeadsReportType', 'Reports.LeadsReportType');
}
