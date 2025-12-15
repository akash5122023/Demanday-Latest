namespace AdvanceCRM.Reports {
    export enum SalesReportType {
        Customerwise = 1,
        Divisionwise = 2,
        Mediawise = 3,
        Productwise = 4,
        Representativewise = 5
    }
    Serenity.Decorators.registerEnumType(SalesReportType, 'AdvanceCRM.Reports.SalesReportType', 'Reports.SalesReportType');
}
