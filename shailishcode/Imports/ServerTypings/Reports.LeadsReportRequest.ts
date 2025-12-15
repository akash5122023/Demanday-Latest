namespace AdvanceCRM.Reports {
    export interface LeadsReportRequest extends Serenity.ServiceRequest {
        Type?: LeadsReportType;
        DateFrom?: string;
        DateTo?: string;
        Representative?: number;
        Contact?: number;
        TeamsId?: number;
        Project?: number;
    }
}
