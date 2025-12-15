namespace AdvanceCRM.Masters {
    export interface BankMasterRow {
        Id?: number;
        BankName?: string;
        AccountNumber?: string;
        IFSC?: string;
        Type?: string;
        Branch?: string;
        AdditionalInfo?: string;
    }

    export namespace BankMasterRow {
        export const idProperty = 'Id';
        export const nameProperty = 'BankName';
        export const localTextPrefix = 'Masters.BankMaster';
        export const lookupKey = 'Masters.BankMaster';

        export function getLookup(): Q.Lookup<BankMasterRow> {
            return Q.getLookup<BankMasterRow>('Masters.BankMaster');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            BankName = "BankName",
            AccountNumber = "AccountNumber",
            IFSC = "IFSC",
            Type = "Type",
            Branch = "Branch",
            AdditionalInfo = "AdditionalInfo"
        }
    }
}
