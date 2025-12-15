namespace AdvanceCRM.Masters {
    export interface MessageMasterRow {
        Id?: number;
        Message?: string;
    }

    export namespace MessageMasterRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Message';
        export const localTextPrefix = 'Masters.MessageMaster';
        export const lookupKey = 'Masters.MessageMaster';

        export function getLookup(): Q.Lookup<MessageMasterRow> {
            return Q.getLookup<MessageMasterRow>('Masters.MessageMaster');
        }
        export const deletePermission = 'Masters:Modify';
        export const insertPermission = 'Masters:Modify';
        export const readPermission = 'Masters:Read';
        export const updatePermission = 'Masters:Modify';

        export declare const enum Fields {
            Id = "Id",
            Message = "Message"
        }
    }
}
