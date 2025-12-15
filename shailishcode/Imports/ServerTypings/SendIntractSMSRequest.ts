namespace AdvanceCRM {
    export interface SendIntractSMSRequest extends Serenity.ServiceRequest {
        Id?: number;
        Phone?: string;
        Variable?: string;
        Template?: string;
        ImageUrl?: string;
    }
}
