namespace AdvanceCRM.Settings {
    export interface TicketWebForm {
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

    export class TicketWebForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.TicketWeb';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TicketWebForm.init)  {
                TicketWebForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = BooleanSwitchEditor;
                var w2 = s.HtmlContentEditor;
                var w3 = s.TextAreaEditor;
                var w4 = s.IntegerEditor;
                var w5 = s.BooleanEditor;

                Q.initFormType(TicketWebForm, [
                    'Username', w0,
                    'Password', w0,
                    'AutoEmail', w1,
                    'AutoSms', w1,
                    'Sender', w0,
                    'Subject', w0,
                    'EmailTemplate', w2,
                    'Attachment', w0,
                    'SmsTemplate', w3,
                    'TemplateId', w0,
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
