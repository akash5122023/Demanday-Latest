namespace AdvanceCRM.Template {
    export interface CmsTemplateForm {
        Sender: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        EmailTemplate: Serenity.HtmlContentEditor;
        EmailTemplateReceipt: Serenity.HtmlContentEditor;
        ClosedEmailTemplate: Serenity.HtmlContentEditor;
        EngineerEmailTemplate: Serenity.HtmlContentEditor;
        SMSTemplate: Serenity.TextAreaEditor;
        SmsTemplateId: Serenity.StringEditor;
        ClosedSMSTemplate: Serenity.TextAreaEditor;
        ClosedTemplateId: Serenity.StringEditor;
        EngineerSMSTemplate: Serenity.TextAreaEditor;
        EmgineerTemplateId: Serenity.StringEditor;
        SmsReminder: Serenity.TextAreaEditor;
        SmsrTemplateId: Serenity.StringEditor;
        WaTemplate: Serenity.TextAreaEditor;
        WaTemplateId: Serenity.StringEditor;
        WaClosedTemplate: Serenity.TextAreaEditor;
        WaClosedTemplateId: Serenity.StringEditor;
        WaengTemplate: Serenity.TextAreaEditor;
        WaengTemplateId: Serenity.StringEditor;
        WaReminder: Serenity.TextAreaEditor;
        WarTemplateId: Serenity.StringEditor;
        CCEmails: Serenity.TextAreaEditor;
        BCCEmails: Serenity.TextAreaEditor;
        CcsmSs: Serenity.TextAreaEditor;
        Host: Serenity.StringEditor;
        Port: Serenity.IntegerEditor;
        SSL: Serenity.BooleanEditor;
        EmailId: Serenity.EmailEditor;
        EmailPassword: Serenity.PasswordEditor;
    }

    export class CmsTemplateForm extends Serenity.PrefixedContext {
        static formKey = 'Template.CmsTemplate';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CmsTemplateForm.init)  {
                CmsTemplateForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.HtmlContentEditor;
                var w2 = s.TextAreaEditor;
                var w3 = s.IntegerEditor;
                var w4 = s.BooleanEditor;
                var w5 = s.EmailEditor;
                var w6 = s.PasswordEditor;

                Q.initFormType(CmsTemplateForm, [
                    'Sender', w0,
                    'Subject', w0,
                    'EmailTemplate', w1,
                    'EmailTemplateReceipt', w1,
                    'ClosedEmailTemplate', w1,
                    'EngineerEmailTemplate', w1,
                    'SMSTemplate', w2,
                    'SmsTemplateId', w0,
                    'ClosedSMSTemplate', w2,
                    'ClosedTemplateId', w0,
                    'EngineerSMSTemplate', w2,
                    'EmgineerTemplateId', w0,
                    'SmsReminder', w2,
                    'SmsrTemplateId', w0,
                    'WaTemplate', w2,
                    'WaTemplateId', w0,
                    'WaClosedTemplate', w2,
                    'WaClosedTemplateId', w0,
                    'WaengTemplate', w2,
                    'WaengTemplateId', w0,
                    'WaReminder', w2,
                    'WarTemplateId', w0,
                    'CCEmails', w2,
                    'BCCEmails', w2,
                    'CcsmSs', w2,
                    'Host', w0,
                    'Port', w3,
                    'SSL', w4,
                    'EmailId', w5,
                    'EmailPassword', w6
                ]);
            }
        }
    }
}
