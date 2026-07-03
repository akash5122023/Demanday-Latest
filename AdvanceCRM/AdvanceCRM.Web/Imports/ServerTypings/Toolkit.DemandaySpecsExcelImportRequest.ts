namespace AdvanceCRM.Toolkit {
    export interface DemandaySpecsExcelImportRequest extends Serenity.ServiceRequest {
        FileName?: string;
        CampaignId?: number;
    }
}
