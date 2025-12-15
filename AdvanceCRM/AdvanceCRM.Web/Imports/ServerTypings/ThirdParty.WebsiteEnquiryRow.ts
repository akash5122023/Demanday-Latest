namespace AdvanceCRM.ThirdParty {
    export interface WebsiteEnquiryRow {
        Id?: number;
        Name?: string;
        Phone?: string;
        Email?: string;
        Address?: string;
        DateTime?: string;
        Requirement?: string;
        Feedback?: string;
        IsMoved?: boolean;
    }

    export namespace WebsiteEnquiryRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'ThirdParty.WebsiteEnquiry';
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
            DateTime = "DateTime",
            Requirement = "Requirement",
            Feedback = "Feedback",
            IsMoved = "IsMoved"
        }
    }
}
