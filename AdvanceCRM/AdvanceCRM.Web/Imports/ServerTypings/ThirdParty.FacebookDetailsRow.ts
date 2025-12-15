namespace AdvanceCRM.ThirdParty {
    export interface FacebookDetailsRow {
        Id?: number;
        Name?: string;
        Phone?: string;
        Address?: string;
        Email?: string;
        CompaignName?: string;
        AdSetName?: string;
        CreatedTime?: string;
        IsMoved?: boolean;
        LeadId?: string;
        Campaignid?: string;
        Company?: string;
        AdId?: string;
        AdName?: string;
        AdSetId?: string;
        AdditionalDetails?: string;
        Feedback?: string;
    }

    export namespace FacebookDetailsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'ThirdParty.FacebookDetails';
        export const deletePermission = 'Facebook:Inbox';
        export const insertPermission = 'Facebook:Inbox';
        export const readPermission = 'Facebook:Inbox';
        export const updatePermission = 'Facebook:Inbox';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Phone = "Phone",
            Address = "Address",
            Email = "Email",
            CompaignName = "CompaignName",
            AdSetName = "AdSetName",
            CreatedTime = "CreatedTime",
            IsMoved = "IsMoved",
            LeadId = "LeadId",
            Campaignid = "Campaignid",
            Company = "Company",
            AdId = "AdId",
            AdName = "AdName",
            AdSetId = "AdSetId",
            AdditionalDetails = "AdditionalDetails",
            Feedback = "Feedback"
        }
    }
}
