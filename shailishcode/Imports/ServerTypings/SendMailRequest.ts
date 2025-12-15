namespace AdvanceCRM {
    export interface SendMailRequest extends Serenity.ServiceRequest {
        Id?: number;
        MailType?: string;
        EmailId?: string;
        Subject?: string;
        Message?: string;
    }
}
