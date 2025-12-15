namespace AdvanceCRM.Services {
    export interface AMCTermsRow {
        Id?: number;
        TermsId?: number;
        AMCId?: number;
        Terms?: string;
        AMCDate?: string;
        AMCContactsId?: number;
        AMCStatus?: number;
        AMCStartDate?: string;
        AMCEndDate?: string;
        AMCAdditionalInfo?: string;
        AMCOwnerId?: number;
        AMCAssignedId?: number;
        AMCAttachment?: string;
        AMCLines?: number;
        AMCTerms?: string;
    }

    export namespace AMCTermsRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Services.AMCTerms';
        export const deletePermission = 'AMC:Delete';
        export const insertPermission = 'AMC:Insert';
        export const readPermission = 'AMC:Read';
        export const updatePermission = 'AMC:Update';

        export declare const enum Fields {
            Id = "Id",
            TermsId = "TermsId",
            AMCId = "AMCId",
            Terms = "Terms",
            AMCDate = "AMCDate",
            AMCContactsId = "AMCContactsId",
            AMCStatus = "AMCStatus",
            AMCStartDate = "AMCStartDate",
            AMCEndDate = "AMCEndDate",
            AMCAdditionalInfo = "AMCAdditionalInfo",
            AMCOwnerId = "AMCOwnerId",
            AMCAssignedId = "AMCAssignedId",
            AMCAttachment = "AMCAttachment",
            AMCLines = "AMCLines",
            AMCTerms = "AMCTerms"
        }
    }
}
