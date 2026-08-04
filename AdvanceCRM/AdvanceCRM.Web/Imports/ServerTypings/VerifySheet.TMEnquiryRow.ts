namespace AdvanceCRM.VerifySheet {
    export interface TMEnquiryRow {
        Id?: number;
        MasterAccountId?: number;
        CampaignId?: string;
        FirstName?: string;
        LastName?: string;
        Email?: string;
        CompanyName?: string;
        Timestamp?: string;
        DemandayTeleMarketingEnquiryId?: number;
        CreatedOn?: string;
        CreatedBy?: string;
        UpdatedOn?: string;
        UpdatedBy?: string;
    }

    export namespace TMEnquiryRow {
        export const idProperty = 'Id';
        export const nameProperty = 'FirstName';
        export const localTextPrefix = 'VerifySheet.TMEnquiry';
        export const lookupKey = 'VerifySheet.TMEnquiry';

        export function getLookup(): Q.Lookup<TMEnquiryRow> {
            return Q.getLookup<TMEnquiryRow>('VerifySheet.TMEnquiry');
        }
        export const deletePermission = 'TMEnquiry:Delete';
        export const insertPermission = 'TMEnquiry:Insert';
        export const readPermission = 'TMEnquiry:Read';
        export const updatePermission = 'TMEnquiry:Update';

        export declare const enum Fields {
            Id = "Id",
            MasterAccountId = "MasterAccountId",
            CampaignId = "CampaignId",
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "Email",
            CompanyName = "CompanyName",
            Timestamp = "Timestamp",
            DemandayTeleMarketingEnquiryId = "DemandayTeleMarketingEnquiryId",
            CreatedOn = "CreatedOn",
            CreatedBy = "CreatedBy",
            UpdatedOn = "UpdatedOn",
            UpdatedBy = "UpdatedBy"
        }
    }
}
