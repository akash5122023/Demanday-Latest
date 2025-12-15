namespace AdvanceCRM.BizMail {
    export interface BmListRow {
        Id?: number;
        ListId?: string;
        CompanyName?: string;
        Name?: string;
        City?: string;
        DisplayName?: string;
        Description?: string;
        From?: string;
        ReplyTo?: string;
    }

    export namespace BmListRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'BizMail.BmList';
        export const lookupKey = 'BizMail.BMList';

        export function getLookup(): Q.Lookup<BmListRow> {
            return Q.getLookup<BmListRow>('BizMail.BMList');
        }
        export const deletePermission = 'BizMail:Delete';
        export const insertPermission = 'BizMail:Insert';
        export const readPermission = 'BizMail:Read';
        export const updatePermission = 'BizMail:Update';

        export declare const enum Fields {
            Id = "Id",
            ListId = "ListId",
            CompanyName = "CompanyName",
            Name = "Name",
            City = "City",
            DisplayName = "DisplayName",
            Description = "Description",
            From = "From",
            ReplyTo = "ReplyTo"
        }
    }
}
