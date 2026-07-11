namespace AdvanceCRM.EmailVerification {
    export interface EmailVerificationResultRow {
        Id?: number;
        Email?: string;
        Status?: string;
        SubStatus?: string;
        Message?: string;
        VerifiedByUserId?: number;
        VerifiedDate?: string;
    }

    export namespace EmailVerificationResultRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'EmailVerification.EmailVerificationResult';
        export const deletePermission = null;
        export const insertPermission = null;
        export const readPermission = '';
        export const updatePermission = null;

        export declare const enum Fields {
            Id = "Id",
            Email = "Email",
            Status = "Status",
            SubStatus = "SubStatus",
            Message = "Message",
            VerifiedByUserId = "VerifiedByUserId",
            VerifiedDate = "VerifiedDate"
        }
    }
}
