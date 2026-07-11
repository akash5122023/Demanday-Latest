namespace AdvanceCRM.EmailVerification {
    export interface EmailVerificationQuotaRow {
        Id?: number;
        UserId?: number;
        AllowedCount?: number;
        UsedCount?: number;
    }

    export namespace EmailVerificationQuotaRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'EmailVerification.EmailVerificationQuota';
        export const deletePermission = null;
        export const insertPermission = null;
        export const readPermission = '';
        export const updatePermission = null;

        export declare const enum Fields {
            Id = "Id",
            UserId = "UserId",
            AllowedCount = "AllowedCount",
            UsedCount = "UsedCount"
        }
    }
}
