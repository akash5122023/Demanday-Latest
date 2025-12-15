namespace AdvanceCRM.Template {
    export interface TeleCallingTemplateForm {
        From: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        CustomerEmail: Serenity.HtmlContentEditor;
        ExecutiveEmail: Serenity.HtmlContentEditor;
        CustomerSms: Serenity.TextAreaEditor;
        CustTemplateId: Serenity.StringEditor;
        ExecutiveSms: Serenity.TextAreaEditor;
        ExeTemplateId: Serenity.StringEditor;
        CustomerReminderSMS: Serenity.TextAreaEditor;
        CustRTemplateId: Serenity.StringEditor;
        ExecutiveReminderSMS: Serenity.TextAreaEditor;
        ExeRTemplateId: Serenity.StringEditor;
        SmsReminder: Serenity.TextAreaEditor;
        SmsrTemplateId: Serenity.StringEditor;
        WaCustomTemplate: Serenity.TextAreaEditor;
        WaCustomTemplateId: Serenity.StringEditor;
        WaExeTemplate: Serenity.TextAreaEditor;
        WaExeTemplateId: Serenity.StringEditor;
        RwaCustomTemplate: Serenity.TextAreaEditor;
        RwaCustomTemplateId: Serenity.StringEditor;
        RwaExeTemplate: Serenity.TextAreaEditor;
        RwaExeTemplateId: Serenity.StringEditor;
        WaReminder: Serenity.TextAreaEditor;
        WarTemplateId: Serenity.StringEditor;
    }

    export class TeleCallingTemplateForm extends Serenity.PrefixedContext {
        static formKey = 'Template.TeleCallingTemplate';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TeleCallingTemplateForm.init)  {
                TeleCallingTemplateForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.HtmlContentEditor;
                var w2 = s.TextAreaEditor;

                Q.initFormType(TeleCallingTemplateForm, [
                    'From', w0,
                    'Subject', w0,
                    'CustomerEmail', w1,
                    'ExecutiveEmail', w1,
                    'CustomerSms', w2,
                    'CustTemplateId', w0,
                    'ExecutiveSms', w2,
                    'ExeTemplateId', w0,
                    'CustomerReminderSMS', w2,
                    'CustRTemplateId', w0,
                    'ExecutiveReminderSMS', w2,
                    'ExeRTemplateId', w0,
                    'SmsReminder', w2,
                    'SmsrTemplateId', w0,
                    'WaCustomTemplate', w2,
                    'WaCustomTemplateId', w0,
                    'WaExeTemplate', w2,
                    'WaExeTemplateId', w0,
                    'RwaCustomTemplate', w2,
                    'RwaCustomTemplateId', w0,
                    'RwaExeTemplate', w2,
                    'RwaExeTemplateId', w0,
                    'WaReminder', w2,
                    'WarTemplateId', w0
                ]);
            }
        }
    }
}
