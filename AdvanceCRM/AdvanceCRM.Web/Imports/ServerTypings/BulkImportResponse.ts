namespace AdvanceCRM {
    export interface BulkImportResponse extends Serenity.ServiceResponse {
        Inserted?: number;
        Updated?: number;
        Status?: string;
        ErrorList?: string[];
    }
}
