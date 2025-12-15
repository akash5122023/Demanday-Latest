namespace AdvanceCRM.Template {
    export interface ChallanTemplateForm {
        Sender: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        EmailTemplate: Serenity.HtmlContentEditor;
        TermsConditions: Serenity.TextAreaEditor;
        SMSTemplate: Serenity.TextAreaEditor;
        TemplateId: Serenity.StringEditor;
        WaTemplate: Serenity.TextAreaEditor;
        WaTemplateId: Serenity.StringEditor;
        CCEmails: Serenity.TextAreaEditor;
        Bcc: Serenity.TextAreaEditor;
        CcsmSs: Serenity.TextAreaEditor;
        Host: Serenity.StringEditor;
        Port: Serenity.IntegerEditor;
        SSL: Serenity.BooleanEditor;
        EmailId: Serenity.EmailEditor;
        EmailPassword: Serenity.PasswordEditor;
    }

    export class ChallanTemplateForm extends Serenity.PrefixedContext {
        static formKey = 'Template.ChallanTemplate';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ChallanTemplateForm.init)  {
                ChallanTemplateForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.HtmlContentEditor;
                var w2 = s.TextAreaEditor;
                var w3 = s.IntegerEditor;
                var w4 = s.BooleanEditor;
                var w5 = s.EmailEditor;
                var w6 = s.PasswordEditor;

                Q.initFormType(ChallanTemplateForm, [
                    'Sender', w0,
                    'Subject', w0,
                    'EmailTemplate', w1,
                    'TermsConditions', w2,
                    'SMSTemplate', w2,
                    'TemplateId', w0,
                    'WaTemplate', w2,
                    'WaTemplateId', w0,
                    'CCEmails', w2,
                    'Bcc', w2,
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
