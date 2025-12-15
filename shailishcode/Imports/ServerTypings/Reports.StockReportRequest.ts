namespace AdvanceCRM.Reports {
    export interface StockReportRequest extends Serenity.ServiceRequest {
        Type?: StockReportType;
        Branch?: number;
        Division?: number;
        Group?: number;
        Product?: number;
        Status?: number;
        PurchaseOrderNo?: number;
    }
}
