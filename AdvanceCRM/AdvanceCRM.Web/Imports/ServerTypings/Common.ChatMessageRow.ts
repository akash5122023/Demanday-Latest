namespace AdvanceCRM.Common {
    export interface ChatMessageRow {
        Id?: number;
        SenderId?: number;
        ReceiverId?: number;
        Message?: string;
        SentDate?: string;
        AttachmentPath?: string;
        AttachmentName?: string;
        AttachmentType?: string;
        IsRead?: boolean;
        IsDelivered?: boolean;
        SenderName?: string;
        ReceiverName?: string;
    }

    export namespace ChatMessageRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Common.ChatMessage';
        export const deletePermission = null;
        export const insertPermission = null;
        export const readPermission = '';
        export const updatePermission = null;

        export declare const enum Fields {
            Id = "Id",
            SenderId = "SenderId",
            ReceiverId = "ReceiverId",
            Message = "Message",
            SentDate = "SentDate",
            AttachmentPath = "AttachmentPath",
            AttachmentName = "AttachmentName",
            AttachmentType = "AttachmentType",
            IsRead = "IsRead",
            IsDelivered = "IsDelivered",
            SenderName = "SenderName",
            ReceiverName = "ReceiverName"
        }
    }
}
