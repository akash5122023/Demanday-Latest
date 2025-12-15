namespace AdvanceCRM.Administration {
    export interface TabGenderRow {
        Id?: number;
        Name?: string;
        Creation?: string;
        Modified?: string;
        ModifiedBy?: string;
        Owner?: string;
        Docstatus?: number;
        Parent?: string;
        Parentfield?: string;
        Parenttype?: string;
        Idx?: number;
        Gender?: string;
    }

    export namespace TabGenderRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Gender';
        export const localTextPrefix = 'Administration.TabGender';
        export const lookupKey = 'Administration.TabGender';

        export function getLookup(): Q.Lookup<TabGenderRow> {
            return Q.getLookup<TabGenderRow>('Administration.TabGender');
        }
        export const deletePermission = 'Administration:General';
        export const insertPermission = 'Administration:General';
        export const readPermission = 'Administration:General';
        export const updatePermission = 'Administration:General';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Creation = "Creation",
            Modified = "Modified",
            ModifiedBy = "ModifiedBy",
            Owner = "Owner",
            Docstatus = "Docstatus",
            Parent = "Parent",
            Parentfield = "Parentfield",
            Parenttype = "Parenttype",
            Idx = "Idx",
            Gender = "Gender"
        }
    }
}
