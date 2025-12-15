namespace AdvanceCRM.Settings {
    export interface ProductPlanModuleRow {
        Id?: number;
        PlanId?: number;
        ModuleId?: number;
        PlanName?: string;
        ModuleDisplayName?: string;
        ModuleName?: string;
        ModuleIsActive?: boolean;
        ModuleSortOrder?: number;
    }

    export namespace ProductPlanModuleRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Settings.ProductPlanModule';
        export const deletePermission = 'Settings:ProductPlans';
        export const insertPermission = 'Settings:ProductPlans';
        export const readPermission = 'Settings:ProductPlans';
        export const updatePermission = 'Settings:ProductPlans';

        export declare const enum Fields {
            Id = "Id",
            PlanId = "PlanId",
            ModuleId = "ModuleId",
            PlanName = "PlanName",
            ModuleDisplayName = "ModuleDisplayName",
            ModuleName = "ModuleName",
            ModuleIsActive = "ModuleIsActive",
            ModuleSortOrder = "ModuleSortOrder"
        }
    }
}
