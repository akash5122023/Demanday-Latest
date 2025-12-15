namespace AdvanceCRM.ThirdParty {
    export interface WatiContactsForm {
        Waid: Serenity.StringEditor;
        FirtName: Serenity.StringEditor;
        FullName: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Source: Serenity.StringEditor;
        Status: Serenity.StringEditor;
        Created: Serenity.DateEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class WatiContactsForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.WatiContacts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!WatiContactsForm.init)  {
                WatiContactsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateEditor;
                var w2 = s.BooleanEditor;

                Q.initFormType(WatiContactsForm, [
                    'Waid', w0,
                    'FirtName', w0,
                    'FullName', w0,
                    'Phone', w0,
                    'Source', w0,
                    'Status', w0,
                    'Created', w1,
                    'IsMoved', w2
                ]);
            }
        }
    }
}
