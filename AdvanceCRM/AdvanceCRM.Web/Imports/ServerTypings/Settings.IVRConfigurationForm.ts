namespace AdvanceCRM.Settings {
    export interface IVRConfigurationForm {
        IVRType: Serenity.EnumEditor;
        IVRNumber: Serenity.StringEditor;
        ApiKey: Serenity.StringEditor;
        Plan: Serenity.StringEditor;
        AppId: Serenity.StringEditor;
        AppSecret: Serenity.StringEditor;
        Token_Id: Serenity.StringEditor;
        userType: Serenity.StringEditor;
        Number: Serenity.StringEditor;
        CliNumber: Serenity.StringEditor;
        Username: Serenity.StringEditor;
        Password: Serenity.PasswordEditor;
        PostUrl: Serenity.TextAreaEditor;
        Agents: KnowlarityAgentsEditor;
        AutoEmail: BooleanSwitchEditor;
        RoundRobin: BooleanSwitchEditor;
        AutoRefresh: BooleanSwitchEditor;
        AutoRefreshTime: Serenity.IntegerEditor;
        AutoSms: BooleanSwitchEditor;
        Sender: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        EmailTemplate: Serenity.HtmlContentEditor;
        Attachment: Serenity.StringEditor;
        SmsTemplate: Serenity.TextAreaEditor;
        TemplateId: Serenity.StringEditor;
        WaTemplate: Serenity.TextAreaEditor;
        WaTemplateId: Serenity.StringEditor;
        Host: Serenity.StringEditor;
        Port: Serenity.IntegerEditor;
        Ssl: Serenity.BooleanEditor;
        EmailId: Serenity.StringEditor;
        EmailPassword: Serenity.StringEditor;
    }

    export class IVRConfigurationForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.IVRConfiguration';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!IVRConfigurationForm.init)  {
                IVRConfigurationForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.StringEditor;
                var w2 = s.PasswordEditor;
                var w3 = s.TextAreaEditor;
                var w4 = KnowlarityAgentsEditor;
                var w5 = BooleanSwitchEditor;
                var w6 = s.IntegerEditor;
                var w7 = s.HtmlContentEditor;
                var w8 = s.BooleanEditor;

                Q.initFormType(IVRConfigurationForm, [
                    'IVRType', w0,
                    'IVRNumber', w1,
                    'ApiKey', w1,
                    'Plan', w1,
                    'AppId', w1,
                    'AppSecret', w1,
                    'Token_Id', w1,
                    'userType', w1,
                    'Number', w1,
                    'CliNumber', w1,
                    'Username', w1,
                    'Password', w2,
                    'PostUrl', w3,
                    'Agents', w4,
                    'AutoEmail', w5,
                    'RoundRobin', w5,
                    'AutoRefresh', w5,
                    'AutoRefreshTime', w6,
                    'AutoSms', w5,
                    'Sender', w1,
                    'Subject', w1,
                    'EmailTemplate', w7,
                    'Attachment', w1,
                    'SmsTemplate', w3,
                    'TemplateId', w1,
                    'WaTemplate', w3,
                    'WaTemplateId', w1,
                    'Host', w1,
                    'Port', w6,
                    'Ssl', w8,
                    'EmailId', w1,
                    'EmailPassword', w1
                ]);
            }
        }
    }
}
