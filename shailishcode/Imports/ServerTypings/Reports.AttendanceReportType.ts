namespace AdvanceCRM.Reports {
    export enum AttendanceReportType {
        All = 1,
        Representativewise = 2
    }
    Serenity.Decorators.registerEnumType(AttendanceReportType, 'AdvanceCRM.Reports.AttendanceReportType', 'Reports.AttendanceReportType');
}
