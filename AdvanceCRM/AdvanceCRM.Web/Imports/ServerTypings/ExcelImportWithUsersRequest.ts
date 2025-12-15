namespace AdvanceCRM {
    export interface ExcelImportWithUsersRequest extends Serenity.ServiceRequest {
        FileName?: string;
        UIds?: string[];
    }
}
