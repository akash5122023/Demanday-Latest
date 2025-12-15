namespace AdvanceCRM.Reports {
    export enum CMSReportType {
        Customerwise = 1,
        ProjectWise = 10
    }
    Serenity.Decorators.registerEnumType(CMSReportType, 'AdvanceCRM.Reports.CMSReportType', 'Reports.CMSReportType');
}
