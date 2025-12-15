namespace AdvanceCRM.Settings {
    export interface CouponCodeRow {
        Id?: number;
        Code?: string;
        DiscountType?: string;
        DiscountValue?: number;
        MaxUsageCount?: number;
        UsedCount?: number;
        IsActive?: boolean;
        ExpiryDate?: string;
    }

    export namespace CouponCodeRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Code';
        export const localTextPrefix = 'Settings.CouponCode';
        export const deletePermission = 'Settings:ProductPlans';
        export const insertPermission = 'Settings:ProductPlans';
        export const readPermission = 'Settings:ProductPlans';
        export const updatePermission = 'Settings:ProductPlans';

        export declare const enum Fields {
            Id = "Id",
            Code = "Code",
            DiscountType = "DiscountType",
            DiscountValue = "DiscountValue",
            MaxUsageCount = "MaxUsageCount",
            UsedCount = "UsedCount",
            IsActive = "IsActive",
            ExpiryDate = "ExpiryDate"
        }
    }
}
