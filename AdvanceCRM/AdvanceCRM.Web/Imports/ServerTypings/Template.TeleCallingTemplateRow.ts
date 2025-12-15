namespace AdvanceCRM.Template {
    export interface TeleCallingTemplateRow {
        Id?: number;
        From?: string;
        Subject?: string;
        CustomerSms?: string;
        ExecutiveSms?: string;
        CustomerEmail?: string;
        ExecutiveEmail?: string;
        CustomerReminderSMS?: string;
        ExecutiveReminderSMS?: string;
        CompanyId?: number;
        CustTemplateId?: string;
        ExeTemplateId?: string;
        CustRTemplateId?: string;
        ExeRTemplateId?: string;
        WaCustomTemplate?: string;
        WaCustomTemplateId?: string;
        WaExeTemplate?: string;
        WaExeTemplateId?: string;
        RwaCustomTemplate?: string;
        RwaCustomTemplateId?: string;
        RwaExeTemplate?: string;
        RwaExeTemplateId?: string;
        SmsReminder?: string;
        SmsrTemplateId?: string;
        WaReminder?: string;
        WarTemplateId?: string;
    }

    export namespace TeleCallingTemplateRow {
        export const idProperty = 'Id';
        export const nameProperty = 'From';
        export const localTextPrefix = 'Template.TeleCallingTemplate';
        export const lookupKey = 'Template.TeleCallingTemplate';

        export function getLookup(): Q.Lookup<TeleCallingTemplateRow> {
            return Q.getLookup<TeleCallingTemplateRow>('Template.TeleCallingTemplate');
        }
        export const deletePermission = 'Template:TeleCalling';
        export const insertPermission = 'Template:TeleCalling';
        export const readPermission = 'Template:TeleCalling';
        export const updatePermission = 'Template:TeleCalling';

        export declare const enum Fields {
            Id = "Id",
            From = "From",
            Subject = "Subject",
            CustomerSms = "CustomerSms",
            ExecutiveSms = "ExecutiveSms",
            CustomerEmail = "CustomerEmail",
            ExecutiveEmail = "ExecutiveEmail",
            CustomerReminderSMS = "CustomerReminderSMS",
            ExecutiveReminderSMS = "ExecutiveReminderSMS",
            CompanyId = "CompanyId",
            CustTemplateId = "CustTemplateId",
            ExeTemplateId = "ExeTemplateId",
            CustRTemplateId = "CustRTemplateId",
            ExeRTemplateId = "ExeRTemplateId",
            WaCustomTemplate = "WaCustomTemplate",
            WaCustomTemplateId = "WaCustomTemplateId",
            WaExeTemplate = "WaExeTemplate",
            WaExeTemplateId = "WaExeTemplateId",
            RwaCustomTemplate = "RwaCustomTemplate",
            RwaCustomTemplateId = "RwaCustomTemplateId",
            RwaExeTemplate = "RwaExeTemplate",
            RwaExeTemplateId = "RwaExeTemplateId",
            SmsReminder = "SmsReminder",
            SmsrTemplateId = "SmsrTemplateId",
            WaReminder = "WaReminder",
            WarTemplateId = "WarTemplateId"
        }
    }
}
