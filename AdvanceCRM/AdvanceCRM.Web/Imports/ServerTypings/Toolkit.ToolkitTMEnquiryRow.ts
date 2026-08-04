namespace AdvanceCRM.Toolkit {
    export interface ToolkitTMEnquiryRow {
        SrNo?: number;
        MasterAccountId?: number;
        CampaignId?: string;
        FirstName?: string;
        LastName?: string;
        Email?: string;
        CompanyName?: string;
        Timestamp?: string;
        DemandayTeleMarketingEnquiryId?: number;
        MasterAccountAccountNumber?: string;
        TMEnquiryFirstName?: string;
        CreatedOn?: string;
        CreatedBy?: string;
        UpdatedOn?: string;
        UpdatedBy?: string;
    }

    export namespace ToolkitTMEnquiryRow {
        export const idProperty = 'SrNo';
        export const nameProperty = 'FirstName';
        export const localTextPrefix = 'Toolkit.ToolkitTMEnquiry';
        export const lookupKey = 'Toolkit.ToolkitTMEnquiry';

        export function getLookup(): Q.Lookup<ToolkitTMEnquiryRow> {
            return Q.getLookup<ToolkitTMEnquiryRow>('Toolkit.ToolkitTMEnquiry');
        }
        export const deletePermission = 'ToolkitTMEnquiry:Delete';
        export const insertPermission = 'ToolkitTMEnquiry:Insert';
        export const readPermission = 'ToolkitTMEnquiry:Read';
        export const updatePermission = 'ToolkitTMEnquiry:Update';

        export declare const enum Fields {
            SrNo = "SrNo",
            MasterAccountId = "MasterAccountId",
            CampaignId = "CampaignId",
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "Email",
            CompanyName = "CompanyName",
            Timestamp = "Timestamp",
            DemandayTeleMarketingEnquiryId = "DemandayTeleMarketingEnquiryId",
            MasterAccountAccountNumber = "MasterAccountAccountNumber",
            TMEnquiryFirstName = "TMEnquiryFirstName",
            CreatedOn = "CreatedOn",
            CreatedBy = "CreatedBy",
            UpdatedOn = "UpdatedOn",
            UpdatedBy = "UpdatedBy"
        }
    }
}
