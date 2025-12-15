namespace AdvanceCRM.Membership {
    export interface TenantLicenseStatusResponse extends Serenity.ServiceResponse {
        HasLicense?: boolean;
        Plan?: string;
        LicenseStartDate?: string;
        LicenseEndDate?: string;
        TotalDays?: number;
        RemainingDays?: number;
        IsExpired?: boolean;
        IsTrial?: boolean;
    }
}
