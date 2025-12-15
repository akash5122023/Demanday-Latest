namespace AdvanceCRM.Settings {
    export interface MailChimpConfigurationRow {
        Id?: number;
        ApiKey?: string;
        CompanyName?: string;
        Address?: string;
        City?: string;
        State?: string;
        Zip?: string;
        Country?: number;
        Phone?: string;
        Reminder?: string;
        ContactFromEmail?: string;
        ContactFromName?: string;
        ContactSubject?: string;
        EnquiryFromEmail?: string;
        EnquiryFromName?: string;
        EnquirySubject?: string;
        QuotationFromEmail?: string;
        QuotationFromName?: string;
        QuotationSubject?: string;
        SaleFromEmail?: string;
        SaleFromName?: string;
        SaleSubject?: string;
    }

    export namespace MailChimpConfigurationRow {
        export const idProperty = 'Id';
        export const nameProperty = 'ApiKey';
        export const localTextPrefix = 'Settings.MailChimpConfiguration';
        export const deletePermission = 'Settings:MailChimp';
        export const insertPermission = 'Settings:MailChimp';
        export const readPermission = 'Settings:MailChimp';
        export const updatePermission = 'Settings:MailChimp';

        export declare const enum Fields {
            Id = "Id",
            ApiKey = "ApiKey",
            CompanyName = "CompanyName",
            Address = "Address",
            City = "City",
            State = "State",
            Zip = "Zip",
            Country = "Country",
            Phone = "Phone",
            Reminder = "Reminder",
            ContactFromEmail = "ContactFromEmail",
            ContactFromName = "ContactFromName",
            ContactSubject = "ContactSubject",
            EnquiryFromEmail = "EnquiryFromEmail",
            EnquiryFromName = "EnquiryFromName",
            EnquirySubject = "EnquirySubject",
            QuotationFromEmail = "QuotationFromEmail",
            QuotationFromName = "QuotationFromName",
            QuotationSubject = "QuotationSubject",
            SaleFromEmail = "SaleFromEmail",
            SaleFromName = "SaleFromName",
            SaleSubject = "SaleSubject"
        }
    }
}
