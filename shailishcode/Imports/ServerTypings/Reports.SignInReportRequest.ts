namespace AdvanceCRM.Reports {
    export interface SignInReportRequest extends Serenity.ServiceRequest {
        Representative?: number;
        DateFrom?: string;
        DateTo?: string;
    }
}
