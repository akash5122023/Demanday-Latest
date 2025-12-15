namespace AdvanceCRM.Feedback {
    export interface FeedbackDetailsRow {
        Id?: number;
        Name?: string;
        Phone?: string;
        Service?: number;
        Refer?: boolean;
        Details?: string;
    }

    export namespace FeedbackDetailsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'Feedback.FeedbackDetails';
        export const lookupKey = 'Feedback.Feedback';

        export function getLookup(): Q.Lookup<FeedbackDetailsRow> {
            return Q.getLookup<FeedbackDetailsRow>('Feedback.Feedback');
        }
        export const deletePermission = 'Feedback:Delete';
        export const insertPermission = 'Feedback:Insert';
        export const readPermission = 'Feedback:Read';
        export const updatePermission = 'Feedback:Update';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Phone = "Phone",
            Service = "Service",
            Refer = "Refer",
            Details = "Details"
        }
    }
}
