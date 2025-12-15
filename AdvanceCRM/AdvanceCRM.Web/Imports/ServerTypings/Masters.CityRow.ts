namespace AdvanceCRM.Masters {
    export interface CityRow {
        Id?: number;
        City?: string;
        StateId?: number;
        State?: string;
    }

    export namespace CityRow {
        export const idProperty = 'Id';
        export const nameProperty = 'City';
        export const localTextPrefix = 'Masters.City';
        export const lookupKey = 'Masters.City';

        export function getLookup(): Q.Lookup<CityRow> {
            return Q.getLookup<CityRow>('Masters.City');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            City = "City",
            StateId = "StateId",
            State = "State"
        }
    }
}
