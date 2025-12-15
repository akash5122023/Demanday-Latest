namespace AdvanceCRM.ThirdParty {
    export interface SIndiaMartDetailsForm {
        SenderName: Serenity.StringEditor;
        SenderEmail: Serenity.StringEditor;
        Subject: Serenity.StringEditor;
        ProductName: Serenity.StringEditor;
        Source: Serenity.EnumEditor;
        DateTimeRe: Serenity.DateTimeEditor;
        GlUserCompanyName: Serenity.StringEditor;
        Mob: Serenity.StringEditor;
        EnqMessage: Serenity.TextAreaEditor;
        EnqAddress: Serenity.TextAreaEditor;
        EnqCallDuration: Serenity.StringEditor;
        EnqReceiverMob: Serenity.StringEditor;
        EnqCity: Serenity.StringEditor;
        EnqState: Serenity.StringEditor;
        EmailAlt: Serenity.StringEditor;
        MobileAlt: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        PhoneAlt: Serenity.StringEditor;
        Feedback: Serenity.TextAreaEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class SIndiaMartDetailsForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.SIndiaMartDetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!SIndiaMartDetailsForm.init)  {
                SIndiaMartDetailsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.EnumEditor;
                var w2 = s.DateTimeEditor;
                var w3 = s.TextAreaEditor;
                var w4 = s.BooleanEditor;

                Q.initFormType(SIndiaMartDetailsForm, [
                    'SenderName', w0,
                    'SenderEmail', w0,
                    'Subject', w0,
                    'ProductName', w0,
                    'Source', w1,
                    'DateTimeRe', w2,
                    'GlUserCompanyName', w0,
                    'Mob', w0,
                    'EnqMessage', w3,
                    'EnqAddress', w3,
                    'EnqCallDuration', w0,
                    'EnqReceiverMob', w0,
                    'EnqCity', w0,
                    'EnqState', w0,
                    'EmailAlt', w0,
                    'MobileAlt', w0,
                    'Phone', w0,
                    'PhoneAlt', w0,
                    'Feedback', w3,
                    'IsMoved', w4
                ]);
            }
        }
    }
}
