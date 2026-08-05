namespace AdvanceCRM.Demanday {
    export interface AccountCampaignSummaryResponse extends Serenity.ServiceResponse {
        Total?: number;
        Items?: AccountCampaignCount[];
    }
}
