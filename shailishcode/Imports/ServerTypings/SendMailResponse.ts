namespace AdvanceCRM {
    export interface SendMailResponse extends Serenity.ServiceResponse {
        Status?: string;
        ErrorList?: string[];
        Id?: number;
    }
}
