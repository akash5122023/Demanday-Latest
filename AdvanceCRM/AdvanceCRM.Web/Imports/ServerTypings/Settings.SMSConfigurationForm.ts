namespace AdvanceCRM.Settings {
    export interface SMSConfigurationForm {
        API: Serenity.StringEditor;
        BulkAPI: Serenity.StringEditor;
        ScheduleAPI: Serenity.StringEditor;
        SuccessResponse: Serenity.StringEditor;
        Username: Serenity.StringEditor;
        Password: Serenity.PasswordEditor;
        SenderId: Serenity.StringEditor;
        Key: Serenity.StringEditor;
    }

    export class SMSConfigurationForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.SMSConfiguration';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!SMSConfigurationForm.init)  {
                SMSConfigurationForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.PasswordEditor;

                Q.initFormType(SMSConfigurationForm, [
                    'API', w0,
                    'BulkAPI', w0,
                    'ScheduleAPI', w0,
                    'SuccessResponse', w0,
                    'Username', w0,
                    'Password', w1,
                    'SenderId', w0,
                    'Key', w0
                ]);
            }
        }
    }
}
