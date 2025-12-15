namespace AdvanceCRM.Products {
    export interface StockTransferRow {
        Id?: number;
        Date?: string;
        FromBranchId?: number;
        ToBranchId?: number;
        TotalQty?: number;
        Amount?: number;
        AdditionalInfo?: string;
        RepresentativeId?: number;
        CompanyId?: number;
        FromBranchBranch?: string;
        FromBranchPhone?: string;
        FromBranchEmail?: string;
        FromBranchAddress?: string;
        FromBranchCityId?: number;
        FromBranchStateId?: number;
        FromBranchPin?: string;
        FromBranchCountry?: number;
        ToBranchBranch?: string;
        ToBranchPhone?: string;
        ToBranchEmail?: string;
        ToBranchAddress?: string;
        ToBranchCityId?: number;
        ToBranchStateId?: number;
        ToBranchPin?: string;
        ToBranchCountry?: number;
        RepresentativeUsername?: string;
        RepresentativeDisplayName?: string;
        RepresentativeEmail?: string;
        RepresentativeSource?: string;
        RepresentativePasswordHash?: string;
        RepresentativePasswordSalt?: string;
        RepresentativeLastDirectoryUpdate?: string;
        RepresentativeUserImage?: string;
        RepresentativeInsertDate?: string;
        RepresentativeInsertUserId?: number;
        RepresentativeUpdateDate?: string;
        RepresentativeUpdateUserId?: number;
        RepresentativeIsActive?: number;
        RepresentativeUpperLevel?: number;
        RepresentativeUpperLevel2?: number;
        RepresentativeUpperLevel3?: number;
        RepresentativeUpperLevel4?: number;
        RepresentativeUpperLevel5?: number;
        RepresentativeHost?: string;
        RepresentativePort?: number;
        RepresentativeSsl?: boolean;
        RepresentativeEmailId?: string;
        RepresentativeEmailPassword?: string;
        RepresentativePhone?: string;
        RepresentativeMcsmtpServer?: string;
        RepresentativeMcsmtpPort?: number;
        RepresentativeMcimapServer?: string;
        RepresentativeMcimapPort?: number;
        RepresentativeMcUsername?: string;
        RepresentativeMcPassword?: string;
        RepresentativeStartTime?: string;
        RepresentativeEndTime?: string;
        RepresentativeBranchId?: number;
        RepresentativeUid?: string;
        RepresentativeNonOperational?: boolean;
        Products?: StockTransferProductsRow[];
    }

    export namespace StockTransferRow {
        export const idProperty = 'Id';
        export const nameProperty = 'AdditionalInfo';
        export const localTextPrefix = 'Products.StockTransfer';
        export const lookupKey = 'Products.StockTransfer';

        export function getLookup(): Q.Lookup<StockTransferRow> {
            return Q.getLookup<StockTransferRow>('Products.StockTransfer');
        }
        export const deletePermission = 'StockTransfer:Modify';
        export const insertPermission = 'StockTransfer:Modify';
        export const readPermission = 'StockTransfer:Read';
        export const updatePermission = 'StockTransfer:Modify';

        export declare const enum Fields {
            Id = "Id",
            Date = "Date",
            FromBranchId = "FromBranchId",
            ToBranchId = "ToBranchId",
            TotalQty = "TotalQty",
            Amount = "Amount",
            AdditionalInfo = "AdditionalInfo",
            RepresentativeId = "RepresentativeId",
            CompanyId = "CompanyId",
            FromBranchBranch = "FromBranchBranch",
            FromBranchPhone = "FromBranchPhone",
            FromBranchEmail = "FromBranchEmail",
            FromBranchAddress = "FromBranchAddress",
            FromBranchCityId = "FromBranchCityId",
            FromBranchStateId = "FromBranchStateId",
            FromBranchPin = "FromBranchPin",
            FromBranchCountry = "FromBranchCountry",
            ToBranchBranch = "ToBranchBranch",
            ToBranchPhone = "ToBranchPhone",
            ToBranchEmail = "ToBranchEmail",
            ToBranchAddress = "ToBranchAddress",
            ToBranchCityId = "ToBranchCityId",
            ToBranchStateId = "ToBranchStateId",
            ToBranchPin = "ToBranchPin",
            ToBranchCountry = "ToBranchCountry",
            RepresentativeUsername = "RepresentativeUsername",
            RepresentativeDisplayName = "RepresentativeDisplayName",
            RepresentativeEmail = "RepresentativeEmail",
            RepresentativeSource = "RepresentativeSource",
            RepresentativePasswordHash = "RepresentativePasswordHash",
            RepresentativePasswordSalt = "RepresentativePasswordSalt",
            RepresentativeLastDirectoryUpdate = "RepresentativeLastDirectoryUpdate",
            RepresentativeUserImage = "RepresentativeUserImage",
            RepresentativeInsertDate = "RepresentativeInsertDate",
            RepresentativeInsertUserId = "RepresentativeInsertUserId",
            RepresentativeUpdateDate = "RepresentativeUpdateDate",
            RepresentativeUpdateUserId = "RepresentativeUpdateUserId",
            RepresentativeIsActive = "RepresentativeIsActive",
            RepresentativeUpperLevel = "RepresentativeUpperLevel",
            RepresentativeUpperLevel2 = "RepresentativeUpperLevel2",
            RepresentativeUpperLevel3 = "RepresentativeUpperLevel3",
            RepresentativeUpperLevel4 = "RepresentativeUpperLevel4",
            RepresentativeUpperLevel5 = "RepresentativeUpperLevel5",
            RepresentativeHost = "RepresentativeHost",
            RepresentativePort = "RepresentativePort",
            RepresentativeSsl = "RepresentativeSsl",
            RepresentativeEmailId = "RepresentativeEmailId",
            RepresentativeEmailPassword = "RepresentativeEmailPassword",
            RepresentativePhone = "RepresentativePhone",
            RepresentativeMcsmtpServer = "RepresentativeMcsmtpServer",
            RepresentativeMcsmtpPort = "RepresentativeMcsmtpPort",
            RepresentativeMcimapServer = "RepresentativeMcimapServer",
            RepresentativeMcimapPort = "RepresentativeMcimapPort",
            RepresentativeMcUsername = "RepresentativeMcUsername",
            RepresentativeMcPassword = "RepresentativeMcPassword",
            RepresentativeStartTime = "RepresentativeStartTime",
            RepresentativeEndTime = "RepresentativeEndTime",
            RepresentativeBranchId = "RepresentativeBranchId",
            RepresentativeUid = "RepresentativeUid",
            RepresentativeNonOperational = "RepresentativeNonOperational",
            Products = "Products"
        }
    }
}
