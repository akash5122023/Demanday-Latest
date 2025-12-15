namespace AdvanceCRM.ThirdParty {
    export interface MailInboxDetailsForm {
        Subject: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        ToName: Serenity.StringEditor;
        To: Serenity.StringEditor;
        ToAddress: Serenity.StringEditor;
        FromName: Serenity.StringEditor;
        From: Serenity.StringEditor;
        FromAddress: Serenity.StringEditor;
        CreatedDate: Serenity.DateEditor;
        Content: Serenity.TextAreaEditor;
        Attachment: Serenity.StringEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class MailInboxDetailsForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.MailInboxDetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!MailInboxDetailsForm.init)  {
                MailInboxDetailsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateEditor;
                var w2 = s.TextAreaEditor;
                var w3 = s.BooleanEditor;

                Q.initFormType(MailInboxDetailsForm, [
                    'Subject', w0,
                    'Phone', w0,
                    'ToName', w0,
                    'To', w0,
                    'ToAddress', w0,
                    'FromName', w0,
                    'From', w0,
                    'FromAddress', w0,
                    'CreatedDate', w1,
                    'Content', w2,
                    'Attachment', w0,
                    'IsMoved', w3
                ]);
            }
        }
    }
}
