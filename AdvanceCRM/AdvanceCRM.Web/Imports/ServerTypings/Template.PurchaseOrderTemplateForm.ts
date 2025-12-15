namespace AdvanceCRM.Template {
    export interface PurchaseOrderTemplateForm {
        Sender: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        EmailTemplate: Serenity.HtmlContentEditor;
        CCEmails: Serenity.TextAreaEditor;
        Bcc: Serenity.TextAreaEditor;
        Host: Serenity.StringEditor;
        Port: Serenity.IntegerEditor;
        SSL: Serenity.BooleanEditor;
        EmailId: Serenity.EmailEditor;
        EmailPassword: Serenity.PasswordEditor;
    }

    export class PurchaseOrderTemplateForm extends Serenity.PrefixedContext {
        static formKey = 'Template.PurchaseOrderTemplate';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!PurchaseOrderTemplateForm.init)  {
                PurchaseOrderTemplateForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.HtmlContentEditor;
                var w2 = s.TextAreaEditor;
                var w3 = s.IntegerEditor;
                var w4 = s.BooleanEditor;
                var w5 = s.EmailEditor;
                var w6 = s.PasswordEditor;

                Q.initFormType(PurchaseOrderTemplateForm, [
                    'Sender', w0,
                    'Subject', w0,
                    'EmailTemplate', w1,
                    'CCEmails', w2,
                    'Bcc', w2,
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
