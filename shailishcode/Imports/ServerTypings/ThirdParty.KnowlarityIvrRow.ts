namespace AdvanceCRM.ThirdParty {
    export interface KnowlarityIvrRow {
        Id?: number;
        Name?: string;
        Mobile?: string;
        EmpMobile?: string;
        IvrNo?: string;
        Recording?: string;
        Date?: string;
        Duration?: string;
    }

    export namespace KnowlarityIvrRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'ThirdParty.KnowlarityIvr';
        export const deletePermission = 'KnowlarityIVR:Inbox';
        export const insertPermission = 'KnowlarityIVR:Inbox';
        export const readPermission = 'KnowlarityIVR:Inbox';
        export const updatePermission = 'KnowlarityIVR:Inbox';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            Mobile = "Mobile",
            EmpMobile = "EmpMobile",
            IvrNo = "IvrNo",
            Recording = "Recording",
            Date = "Date",
            Duration = "Duration"
        }
    }
}
