namespace AdvanceCRM.Template {
    export interface EnquiryTemplateForm {
        Sender: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        EmailTemplate: Serenity.HtmlContentEditor;
        Attachment: LargeFileUploadEditor;
        SMSTemplate: Serenity.TextAreaEditor;
        TemplateId: Serenity.StringEditor;
        SmsReminder: Serenity.TextAreaEditor;
        SmsrTemplateId: Serenity.StringEditor;
        WaTemplate: Serenity.TextAreaEditor;
        WaTemplateId: Serenity.StringEditor;
        WaReminder: Serenity.TextAreaEditor;
        WarTemplateId: Serenity.StringEditor;
        Host: Serenity.StringEditor;
        Port: Serenity.IntegerEditor;
        SSL: Serenity.BooleanEditor;
        EmailId: Serenity.EmailEditor;
        EmailPassword: Serenity.PasswordEditor;
    }

    export class EnquiryTemplateForm extends Serenity.PrefixedContext {
        static formKey = 'Template.EnquiryTemplate';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!EnquiryTemplateForm.init)  {
                EnquiryTemplateForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.HtmlContentEditor;
                var w2 = LargeFileUploadEditor;
                var w3 = s.TextAreaEditor;
                var w4 = s.IntegerEditor;
                var w5 = s.BooleanEditor;
                var w6 = s.EmailEditor;
                var w7 = s.PasswordEditor;

                Q.initFormType(EnquiryTemplateForm, [
                    'Sender', w0,
                    'Subject', w0,
                    'EmailTemplate', w1,
                    'Attachment', w2,
                    'SMSTemplate', w3,
                    'TemplateId', w0,
                    'SmsReminder', w3,
                    'SmsrTemplateId', w0,
                    'WaTemplate', w3,
                    'WaTemplateId', w0,
                    'WaReminder', w3,
                    'WarTemplateId', w0,
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
