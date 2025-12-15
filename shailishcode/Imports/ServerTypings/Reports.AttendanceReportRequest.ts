namespace AdvanceCRM.Reports {
    export interface AttendanceReportRequest extends Serenity.ServiceRequest {
        Type?: AttendanceReportType;
        DateFrom?: string;
        DateTo?: string;
        Representative?: number;
    }
}
