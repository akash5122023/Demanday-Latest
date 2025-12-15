namespace AdvanceCRM.BizMail {
    export interface BmListForm {
        ListId: Serenity.StringEditor;
        CompanyName: Serenity.StringEditor;
        Name: Serenity.StringEditor;
        City: Serenity.StringEditor;
        DisplayName: Serenity.StringEditor;
        From: Serenity.StringEditor;
        ReplyTo: Serenity.StringEditor;
        Description: Serenity.StringEditor;
    }

    export class BmListForm extends Serenity.PrefixedContext {
        static formKey = 'BizMail.BmList';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BmListForm.init)  {
                BmListForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(BmListForm, [
                    'ListId', w0,
                    'CompanyName', w0,
                    'Name', w0,
                    'City', w0,
                    'DisplayName', w0,
                    'From', w0,
                    'ReplyTo', w0,
                    'Description', w0
                ]);
            }
        }
    }
}
