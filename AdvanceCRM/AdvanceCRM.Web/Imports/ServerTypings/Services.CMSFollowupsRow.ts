namespace AdvanceCRM.Services {
    export interface CMSFollowupsRow {
        Id?: number;
        FollowupNote?: string;
        Details?: string;
        FollowupDate?: string;
        Status?: Masters.StatusMaster;
        RepresentativeId?: number;
        CMSId?: number;
        ClosingDate?: string;
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
        CMSContactsId?: number;
        CMSDate?: string;
        CMSProductsId?: number;
        CMSSerialNo?: string;
        CMSComplaintId?: number;
        CMSCategory?: number;
        CMSAmount?: number;
        CMSExpectedCompletion?: string;
        CMSAssignedBy?: number;
        CMSAssignedTo?: number;
        CMSInstructions?: string;
        CMSBranchId?: number;
        CMSStatus?: number;
        CMSCompletionDate?: string;
        CMSFeedback?: string;
        CMSAdditionalInfo?: string;
        CMSImage?: string;
        CMSPhone?: string;
        CMSAddress?: string;
        CMSStageId?: number;
        CMSPriority?: number;
        CMSAttachment?: string;
        CMSPmrClosed?: boolean;
        CMSInvestigationBy?: number;
        CMSActionBy?: number;
        CMSSupervisedBy?: number;
        CMSObservation?: string;
        CMSAction?: string;
        CMSComments?: string;
        ContactName?: string;
        ContactPhone?: string;
        ContactEmail?: string;
        ContactAddress?: string;
        ProductsName?: string;
        ComplaintType?: string;
        NoteList?: Common.NoteRow[];
    }

    export namespace CMSFollowupsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'FollowupNote';
        export const localTextPrefix = 'Services.CMSFollowups';
        export const deletePermission = 'CMS:Followups';
        export const insertPermission = 'CMS:Followups';
        export const readPermission = '?';
        export const updatePermission = 'CMS:Followups';

        export declare const enum Fields {
            Id = "Id",
            FollowupNote = "FollowupNote",
            Details = "Details",
            FollowupDate = "FollowupDate",
            Status = "Status",
            RepresentativeId = "RepresentativeId",
            CMSId = "CMSId",
            ClosingDate = "ClosingDate",
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
            CMSContactsId = "CMSContactsId",
            CMSDate = "CMSDate",
            CMSProductsId = "CMSProductsId",
            CMSSerialNo = "CMSSerialNo",
            CMSComplaintId = "CMSComplaintId",
            CMSCategory = "CMSCategory",
            CMSAmount = "CMSAmount",
            CMSExpectedCompletion = "CMSExpectedCompletion",
            CMSAssignedBy = "CMSAssignedBy",
            CMSAssignedTo = "CMSAssignedTo",
            CMSInstructions = "CMSInstructions",
            CMSBranchId = "CMSBranchId",
            CMSStatus = "CMSStatus",
            CMSCompletionDate = "CMSCompletionDate",
            CMSFeedback = "CMSFeedback",
            CMSAdditionalInfo = "CMSAdditionalInfo",
            CMSImage = "CMSImage",
            CMSPhone = "CMSPhone",
            CMSAddress = "CMSAddress",
            CMSStageId = "CMSStageId",
            CMSPriority = "CMSPriority",
            CMSAttachment = "CMSAttachment",
            CMSPmrClosed = "CMSPmrClosed",
            CMSInvestigationBy = "CMSInvestigationBy",
            CMSActionBy = "CMSActionBy",
            CMSSupervisedBy = "CMSSupervisedBy",
            CMSObservation = "CMSObservation",
            CMSAction = "CMSAction",
            CMSComments = "CMSComments",
            ContactName = "ContactName",
            ContactPhone = "ContactPhone",
            ContactEmail = "ContactEmail",
            ContactAddress = "ContactAddress",
            ProductsName = "ProductsName",
            ComplaintType = "ComplaintType",
            NoteList = "NoteList"
        }
    }
}
