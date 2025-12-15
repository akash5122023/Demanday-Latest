namespace AdvanceCRM {
    export interface MailChimpRequest extends Serenity.ServiceRequest {
        MailChimpIds?: string[];
        ListName?: string;
    }
}
