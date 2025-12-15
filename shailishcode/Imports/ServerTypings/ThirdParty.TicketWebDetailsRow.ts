namespace AdvanceCRM.ThirdParty {
    export interface TicketWebDetailsRow {
        Id?: number;
        Name?: string;
        Phone?: string;
        Email?: string;
        Address?: string;
        ProductName?: string;
        Requirement?: string;
        DateTime?: string;
        PurchaseDate?: string;
        ComplaintDetails?: string;
        Attachment?: string;
        IsMoved?: boolean;
    }

    export namespace TicketWebDetailsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'ThirdParty.TicketWebDetails';
        export const deletePermission = 'WebsiteEnquiry:Inbox';
        export const insertPermission = 'WebsiteEnquiry:Inbox';
        export const readPermission = 'WebsiteEnquiry:Inbox';
        export const updatePermission = 'WebsiteEnquiry:Inbox';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Phone = "Phone",
            Email = "Email",
            Address = "Address",
            ProductName = "ProductName",
            Requirement = "Requirement",
            DateTime = "DateTime",
            PurchaseDate = "PurchaseDate",
            ComplaintDetails = "ComplaintDetails",
            Attachment = "Attachment",
            IsMoved = "IsMoved"
        }
    }
}
