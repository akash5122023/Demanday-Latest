namespace AdvanceCRM.Attendance {
    export interface AttendanceRow {
        Id?: number;
        Name?: number;
        DateNTime?: string;
        Type?: Masters.AttendanceTypeMaster;
        Coordinates?: string;
        Location?: string;
        ApprovedBy?: number;
        PunchIn?: string;
        PunchOut?: string;
        Distance?: number;
        UserId?: string;
        NameUsername?: string;
        NameDisplayName?: string;
        NameEmail?: string;
        NameSource?: string;
        NamePasswordHash?: string;
        NamePasswordSalt?: string;
        NameLastDirectoryUpdate?: string;
        NameUserImage?: string;
        NameInsertDate?: string;
        NameInsertUserId?: number;
        NameUpdateDate?: string;
        NameUpdateUserId?: number;
        NameIsActive?: number;
        NameUpperLevel?: number;
        NameUpperLevel2?: number;
        NameUpperLevel3?: number;
        NameUpperLevel4?: number;
        NameUpperLevel5?: number;
        NameHost?: string;
        NamePort?: number;
        NameSsl?: boolean;
        NameEmailId?: string;
        NameEmailPassword?: string;
        NamePhone?: string;
        NameMcsmtpServer?: string;
        NameMcsmtpPort?: number;
        NameMcimapServer?: string;
        NameMcimapPort?: number;
        NameMcUsername?: string;
        NameMcPassword?: string;
        NameStartTime?: string;
        NameEndTime?: string;
        NameBranchId?: number;
        NameUid?: string;
        NameNonOperational?: boolean;
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

    export namespace AttendanceRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Coordinates';
        export const localTextPrefix = 'Attendance.Attendance';
        export const deletePermission = 'Attendance:Manual Entry';
        export const insertPermission = 'Attendance:Manual Entry';
        export const readPermission = 'Attendance:Read';
        export const updatePermission = 'Attendance:Manual Entry';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            DateNTime = "DateNTime",
            Type = "Type",
            Coordinates = "Coordinates",
            Location = "Location",
            ApprovedBy = "ApprovedBy",
            PunchIn = "PunchIn",
            PunchOut = "PunchOut",
            Distance = "Distance",
            UserId = "UserId",
            NameUsername = "NameUsername",
            NameDisplayName = "NameDisplayName",
            NameEmail = "NameEmail",
            NameSource = "NameSource",
            NamePasswordHash = "NamePasswordHash",
            NamePasswordSalt = "NamePasswordSalt",
            NameLastDirectoryUpdate = "NameLastDirectoryUpdate",
            NameUserImage = "NameUserImage",
            NameInsertDate = "NameInsertDate",
            NameInsertUserId = "NameInsertUserId",
            NameUpdateDate = "NameUpdateDate",
            NameUpdateUserId = "NameUpdateUserId",
            NameIsActive = "NameIsActive",
            NameUpperLevel = "NameUpperLevel",
            NameUpperLevel2 = "NameUpperLevel2",
            NameUpperLevel3 = "NameUpperLevel3",
            NameUpperLevel4 = "NameUpperLevel4",
            NameUpperLevel5 = "NameUpperLevel5",
            NameHost = "NameHost",
            NamePort = "NamePort",
            NameSsl = "NameSsl",
            NameEmailId = "NameEmailId",
            NameEmailPassword = "NameEmailPassword",
            NamePhone = "NamePhone",
            NameMcsmtpServer = "NameMcsmtpServer",
            NameMcsmtpPort = "NameMcsmtpPort",
            NameMcimapServer = "NameMcimapServer",
            NameMcimapPort = "NameMcimapPort",
            NameMcUsername = "NameMcUsername",
            NameMcPassword = "NameMcPassword",
            NameStartTime = "NameStartTime",
            NameEndTime = "NameEndTime",
            NameBranchId = "NameBranchId",
            NameUid = "NameUid",
            NameNonOperational = "NameNonOperational",
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
