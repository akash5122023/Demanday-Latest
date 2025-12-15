namespace AdvanceCRM.ThirdParty {
    export interface KnowlarityDetailsRow {
        Id?: number;
        Name?: string;
        CustomerNumber?: string;
        CompanyType?: number;
        Email?: string;
        EmployeeNumber?: string;
        Duration?: string;
        Recording?: string;
        DateTime?: string;
        IsMoved?: boolean;
        Cmiuid?: string;
        BilledSec?: string;
        Rate?: string;
        Record?: string;
        From?: string;
        To?: string;
        Type?: string;
        EmployeeName?: string;
        CallDurationState?: Modules.ThirdParty.KnowlarityDetails.IvrCallDuration;
    }

    export namespace KnowlarityDetailsRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Name';
        export const localTextPrefix = 'ThirdParty.KnowlarityDetails';
        export const deletePermission = 'KnowlarityDetails:inbox';
        export const insertPermission = 'KnowlarityDetails:inbox';
        export const readPermission = 'KnowlarityDetails:inbox';
        export const updatePermission = 'KnowlarityDetails:inbox';

        export declare const enum Fields {
            Id = "Id",
            Name = "Name",
            CustomerNumber = "CustomerNumber",
            CompanyType = "CompanyType",
            Email = "Email",
            EmployeeNumber = "EmployeeNumber",
            Duration = "Duration",
            Recording = "Recording",
            DateTime = "DateTime",
            IsMoved = "IsMoved",
            Cmiuid = "Cmiuid",
            BilledSec = "BilledSec",
            Rate = "Rate",
            Record = "Record",
            From = "From",
            To = "To",
            Type = "Type",
            EmployeeName = "EmployeeName",
            CallDurationState = "CallDurationState"
        }
    }
}
