namespace AdvanceCRM.Settings {
    export interface WebsiteEnquiryConfigurationForm {
        API: Serenity.TextAreaEditor;
        Username: Serenity.StringEditor;
        Password: Serenity.StringEditor;
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

    export class WebsiteEnquiryConfigurationForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.WebsiteEnquiryConfiguration';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!WebsiteEnquiryConfigurationForm.init)  {
                WebsiteEnquiryConfigurationForm.init = true;

                var s = Serenity;
                var w0 = s.TextAreaEditor;
                var w1 = s.StringEditor;
                var w2 = BooleanSwitchEditor;
                var w3 = s.HtmlContentEditor;
                var w4 = LargeFileUploadEditor;
                var w5 = s.IntegerEditor;
                var w6 = s.BooleanEditor;

                Q.initFormType(WebsiteEnquiryConfigurationForm, [
                    'API', w0,
                    'Username', w1,
                    'Password', w1,
                    'AutoEmail', w2,
                    'AutoSms', w2,
                    'Sender', w1,
                    'Subject', w1,
                    'EmailTemplate', w3,
                    'Attachment', w4,
                    'SMSTemplate', w0,
                    'SmsTemplateId', w1,
                    'WaTemplate', w0,
                    'WaTemplateId', w1,
                    'Host', w1,
                    'Port', w5,
                    'SSL', w6,
                    'EmailId', w1,
                    'EmailPassword', w1
                ]);
            }
        }
    }
}
