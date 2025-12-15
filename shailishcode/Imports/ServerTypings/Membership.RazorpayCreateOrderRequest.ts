namespace AdvanceCRM.Membership {
    export interface RazorpayCreateOrderRequest extends Serenity.ServiceRequest {
        Plan?: string;
    }
}
