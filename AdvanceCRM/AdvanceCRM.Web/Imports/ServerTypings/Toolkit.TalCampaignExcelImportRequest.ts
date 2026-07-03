namespace AdvanceCRM.Toolkit {
    export interface TalCampaignExcelImportRequest extends Serenity.ServiceRequest {
        FileName?: string;
        CampaignId?: number;
    }
}
