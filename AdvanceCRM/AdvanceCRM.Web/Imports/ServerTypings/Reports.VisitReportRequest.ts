namespace AdvanceCRM.Reports {
    export interface VisitReportRequest extends Serenity.ServiceRequest {
        Type?: AttendanceReportType;
        DateFrom?: string;
        DateTo?: string;
        Representative?: number;
    }
}
