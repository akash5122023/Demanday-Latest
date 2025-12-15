namespace AdvanceCRM.Masters {
    export interface TehsilRow {
        Id?: number;
        Tehsil?: string;
        StateId?: number;
        CityId?: number;
        State?: string;
        City?: string;
        CityStateId?: number;
    }

    export namespace TehsilRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Tehsil';
        export const localTextPrefix = 'Masters.Tehsil';
        export const lookupKey = 'Masters.Tehsil';

        export function getLookup(): Q.Lookup<TehsilRow> {
            return Q.getLookup<TehsilRow>('Masters.Tehsil');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Tehsil = "Tehsil",
            StateId = "StateId",
            CityId = "CityId",
            State = "State",
            City = "City",
            CityStateId = "CityStateId"
        }
    }
}
