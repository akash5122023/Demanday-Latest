namespace AdvanceCRM.Reports {
    export interface SalesReportRequest extends Serenity.ServiceRequest {
        Type?: SalesReportType;
        DateFrom?: string;
        DateTo?: string;
        Representative?: number;
        Contact?: number;
        TeamsId?: number;
    }
}
