namespace AdvanceCRM.Settings {
    export interface TradeIndiaConfigurationForm {
        Userid: Serenity.StringEditor;
        Profileid: Serenity.StringEditor;
        ApiKey: Serenity.StringEditor;
        AutoEmail: BooleanSwitchEditor;
        AutoSms: BooleanSwitchEditor;
        Sender: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        EmailTemplate: Serenity.HtmlContentEditor;
        Attachment: LargeFileUploadEditor;
        SMSTemplate: Serenity.TextAreaEditor;
        SmsTemplateId: Serenity.StringEditor;
        WaTemplate: Serenity.TextAreaEditor;
        WaTemplateId: Serenity.StringEditor;
        Host: Serenity.StringEditor;
        Port: Serenity.IntegerEditor;
        SSL: Serenity.BooleanEditor;
        EmailId: Serenity.StringEditor;
        EmailPassword: Serenity.PasswordEditor;
    }

    export class TradeIndiaConfigurationForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.TradeIndiaConfiguration';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TradeIndiaConfigurationForm.init)  {
                TradeIndiaConfigurationForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = BooleanSwitchEditor;
                var w2 = s.HtmlContentEditor;
                var w3 = LargeFileUploadEditor;
                var w4 = s.TextAreaEditor;
                var w5 = s.IntegerEditor;
                var w6 = s.BooleanEditor;
                var w7 = s.PasswordEditor;

                Q.initFormType(TradeIndiaConfigurationForm, [
                    'Userid', w0,
                    'Profileid', w0,
                    'ApiKey', w0,
                    'AutoEmail', w1,
                    'AutoSms', w1,
                    'Sender', w0,
                    'Subject', w0,
                    'EmailTemplate', w2,
                    'Attachment', w3,
                    'SMSTemplate', w4,
                    'SmsTemplateId', w0,
                    'WaTemplate', w4,
                    'WaTemplateId', w0,
                    'Host', w0,
                    'Port', w5,
                    'SSL', w6,
                    'EmailId', w0,
                    'EmailPassword', w7
                ]);
            }
        }
    }
}
