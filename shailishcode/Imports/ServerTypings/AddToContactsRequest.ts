namespace AdvanceCRM {
    export interface AddToContactsRequest extends Serenity.ServiceRequest {
        ContactType?: string;
        CompanyName?: string;
        ContactPerson?: string;
        Source?: string;
        Requirement?: string;
        Country?: string;
        Date?: string;
        Phone?: string;
        Address?: string;
        AdditionalInfo?: string;
        Email?: string;
    }
}
