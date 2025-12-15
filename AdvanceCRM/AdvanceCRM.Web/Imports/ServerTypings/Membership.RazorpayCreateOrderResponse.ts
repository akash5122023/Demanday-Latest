namespace AdvanceCRM.Membership {
    export interface RazorpayCreateOrderResponse extends Serenity.ServiceResponse {
        OrderId?: string;
        Amount?: number;
        Currency?: string;
        Key?: string;
        Success?: boolean;
    }
}
