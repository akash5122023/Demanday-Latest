namespace AdvanceCRM {
    export interface TransferRequest extends Serenity.ServiceRequest {
        Type?: string;
        FromID?: number;
        ToID?: number;
    }
}
