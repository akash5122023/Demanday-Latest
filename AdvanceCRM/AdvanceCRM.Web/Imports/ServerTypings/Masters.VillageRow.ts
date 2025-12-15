namespace AdvanceCRM.Masters {
    export interface VillageRow {
        Id?: number;
        Village?: string;
        StateId?: number;
        CityId?: number;
        TehsilId?: number;
        PIN?: string;
        State?: string;
        City?: string;
        CityStateId?: number;
        Tehsil?: string;
        TehsilStateId?: number;
        TehsilCityId?: number;
    }

    export namespace VillageRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Village';
        export const localTextPrefix = 'Masters.Village';
        export const lookupKey = 'Masters.Village';

        export function getLookup(): Q.Lookup<VillageRow> {
            return Q.getLookup<VillageRow>('Masters.Village');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Village = "Village",
            StateId = "StateId",
            CityId = "CityId",
            TehsilId = "TehsilId",
            PIN = "PIN",
            State = "State",
            City = "City",
            CityStateId = "CityStateId",
            Tehsil = "Tehsil",
            TehsilStateId = "TehsilStateId",
            TehsilCityId = "TehsilCityId"
        }
    }
}
