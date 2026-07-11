namespace AdvanceCRM.Toolkit {
    export interface ClientSupressionExcelImportRequest extends Serenity.ServiceRequest {
        FileName?: string;
        CampaignId?: number;
    }
}
