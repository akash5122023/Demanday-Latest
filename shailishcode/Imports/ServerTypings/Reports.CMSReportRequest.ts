namespace AdvanceCRM.Reports {
    export interface CMSReportRequest extends Serenity.ServiceRequest {
        Type?: CMSReportType;
        DateFrom?: string;
        DateTo?: string;
        Representative?: number;
        Contact?: number;
        TeamsId?: number;
        Project?: number;
    }
}
