namespace AdvanceCRM.Settings {
    export interface ProductModuleRow {
        Id?: number;
        Name?: string;
        DisplayName?: string;
        Description?: string;
        IsActive?: boolean;
        SortOrder?: number;
        Price?: number;
        Currency?: string;
        CreatedOn?: string;
        CreatedBy?: string;
        ModifiedOn?: string;
        ModifiedBy?: string;
    }

    export namespace ProductModuleRow {
        export const idProperty = 'Id';
        export const nameProperty = 'DisplayName';
        export const localTextPrefix = 'Settings.ProductModule';
        export const lookupKey = 'Settings.ProductModule';

        export function getLookup(): Q.Lookup<ProductModuleRow> {
            return Q.getLookup<ProductModuleRow>('Settings.ProductModule');
        }
        export const deletePermission = 'Settings:ProductPlans';
        export const insertPermission = 'Settings:ProductPlans';
        export const readPermission = 'Settings:ProductPlans';
        export const updatePermission = 'Settings:ProductPlans';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            DisplayName = "DisplayName",
            Description = "Description",
            IsActive = "IsActive",
            SortOrder = "SortOrder",
            Price = "Price",
            Currency = "Currency",
            CreatedOn = "CreatedOn",
            CreatedBy = "CreatedBy",
            ModifiedOn = "ModifiedOn",
            ModifiedBy = "ModifiedBy"
        }
    }
}
