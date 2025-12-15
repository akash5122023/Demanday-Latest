namespace AdvanceCRM.Administration {
    export interface BranchRow {
        Id?: number;
        Branch?: string;
        Phone?: string;
        Email?: string;
        Address?: string;
        CityId?: number;
        StateId?: number;
        Pin?: string;
        Country?: Masters.CountryMaster;
        CompanyId?: number;
        City?: string;
        CityStateId?: number;
        State?: string;
    }

    export namespace BranchRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Branch';
        export const localTextPrefix = 'Administration.Branch';
        export const lookupKey = 'Administration.Branch';

        export function getLookup(): Q.Lookup<BranchRow> {
            return Q.getLookup<BranchRow>('Administration.Branch');
        }
        export const deletePermission = 'Administration:General';
        export const insertPermission = 'Administration:General';
        export const readPermission = 'Administration:General';
        export const updatePermission = 'Administration:General';

        export declare const enum Fields {
            Id = "Id",
            Branch = "Branch",
            Phone = "Phone",
            Email = "Email",
            Address = "Address",
            CityId = "CityId",
            StateId = "StateId",
            Pin = "Pin",
            Country = "Country",
            CompanyId = "CompanyId",
            City = "City",
            CityStateId = "CityStateId",
            State = "State"
        }
    }
}
