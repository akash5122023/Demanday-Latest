namespace AdvanceCRM.Template {
    export interface AMCTemplateForm {
        Sender: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        EmailTemplateReceipt: Serenity.HtmlContentEditor;
        TermsConditions: Serenity.TextAreaEditor;
        SMSTemplate: Serenity.TextAreaEditor;
        SmsTempId: Serenity.StringEditor;
        VisitSMSTemplate: Serenity.TextAreaEditor;
        VisitTempId: Serenity.StringEditor;
        WaTemplate: Serenity.TextAreaEditor;
        WaTemplateId: Serenity.StringEditor;
        WaVisitTemplate: Serenity.TextAreaEditor;
        WaVisitTemplateId: Serenity.StringEditor;
        CCEmails: Serenity.TextAreaEditor;
        BCCEmails: Serenity.TextAreaEditor;
        Host: Serenity.StringEditor;
        Port: Serenity.IntegerEditor;
        SSL: Serenity.BooleanEditor;
        EmailId: Serenity.EmailEditor;
        EmailPassword: Serenity.PasswordEditor;
    }

    export class AMCTemplateForm extends Serenity.PrefixedContext {
        static formKey = 'Template.AMCTemplate';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AMCTemplateForm.init)  {
                AMCTemplateForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.HtmlContentEditor;
                var w2 = s.TextAreaEditor;
                var w3 = s.IntegerEditor;
                var w4 = s.BooleanEditor;
                var w5 = s.EmailEditor;
                var w6 = s.PasswordEditor;

                Q.initFormType(AMCTemplateForm, [
                    'Sender', w0,
                    'Subject', w0,
                    'EmailTemplateReceipt', w1,
                    'TermsConditions', w2,
                    'SMSTemplate', w2,
                    'SmsTempId', w0,
                    'VisitSMSTemplate', w2,
                    'VisitTempId', w0,
                    'WaTemplate', w2,
                    'WaTemplateId', w0,
                    'WaVisitTemplate', w2,
                    'WaVisitTemplateId', w0,
                    'CCEmails', w2,
                    'BCCEmails', w2,
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
