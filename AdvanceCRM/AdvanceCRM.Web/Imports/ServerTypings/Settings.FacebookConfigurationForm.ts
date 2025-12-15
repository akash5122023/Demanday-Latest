namespace AdvanceCRM.Settings {
    export interface FacebookConfigurationForm {
        AppId: Serenity.StringEditor;
        AccessTokenKey: Serenity.TextAreaEditor;
        TokenExpiryDate: Serenity.DateEditor;
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
        EmailPassword: Serenity.StringEditor;
    }

    export class FacebookConfigurationForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.FacebookConfiguration';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!FacebookConfigurationForm.init)  {
                FacebookConfigurationForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;
                var w2 = s.DateEditor;
                var w3 = BooleanSwitchEditor;
                var w4 = s.HtmlContentEditor;
                var w5 = LargeFileUploadEditor;
                var w6 = s.IntegerEditor;
                var w7 = s.BooleanEditor;

                Q.initFormType(FacebookConfigurationForm, [
                    'AppId', w0,
                    'AccessTokenKey', w1,
                    'TokenExpiryDate', w2,
                    'AutoEmail', w3,
                    'AutoSms', w3,
                    'Sender', w0,
                    'Subject', w0,
                    'EmailTemplate', w4,
                    'Attachment', w5,
                    'SMSTemplate', w1,
                    'SmsTemplateId', w0,
                    'WaTemplate', w1,
                    'WaTemplateId', w0,
                    'Host', w0,
                    'Port', w6,
                    'SSL', w7,
                    'EmailId', w0,
                    'EmailPassword', w0
                ]);
            }
        }
    }
}
