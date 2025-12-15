namespace AdvanceCRM.Settings {
    export interface ProductPlanDefaultFeatureRow {
        Id?: number;
        PlanId?: number;
        FeatureId?: number;
        PlanName?: string;
        FeatureName?: string;
    }

    export namespace ProductPlanDefaultFeatureRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Settings.ProductPlanDefaultFeature';
        export const deletePermission = 'Settings:ProductPlans';
        export const insertPermission = 'Settings:ProductPlans';
        export const readPermission = 'Settings:ProductPlans';
        export const updatePermission = 'Settings:ProductPlans';

        export declare const enum Fields {
            Id = "Id",
            PlanId = "PlanId",
            FeatureId = "FeatureId",
            PlanName = "PlanName",
            FeatureName = "FeatureName"
        }
    }
}
