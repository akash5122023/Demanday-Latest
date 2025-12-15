namespace AdvanceCRM.Masters {
    export interface AccountingHeadsRow {
        Id?: number;
        Head?: string;
        Type?: TransactionTypeMaster;
    }

    export namespace AccountingHeadsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Head';
        export const localTextPrefix = 'Masters.AccountingHeads';
        export const lookupKey = 'Masters.AccountingHeads';

        export function getLookup(): Q.Lookup<AccountingHeadsRow> {
            return Q.getLookup<AccountingHeadsRow>('Masters.AccountingHeads');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Head = "Head",
            Type = "Type"
        }
    }
}
