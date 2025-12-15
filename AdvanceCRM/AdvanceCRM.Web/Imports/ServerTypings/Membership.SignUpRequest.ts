namespace AdvanceCRM.Membership {
    export interface SignUpRequest extends Serenity.ServiceRequest {
        Plan?: string;
        Company?: string;
        Subdomain?: string;
        DisplayName?: string;
        Email?: string;
        MobileNumber?: string;
        Password?: string;
        PaymentOrderId?: string;
        PaymentId?: string;
        PaymentSignature?: string;
        PaymentAmount?: string;
        PaymentCurrency?: string;
        Users?: number;
        CouponCode?: string;
    }
}
