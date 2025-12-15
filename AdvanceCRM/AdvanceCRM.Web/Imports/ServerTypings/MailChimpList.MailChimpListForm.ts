namespace AdvanceCRM.MailChimpList {
    export interface MailChimpListForm {
        ListName: Serenity.StringEditor;
    }

    export class MailChimpListForm extends Serenity.PrefixedContext {
        static formKey = 'MailChimp.MailChimpList';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!MailChimpListForm.init)  {
                MailChimpListForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(MailChimpListForm, [
                    'ListName', w0
                ]);
            }
        }
    }
}
