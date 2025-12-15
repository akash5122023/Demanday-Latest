namespace AdvanceCRM {
    export interface BulkRequest extends Serenity.ServiceRequest {
        EnqIds?: string[];
        Ids?: string[];
    }
}
