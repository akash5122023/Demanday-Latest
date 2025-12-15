namespace AdvanceCRM.Template {
    export interface InvoiceTemplateForm {
        Sender: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        EmailTemplate: Serenity.HtmlContentEditor;
        Attachment: LargeFileUploadEditor;
        TermsConditions: Serenity.TextAreaEditor;
        SMSTemplate: Serenity.TextAreaEditor;
        TemplateId: Serenity.StringEditor;
        SmsReminder: Serenity.TextAreaEditor;
        SmsrTemplateId: Serenity.StringEditor;
        WaTemplate: Serenity.TextAreaEditor;
        WaTemplateId: Serenity.StringEditor;
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

    export class InvoiceTemplateForm extends Serenity.PrefixedContext {
        static formKey = 'Template.InvoiceTemplate';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!InvoiceTemplateForm.init)  {
                InvoiceTemplateForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.HtmlContentEditor;
                var w2 = LargeFileUploadEditor;
                var w3 = s.TextAreaEditor;
                var w4 = s.IntegerEditor;
                var w5 = s.BooleanEditor;
                var w6 = s.EmailEditor;
                var w7 = s.PasswordEditor;

                Q.initFormType(InvoiceTemplateForm, [
                    'Sender', w0,
                    'Subject', w0,
                    'EmailTemplate', w1,
                    'Attachment', w2,
                    'TermsConditions', w3,
                    'SMSTemplate', w3,
                    'TemplateId', w0,
                    'SmsReminder', w3,
                    'SmsrTemplateId', w0,
                    'WaTemplate', w3,
                    'WaTemplateId', w0,
                    'WaReminder', w3,
                    'WarTemplateId', w0,
                    'CCEmails', w3,
                    'BCCEmails', w3,
                    'CcsmSs', w3,
                    'Host', w0,
                    'Port', w4,
                    'SSL', w5,
                    'EmailId', w6,
                    'EmailPassword', w7
                ]);
            }
        }
    }
}
