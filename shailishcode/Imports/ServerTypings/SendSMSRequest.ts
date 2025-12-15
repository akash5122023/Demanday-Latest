namespace AdvanceCRM {
    export interface SendSMSRequest extends Serenity.ServiceRequest {
        Id?: number;
        Phone?: string;
        SMSType?: string;
        EngineerID?: number;
        TemplateID?: string;
    }
}
