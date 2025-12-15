namespace AdvanceCRM.Masters {
    export interface AdditionalInfoRow {
        Id?: number;
        AdditionalInfo?: string;
        Type?: AddInfoTypeMaster;
    }

    export namespace AdditionalInfoRow {
        export const idProperty = 'Id';
        export const nameProperty = 'AdditionalInfo';
        export const localTextPrefix = 'Masters.AdditionalInfo';
        export const lookupKey = 'Masters.AdditionalInfo';

        export function getLookup(): Q.Lookup<AdditionalInfoRow> {
            return Q.getLookup<AdditionalInfoRow>('Masters.AdditionalInfo');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            AdditionalInfo = "AdditionalInfo",
            Type = "Type"
        }
    }
}
