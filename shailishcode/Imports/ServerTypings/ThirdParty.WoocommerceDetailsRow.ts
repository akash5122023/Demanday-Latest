namespace AdvanceCRM.ThirdParty {
    export interface WoocommerceDetailsRow {
        Id?: number;
        WooId?: string;
        FirstName?: string;
        LastName?: string;
        Company?: string;
        Email?: string;
        Phone?: string;
        Address?: string;
        City?: string;
        State?: string;
        Country?: string;
        CreatedDate?: string;
        IsMoved?: boolean;
        Feedback?: string;
    }

    export namespace WoocommerceDetailsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'FirstName';
        export const localTextPrefix = 'ThirdParty.WoocommerceDetails';
        export const deletePermission = 'Woocommerce:Inbox';
        export const insertPermission = 'Woocommerce:Inbox';
        export const readPermission = 'Woocommerce:Inbox';
        export const updatePermission = 'Woocommerce:Inbox';

        export declare const enum Fields {
            Id = "Id",
            WooId = "WooId",
            FirstName = "FirstName",
            LastName = "LastName",
            Company = "Company",
            Email = "Email",
            Phone = "Phone",
            Address = "Address",
            City = "City",
            State = "State",
            Country = "Country",
            CreatedDate = "CreatedDate",
            IsMoved = "IsMoved",
            Feedback = "Feedback"
        }
    }
}
