namespace AdvanceCRM.Toolkit {
    export interface MasterSupressionExcelImportRequest extends Serenity.ServiceRequest {
        FileName?: string;
        MasterAccountId?: number;
    }
}
