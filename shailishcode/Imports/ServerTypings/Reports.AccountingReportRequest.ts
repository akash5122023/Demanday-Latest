namespace AdvanceCRM.Reports {
    export interface AccountingReportRequest extends Serenity.ServiceRequest {
        Type?: AccountingReportType;
        DateFrom?: string;
        DateTo?: string;
        Contact?: number;
        Head?: number;
        Employee?: number;
        Project?: number;
        Bank?: number;
        CashIn?: boolean;
        CashOut?: boolean;
    }
}
