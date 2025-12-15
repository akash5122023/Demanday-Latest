namespace AdvanceCRM.Services {
    export interface TeleCallingAppointmentsRow {
        Id?: number;
        Details?: string;
        AppointmentDate?: string;
        Status?: Masters.StatusMaster;
        RepresentativeId?: number;
        TeleCallingId?: number;
        MinutesOfMeeting?: string;
        Reason?: string;
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
        TeleCallingContactsId?: number;
        TeleCallingProductsId?: number;
        TeleCallingStatus?: number;
        TeleCallingFeedback?: string;
        TeleCallingSourceId?: number;
        TeleCallingStageId?: number;
        TeleCallingBranchId?: number;
        TeleCallingRepresentative?: number;
        TeleCallingDate?: string;
        TeleCallingAppointmentDate?: string;
        TeleCallingDetails?: string;
        TeleCallingAssignedTo?: number;
        ContactName?: string;
        ContactPhone?: string;
        ContactEmail?: string;
        ContactAddress?: string;
        NoteList?: Common.NoteRow[];
    }

    export namespace TeleCallingAppointmentsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Details';
        export const localTextPrefix = 'Services.TeleCallingAppointments';
        export const deletePermission = 'TeleCalling:Appointments';
        export const insertPermission = 'TeleCalling:Appointments';
        export const readPermission = '?';
        export const updatePermission = 'TeleCalling:Appointments';

        export declare const enum Fields {
            Id = "Id",
            Details = "Details",
            AppointmentDate = "AppointmentDate",
            Status = "Status",
            RepresentativeId = "RepresentativeId",
            TeleCallingId = "TeleCallingId",
            MinutesOfMeeting = "MinutesOfMeeting",
            Reason = "Reason",
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
            TeleCallingContactsId = "TeleCallingContactsId",
            TeleCallingProductsId = "TeleCallingProductsId",
            TeleCallingStatus = "TeleCallingStatus",
            TeleCallingFeedback = "TeleCallingFeedback",
            TeleCallingSourceId = "TeleCallingSourceId",
            TeleCallingStageId = "TeleCallingStageId",
            TeleCallingBranchId = "TeleCallingBranchId",
            TeleCallingRepresentative = "TeleCallingRepresentative",
            TeleCallingDate = "TeleCallingDate",
            TeleCallingAppointmentDate = "TeleCallingAppointmentDate",
            TeleCallingDetails = "TeleCallingDetails",
            TeleCallingAssignedTo = "TeleCallingAssignedTo",
            ContactName = "ContactName",
            ContactPhone = "ContactPhone",
            ContactEmail = "ContactEmail",
            ContactAddress = "ContactAddress",
            NoteList = "NoteList"
        }
    }
}
