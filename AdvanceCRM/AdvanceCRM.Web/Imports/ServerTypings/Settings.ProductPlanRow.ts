namespace AdvanceCRM.Settings {
    export interface ProductPlanRow {
        Id?: number;
        Name?: string;
        PricePerUser?: number;
        TrialDays?: number;
        UserLimit?: number;
        NonOperationalUsers?: number;
        Currency?: string;
        IsActive?: boolean;
        SortOrder?: number;
        BadgeLabel?: PlanBadgeLabel;
        BadgeHighlight?: boolean;
        ModuleList?: number[];
        ModuleNames?: string[];
        FeatureList?: number[];
        FeatureNames?: string[];
        CreatedOn?: string;
        CreatedBy?: string;
        ModifiedOn?: string;
        ModifiedBy?: string;
    }

    export namespace ProductPlanRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Settings.ProductPlan';
        export const lookupKey = 'Settings.ProductPlan';

        export function getLookup(): Q.Lookup<ProductPlanRow> {
            return Q.getLookup<ProductPlanRow>('Settings.ProductPlan');
        }
        export const deletePermission = 'Settings:ProductPlans';
        export const insertPermission = 'Settings:ProductPlans';
        export const readPermission = 'Settings:ProductPlans';
        export const updatePermission = 'Settings:ProductPlans';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            PricePerUser = "PricePerUser",
            TrialDays = "TrialDays",
            UserLimit = "UserLimit",
            NonOperationalUsers = "NonOperationalUsers",
            Currency = "Currency",
            IsActive = "IsActive",
            SortOrder = "SortOrder",
            BadgeLabel = "BadgeLabel",
            BadgeHighlight = "BadgeHighlight",
            ModuleList = "ModuleList",
            ModuleNames = "ModuleNames",
            FeatureList = "FeatureList",
            FeatureNames = "FeatureNames",
            CreatedOn = "CreatedOn",
            CreatedBy = "CreatedBy",
            ModifiedOn = "ModifiedOn",
            ModifiedBy = "ModifiedBy"
        }
    }
}
