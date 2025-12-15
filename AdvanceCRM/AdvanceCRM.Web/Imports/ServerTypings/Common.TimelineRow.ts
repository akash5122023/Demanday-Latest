namespace AdvanceCRM.Common {
    export interface TimelineRow {
        TimelineId?: number;
        EntityType?: string;
        EntityId?: number;
        Text?: string;
        Type?: number;
        InsertUserId?: number;
        InsertDate?: string;
        InsertUserDisplayName?: string;
    }

    export namespace TimelineRow {
        export const idProperty = 'TimelineId';
        export const nameProperty = 'EntityType';
        export const localTextPrefix = 'Common.Timeline';
        export const deletePermission = '';
        export const insertPermission = '';
        export const readPermission = '';
        export const updatePermission = '';

        export declare const enum Fields {
            TimelineId = "TimelineId",
            EntityType = "EntityType",
            EntityId = "EntityId",
            Text = "Text",
            Type = "Type",
            InsertUserId = "InsertUserId",
            InsertDate = "InsertDate",
            InsertUserDisplayName = "InsertUserDisplayName"
        }
    }
}
