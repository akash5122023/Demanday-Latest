namespace AdvanceCRM.Masters {
    export interface DealerRow {
        Id?: number;
        DealerName?: string;
        Phone?: string;
        Email?: string;
        Address?: string;
        CityId?: number;
        StateId?: number;
        Pin?: string;
        Country?: CountryMaster;
        AdditionalInfo?: string;
        City?: string;
        CityStateId?: number;
        State?: string;
    }

    export namespace DealerRow {
        export const idProperty = 'Id';
        export const nameProperty = 'DealerName';
        export const localTextPrefix = 'Masters.Dealer';
        export const lookupKey = 'Masters.Dealer';

        export function getLookup(): Q.Lookup<DealerRow> {
            return Q.getLookup<DealerRow>('Masters.Dealer');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            DealerName = "DealerName",
            Phone = "Phone",
            Email = "Email",
            Address = "Address",
            CityId = "CityId",
            StateId = "StateId",
            Pin = "Pin",
            Country = "Country",
            AdditionalInfo = "AdditionalInfo",
            City = "City",
            CityStateId = "CityStateId",
            State = "State"
        }
    }
}
