namespace AdvanceCRM.Masters {
    export interface StateRow {
        Id?: number;
        State?: string;
    }

    export namespace StateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'State';
        export const localTextPrefix = 'Masters.State';
        export const lookupKey = 'Masters.State';

        export function getLookup(): Q.Lookup<StateRow> {
            return Q.getLookup<StateRow>('Masters.State');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            State = "State"
        }
    }
}
