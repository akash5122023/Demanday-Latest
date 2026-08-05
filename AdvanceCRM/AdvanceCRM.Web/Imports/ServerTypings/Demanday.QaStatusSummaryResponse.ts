namespace AdvanceCRM.Demanday {
    export interface QaStatusSummaryResponse extends Serenity.ServiceResponse {
        Total?: number;
        Items?: QaStatusCount[];
    }
}
