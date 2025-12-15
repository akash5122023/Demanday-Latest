namespace AdvanceCRM.Accounting {
    export interface ExpenseManagementRow {
        Id?: number;
        RepresentativeId?: number;
        HeadId?: number;
        Amount?: number;
        Attachment?: string;
        AdditionalInfo?: string;
        ApprovedBy?: number;
        Date?: string;
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
        Head?: string;
        HeadType?: number;
        ApprovedByUsername?: string;
        ApprovedByDisplayName?: string;
        ApprovedByEmail?: string;
        ApprovedBySource?: string;
        ApprovedByPasswordHash?: string;
        ApprovedByPasswordSalt?: string;
        ApprovedByLastDirectoryUpdate?: string;
        ApprovedByUserImage?: string;
        ApprovedByInsertDate?: string;
        ApprovedByInsertUserId?: number;
        ApprovedByUpdateDate?: string;
        ApprovedByUpdateUserId?: number;
        ApprovedByIsActive?: number;
        ApprovedByUpperLevel?: number;
        ApprovedByUpperLevel2?: number;
        ApprovedByUpperLevel3?: number;
        ApprovedByUpperLevel4?: number;
        ApprovedByUpperLevel5?: number;
        ApprovedByHost?: string;
        ApprovedByPort?: number;
        ApprovedBySsl?: boolean;
        ApprovedByEmailId?: string;
        ApprovedByEmailPassword?: string;
        ApprovedByPhone?: string;
        ApprovedByMcsmtpServer?: string;
        ApprovedByMcsmtpPort?: number;
        ApprovedByMcimapServer?: string;
        ApprovedByMcimapPort?: number;
        ApprovedByMcUsername?: string;
        ApprovedByMcPassword?: string;
        ApprovedByStartTime?: string;
        ApprovedByEndTime?: string;
        ApprovedByBranchId?: number;
        ApprovedByUid?: string;
        ApprovedByNonOperational?: boolean;
    }

    export namespace ExpenseManagementRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Attachment';
        export const localTextPrefix = 'Accounting.ExpenseManagement';
        export const deletePermission = 'ExpenseManagement:Delete';
        export const insertPermission = 'ExpenseManagement:Insert';
        export const readPermission = 'ExpenseManagement:Read';
        export const updatePermission = 'ExpenseManagement:Update';

        export declare const enum Fields {
            Id = "Id",
            RepresentativeId = "RepresentativeId",
            HeadId = "HeadId",
            Amount = "Amount",
            Attachment = "Attachment",
            AdditionalInfo = "AdditionalInfo",
            ApprovedBy = "ApprovedBy",
            Date = "Date",
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
            Head = "Head",
            HeadType = "HeadType",
            ApprovedByUsername = "ApprovedByUsername",
            ApprovedByDisplayName = "ApprovedByDisplayName",
            ApprovedByEmail = "ApprovedByEmail",
            ApprovedBySource = "ApprovedBySource",
            ApprovedByPasswordHash = "ApprovedByPasswordHash",
            ApprovedByPasswordSalt = "ApprovedByPasswordSalt",
            ApprovedByLastDirectoryUpdate = "ApprovedByLastDirectoryUpdate",
            ApprovedByUserImage = "ApprovedByUserImage",
            ApprovedByInsertDate = "ApprovedByInsertDate",
            ApprovedByInsertUserId = "ApprovedByInsertUserId",
            ApprovedByUpdateDate = "ApprovedByUpdateDate",
            ApprovedByUpdateUserId = "ApprovedByUpdateUserId",
            ApprovedByIsActive = "ApprovedByIsActive",
            ApprovedByUpperLevel = "ApprovedByUpperLevel",
            ApprovedByUpperLevel2 = "ApprovedByUpperLevel2",
            ApprovedByUpperLevel3 = "ApprovedByUpperLevel3",
            ApprovedByUpperLevel4 = "ApprovedByUpperLevel4",
            ApprovedByUpperLevel5 = "ApprovedByUpperLevel5",
            ApprovedByHost = "ApprovedByHost",
            ApprovedByPort = "ApprovedByPort",
            ApprovedBySsl = "ApprovedBySsl",
            ApprovedByEmailId = "ApprovedByEmailId",
            ApprovedByEmailPassword = "ApprovedByEmailPassword",
            ApprovedByPhone = "ApprovedByPhone",
            ApprovedByMcsmtpServer = "ApprovedByMcsmtpServer",
            ApprovedByMcsmtpPort = "ApprovedByMcsmtpPort",
            ApprovedByMcimapServer = "ApprovedByMcimapServer",
            ApprovedByMcimapPort = "ApprovedByMcimapPort",
            ApprovedByMcUsername = "ApprovedByMcUsername",
            ApprovedByMcPassword = "ApprovedByMcPassword",
            ApprovedByStartTime = "ApprovedByStartTime",
            ApprovedByEndTime = "ApprovedByEndTime",
            ApprovedByBranchId = "ApprovedByBranchId",
            ApprovedByUid = "ApprovedByUid",
            ApprovedByNonOperational = "ApprovedByNonOperational"
        }
    }
}
