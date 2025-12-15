namespace AdvanceCRM.Settings {
    export interface MailInboxForm {
        Host: Serenity.StringEditor;
        Port: Serenity.IntegerEditor;
        Ssl: Serenity.BooleanEditor;
        EmailId: Serenity.StringEditor;
        EmailPassword: Serenity.PasswordEditor;
        AutoEmail: BooleanSwitchEditor;
        Sender: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        EmailTemplate: Serenity.HtmlContentEditor;
        Attachment: Serenity.StringEditor;
        SHost: Serenity.StringEditor;
        SPort: Serenity.IntegerEditor;
        Sssl: Serenity.BooleanEditor;
        SEmailId: Serenity.StringEditor;
        SEmailPassword: Serenity.PasswordEditor;
    }

    export class MailInboxForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.MailInbox';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!MailInboxForm.init)  {
                MailInboxForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.IntegerEditor;
                var w2 = s.BooleanEditor;
                var w3 = s.PasswordEditor;
                var w4 = BooleanSwitchEditor;
                var w5 = s.HtmlContentEditor;

                Q.initFormType(MailInboxForm, [
                    'Host', w0,
                    'Port', w1,
                    'Ssl', w2,
                    'EmailId', w0,
                    'EmailPassword', w3,
                    'AutoEmail', w4,
                    'Sender', w0,
                    'Subject', w0,
                    'EmailTemplate', w5,
                    'Attachment', w0,
                    'SHost', w0,
                    'SPort', w1,
                    'Sssl', w2,
                    'SEmailId', w0,
                    'SEmailPassword', w3
                ]);
            }
        }
    }
}
