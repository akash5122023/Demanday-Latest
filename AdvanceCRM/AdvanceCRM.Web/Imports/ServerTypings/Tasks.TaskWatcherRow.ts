namespace AdvanceCRM.Tasks {
    export interface TaskWatcherRow {
        Id?: number;
        AssignedId?: number;
        TasksId?: number;
        AssignedUsername?: string;
        AssignedDisplayName?: string;
        AssignedEmail?: string;
        AssignedSource?: string;
        AssignedPasswordHash?: string;
        AssignedPasswordSalt?: string;
        AssignedLastDirectoryUpdate?: string;
        AssignedUserImage?: string;
        AssignedInsertDate?: string;
        AssignedInsertUserId?: number;
        AssignedUpdateDate?: string;
        AssignedUpdateUserId?: number;
        AssignedIsActive?: number;
        AssignedUpperLevel?: number;
        AssignedUpperLevel2?: number;
        AssignedUpperLevel3?: number;
        AssignedUpperLevel4?: number;
        AssignedUpperLevel5?: number;
        AssignedHost?: string;
        AssignedPort?: number;
        AssignedSsl?: boolean;
        AssignedEmailId?: string;
        AssignedEmailPassword?: string;
        AssignedPhone?: string;
        AssignedMcsmtpServer?: string;
        AssignedMcsmtpPort?: number;
        AssignedMcimapServer?: string;
        AssignedMcimapPort?: number;
        AssignedMcUsername?: string;
        AssignedMcPassword?: string;
        AssignedStartTime?: string;
        AssignedEndTime?: string;
        AssignedBranchId?: number;
        AssignedUid?: string;
        AssignedNonOperational?: boolean;
        TasksTask?: string;
        TasksDetails?: string;
        TasksCreationDate?: string;
        TasksExpectedCompletion?: string;
        TasksAssignedBy?: number;
        TasksAssignedTo?: number;
        TasksStatusId?: number;
        TasksCompletionDate?: string;
        TasksTypeId?: number;
        TasksPhone?: string;
        TasksAttachments?: string;
        TasksPriority?: number;
        TasksResolution?: string;
        TasksContactsId?: number;
        TasksProductId?: number;
    }

    export namespace TaskWatcherRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Tasks.TaskWatcher';
        export const deletePermission = 'Tasks:Modify';
        export const insertPermission = 'Tasks:Modify';
        export const readPermission = 'Tasks:Read';
        export const updatePermission = 'Tasks:Modify';

        export declare const enum Fields {
            Id = "Id",
            AssignedId = "AssignedId",
            TasksId = "TasksId",
            AssignedUsername = "AssignedUsername",
            AssignedDisplayName = "AssignedDisplayName",
            AssignedEmail = "AssignedEmail",
            AssignedSource = "AssignedSource",
            AssignedPasswordHash = "AssignedPasswordHash",
            AssignedPasswordSalt = "AssignedPasswordSalt",
            AssignedLastDirectoryUpdate = "AssignedLastDirectoryUpdate",
            AssignedUserImage = "AssignedUserImage",
            AssignedInsertDate = "AssignedInsertDate",
            AssignedInsertUserId = "AssignedInsertUserId",
            AssignedUpdateDate = "AssignedUpdateDate",
            AssignedUpdateUserId = "AssignedUpdateUserId",
            AssignedIsActive = "AssignedIsActive",
            AssignedUpperLevel = "AssignedUpperLevel",
            AssignedUpperLevel2 = "AssignedUpperLevel2",
            AssignedUpperLevel3 = "AssignedUpperLevel3",
            AssignedUpperLevel4 = "AssignedUpperLevel4",
            AssignedUpperLevel5 = "AssignedUpperLevel5",
            AssignedHost = "AssignedHost",
            AssignedPort = "AssignedPort",
            AssignedSsl = "AssignedSsl",
            AssignedEmailId = "AssignedEmailId",
            AssignedEmailPassword = "AssignedEmailPassword",
            AssignedPhone = "AssignedPhone",
            AssignedMcsmtpServer = "AssignedMcsmtpServer",
            AssignedMcsmtpPort = "AssignedMcsmtpPort",
            AssignedMcimapServer = "AssignedMcimapServer",
            AssignedMcimapPort = "AssignedMcimapPort",
            AssignedMcUsername = "AssignedMcUsername",
            AssignedMcPassword = "AssignedMcPassword",
            AssignedStartTime = "AssignedStartTime",
            AssignedEndTime = "AssignedEndTime",
            AssignedBranchId = "AssignedBranchId",
            AssignedUid = "AssignedUid",
            AssignedNonOperational = "AssignedNonOperational",
            TasksTask = "TasksTask",
            TasksDetails = "TasksDetails",
            TasksCreationDate = "TasksCreationDate",
            TasksExpectedCompletion = "TasksExpectedCompletion",
            TasksAssignedBy = "TasksAssignedBy",
            TasksAssignedTo = "TasksAssignedTo",
            TasksStatusId = "TasksStatusId",
            TasksCompletionDate = "TasksCompletionDate",
            TasksTypeId = "TasksTypeId",
            TasksPhone = "TasksPhone",
            TasksAttachments = "TasksAttachments",
            TasksPriority = "TasksPriority",
            TasksResolution = "TasksResolution",
            TasksContactsId = "TasksContactsId",
            TasksProductId = "TasksProductId"
        }
    }
}
