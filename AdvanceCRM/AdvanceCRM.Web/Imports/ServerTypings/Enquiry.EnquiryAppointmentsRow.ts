namespace AdvanceCRM.Enquiry {
    export interface EnquiryAppointmentsRow {
        Id?: number;
        Details?: string;
        AppointmentDate?: string;
        Status?: Masters.StatusMaster;
        RepresentativeId?: number;
        EnquiryId?: number;
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
        RepresentativeUid?: string;
        RepresentativeNonOperational?: boolean;
        RepresentativeBranchId?: number;
        EnquiryContactsId?: number;
        EnquiryDate?: string;
        EnquiryStatus?: number;
        EnquiryType?: number;
        EnquiryAdditionalInfo?: string;
        EnquirySourceId?: number;
        EnquiryStageId?: number;
        EnquiryBranchId?: number;
        EnquiryOwnerId?: number;
        EnquiryAssignedId?: number;
        EnquiryReferenceName?: string;
        EnquiryReferencePhone?: string;
        EnquiryClosingType?: number;
        EnquiryLostReason?: string;
        EnquiryClosingDate?: string;
        EnquiryContactPersonId?: number;
        EnquiryAttachments?: string;
        ContactName?: string;
        ContactPhone?: string;
        ContactEmail?: string;
        ContactAddress?: string;
        NoteList?: Common.NoteRow[];
    }

    export namespace EnquiryAppointmentsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Details';
        export const localTextPrefix = 'Enquiry.EnquiryAppointments';
        export const deletePermission = 'Enquiry:Appointments';
        export const insertPermission = 'Enquiry:Appointments';
        export const readPermission = '?';
        export const updatePermission = 'Enquiry:Appointments';

        export declare const enum Fields {
            Id = "Id",
            Details = "Details",
            AppointmentDate = "AppointmentDate",
            Status = "Status",
            RepresentativeId = "RepresentativeId",
            EnquiryId = "EnquiryId",
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
            RepresentativeUid = "RepresentativeUid",
            RepresentativeNonOperational = "RepresentativeNonOperational",
            RepresentativeBranchId = "RepresentativeBranchId",
            EnquiryContactsId = "EnquiryContactsId",
            EnquiryDate = "EnquiryDate",
            EnquiryStatus = "EnquiryStatus",
            EnquiryType = "EnquiryType",
            EnquiryAdditionalInfo = "EnquiryAdditionalInfo",
            EnquirySourceId = "EnquirySourceId",
            EnquiryStageId = "EnquiryStageId",
            EnquiryBranchId = "EnquiryBranchId",
            EnquiryOwnerId = "EnquiryOwnerId",
            EnquiryAssignedId = "EnquiryAssignedId",
            EnquiryReferenceName = "EnquiryReferenceName",
            EnquiryReferencePhone = "EnquiryReferencePhone",
            EnquiryClosingType = "EnquiryClosingType",
            EnquiryLostReason = "EnquiryLostReason",
            EnquiryClosingDate = "EnquiryClosingDate",
            EnquiryContactPersonId = "EnquiryContactPersonId",
            EnquiryAttachments = "EnquiryAttachments",
            ContactName = "ContactName",
            ContactPhone = "ContactPhone",
            ContactEmail = "ContactEmail",
            ContactAddress = "ContactAddress",
            NoteList = "NoteList"
        }
    }
}
