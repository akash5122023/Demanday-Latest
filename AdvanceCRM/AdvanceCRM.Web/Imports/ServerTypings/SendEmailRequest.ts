namespace AdvanceCRM {
    export interface SendEmailRequest extends Serenity.ServiceRequest {
        Id?: number;
        Email?: string;
        Subject?: string;
        EmailType?: string;
        Senddate?: string;
    }
}
