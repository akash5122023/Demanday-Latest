namespace AdvanceCRM.Settings {
    export interface InstamojoForm {
        AppId: Serenity.StringEditor;
        AccessTokenKey: Serenity.TextAreaEditor;
        AutoEmail: BooleanSwitchEditor;
        AutoSms: BooleanSwitchEditor;
        Sender: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        EmailTemplate: Serenity.HtmlContentEditor;
        Attachment: Serenity.StringEditor;
        SmsTemplate: Serenity.TextAreaEditor;
        SmsTemplateId: Serenity.StringEditor;
        Host: Serenity.StringEditor;
        Port: Serenity.IntegerEditor;
        Ssl: Serenity.BooleanEditor;
        EmailId: Serenity.StringEditor;
        EmailPassword: Serenity.StringEditor;
    }

    export class InstamojoForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.Instamojo';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!InstamojoForm.init)  {
                InstamojoForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;
                var w2 = BooleanSwitchEditor;
                var w3 = s.HtmlContentEditor;
                var w4 = s.IntegerEditor;
                var w5 = s.BooleanEditor;

                Q.initFormType(InstamojoForm, [
                    'AppId', w0,
                    'AccessTokenKey', w1,
                    'AutoEmail', w2,
                    'AutoSms', w2,
                    'Sender', w0,
                    'Subject', w0,
                    'EmailTemplate', w3,
                    'Attachment', w0,
                    'SmsTemplate', w1,
                    'SmsTemplateId', w0,
                    'Host', w0,
                    'Port', w4,
                    'Ssl', w5,
                    'EmailId', w0,
                    'EmailPassword', w0
                ]);
            }
        }
    }
}
