namespace AdvanceCRM.Services {
    export interface AMCVisitPlannerRow {
        Id?: number;
        VisitDate?: string;
        AssignedTo?: number;
        Status?: Masters.StatusMaster;
        CompletionDate?: string;
        VisitDetails?: string;
        AMCId?: number;
        Serial?: string;
        Attachment?: string;
        RepresentativeId?: number;
        AssignedToUsername?: string;
        AssignedToDisplayName?: string;
        AssignedToEmail?: string;
        AssignedToSource?: string;
        AssignedToPasswordHash?: string;
        AssignedToPasswordSalt?: string;
        AssignedToLastDirectoryUpdate?: string;
        AssignedToUserImage?: string;
        AssignedToInsertDate?: string;
        AssignedToInsertUserId?: number;
        AssignedToUpdateDate?: string;
        AssignedToUpdateUserId?: number;
        AssignedToIsActive?: number;
        AssignedToUpperLevel?: number;
        AssignedToUpperLevel2?: number;
        AssignedToUpperLevel3?: number;
        AssignedToUpperLevel4?: number;
        AssignedToUpperLevel5?: number;
        AssignedToHost?: string;
        AssignedToPort?: number;
        AssignedToSsl?: boolean;
        AssignedToEmailId?: string;
        AssignedToEmailPassword?: string;
        AssignedToPhone?: string;
        AssignedToMcsmtpServer?: string;
        AssignedToMcsmtpPort?: number;
        AssignedToMcimapServer?: string;
        AssignedToMcimapPort?: number;
        AssignedToMcUsername?: string;
        AssignedToMcPassword?: string;
        AssignedToStartTime?: string;
        AssignedToEndTime?: string;
        AssignedToBranchId?: number;
        AssignedToUid?: string;
        AssignedToNonOperational?: boolean;
        AMCDate?: string;
        AMCContactsId?: number;
        AMCStatus?: number;
        AMCStartDate?: string;
        AMCEndDate?: string;
        AMCAdditionalInfo?: string;
        AMCOwnerId?: number;
        AMCAssignedId?: number;
        AMCAttachment?: string;
        AMCLines?: number;
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
        ContactName?: string;
        ContactPhone?: string;
        ContactEmail?: string;
        ContactAddress?: string;
        NoteList?: Common.NoteRow[];
    }

    export namespace AMCVisitPlannerRow {
        export const idProperty = 'Id';
        export const nameProperty = 'VisitDetails';
        export const localTextPrefix = 'Services.AMCVisitPlanner';
        export const deletePermission = 'AMC:Visits';
        export const insertPermission = 'AMC:Visits';
        export const readPermission = '?';
        export const updatePermission = 'AMC:Visits';

        export declare const enum Fields {
            Id = "Id",
            VisitDate = "VisitDate",
            AssignedTo = "AssignedTo",
            Status = "Status",
            CompletionDate = "CompletionDate",
            VisitDetails = "VisitDetails",
            AMCId = "AMCId",
            Serial = "Serial",
            Attachment = "Attachment",
            RepresentativeId = "RepresentativeId",
            AssignedToUsername = "AssignedToUsername",
            AssignedToDisplayName = "AssignedToDisplayName",
            AssignedToEmail = "AssignedToEmail",
            AssignedToSource = "AssignedToSource",
            AssignedToPasswordHash = "AssignedToPasswordHash",
            AssignedToPasswordSalt = "AssignedToPasswordSalt",
            AssignedToLastDirectoryUpdate = "AssignedToLastDirectoryUpdate",
            AssignedToUserImage = "AssignedToUserImage",
            AssignedToInsertDate = "AssignedToInsertDate",
            AssignedToInsertUserId = "AssignedToInsertUserId",
            AssignedToUpdateDate = "AssignedToUpdateDate",
            AssignedToUpdateUserId = "AssignedToUpdateUserId",
            AssignedToIsActive = "AssignedToIsActive",
            AssignedToUpperLevel = "AssignedToUpperLevel",
            AssignedToUpperLevel2 = "AssignedToUpperLevel2",
            AssignedToUpperLevel3 = "AssignedToUpperLevel3",
            AssignedToUpperLevel4 = "AssignedToUpperLevel4",
            AssignedToUpperLevel5 = "AssignedToUpperLevel5",
            AssignedToHost = "AssignedToHost",
            AssignedToPort = "AssignedToPort",
            AssignedToSsl = "AssignedToSsl",
            AssignedToEmailId = "AssignedToEmailId",
            AssignedToEmailPassword = "AssignedToEmailPassword",
            AssignedToPhone = "AssignedToPhone",
            AssignedToMcsmtpServer = "AssignedToMcsmtpServer",
            AssignedToMcsmtpPort = "AssignedToMcsmtpPort",
            AssignedToMcimapServer = "AssignedToMcimapServer",
            AssignedToMcimapPort = "AssignedToMcimapPort",
            AssignedToMcUsername = "AssignedToMcUsername",
            AssignedToMcPassword = "AssignedToMcPassword",
            AssignedToStartTime = "AssignedToStartTime",
            AssignedToEndTime = "AssignedToEndTime",
            AssignedToBranchId = "AssignedToBranchId",
            AssignedToUid = "AssignedToUid",
            AssignedToNonOperational = "AssignedToNonOperational",
            AMCDate = "AMCDate",
            AMCContactsId = "AMCContactsId",
            AMCStatus = "AMCStatus",
            AMCStartDate = "AMCStartDate",
            AMCEndDate = "AMCEndDate",
            AMCAdditionalInfo = "AMCAdditionalInfo",
            AMCOwnerId = "AMCOwnerId",
            AMCAssignedId = "AMCAssignedId",
            AMCAttachment = "AMCAttachment",
            AMCLines = "AMCLines",
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
            ContactName = "ContactName",
            ContactPhone = "ContactPhone",
            ContactEmail = "ContactEmail",
            ContactAddress = "ContactAddress",
            NoteList = "NoteList"
        }
    }
}
