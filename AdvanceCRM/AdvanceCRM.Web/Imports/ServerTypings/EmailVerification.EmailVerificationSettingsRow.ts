namespace AdvanceCRM.EmailVerification {
    export interface EmailVerificationSettingsRow {
        Id?: number;
        ApiKey?: string;
        DefaultQuota?: number;
        UpdatedByUserId?: number;
        UpdatedDate?: string;
    }

    export namespace EmailVerificationSettingsRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'EmailVerification.EmailVerificationSettings';
        export const deletePermission = null;
        export const insertPermission = null;
        export const readPermission = '';
        export const updatePermission = null;

        export declare const enum Fields {
            Id = "Id",
            ApiKey = "ApiKey",
            DefaultQuota = "DefaultQuota",
            UpdatedByUserId = "UpdatedByUserId",
            UpdatedDate = "UpdatedDate"
        }
    }
}
