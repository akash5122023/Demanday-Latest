namespace AdvanceCRM.Settings {
    export interface SulekhaForm {
        PostUrl: Serenity.TextAreaEditor;
        Username: Serenity.StringEditor;
        Password: Serenity.StringEditor;
        AutoEmail: BooleanSwitchEditor;
        AutoSms: BooleanSwitchEditor;
        Sender: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        EmailTemplate: Serenity.HtmlContentEditor;
        Attachment: Serenity.StringEditor;
        SmsTemplate: Serenity.TextAreaEditor;
        TemplateId: Serenity.StringEditor;
        Host: Serenity.StringEditor;
        Port: Serenity.IntegerEditor;
        Ssl: Serenity.BooleanEditor;
        EmailId: Serenity.StringEditor;
        EmailPassword: Serenity.StringEditor;
    }

    export class SulekhaForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.Sulekha';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!SulekhaForm.init)  {
                SulekhaForm.init = true;

                var s = Serenity;
                var w0 = s.TextAreaEditor;
                var w1 = s.StringEditor;
                var w2 = BooleanSwitchEditor;
                var w3 = s.HtmlContentEditor;
                var w4 = s.IntegerEditor;
                var w5 = s.BooleanEditor;

                Q.initFormType(SulekhaForm, [
                    'PostUrl', w0,
                    'Username', w1,
                    'Password', w1,
                    'AutoEmail', w2,
                    'AutoSms', w2,
                    'Sender', w1,
                    'Subject', w1,
                    'EmailTemplate', w3,
                    'Attachment', w1,
                    'SmsTemplate', w0,
                    'TemplateId', w1,
                    'Host', w1,
                    'Port', w4,
                    'Ssl', w5,
                    'EmailId', w1,
                    'EmailPassword', w1
                ]);
            }
        }
    }
}
